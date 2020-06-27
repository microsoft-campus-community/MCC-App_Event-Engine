using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.CampusCommunity.Infrastructure.Configuration;
using Microsoft.CampusCommunity.Infrastructure.Entities;
using Microsoft.CampusCommunity.Infrastructure.Entities.Dto;
using Microsoft.CampusCommunity.Infrastructure.Enums;
using Microsoft.CampusCommunity.Infrastructure.Exceptions;
using Microsoft.CampusCommunity.Infrastructure.Extensions;
using Microsoft.CampusCommunity.Infrastructure.Helpers;
using Microsoft.CampusCommunity.Infrastructure.Interfaces;
using Microsoft.Graph;

namespace Microsoft.CampusCommunity.Services.Graph
{
    public class GraphUserService : IGraphUserService
    {
        public const string UserJobTitleCampusLead = "Campus Lead";
        public const string UserJobTitleHubLead = "Hub Lead";
        public const string UserJobTitleMember = "Member";

        private const string GraphUserSelectTerm = "Id,AccountEnabled,City,Department,DisplayName,JobTitle,Mail,OfficeLocation";

        // TODO: Define in config
        public readonly Guid SkuId = new Guid("314c4481-f395-4525-be8b-2ec4bb1e9d91");

        public static readonly PasswordOptions DefaultPasswordOptions = new PasswordOptions()
        {
            RequiredLength = 15
        };

        // public readonly List<string> DefaultNewUserGroups = new List<string>(){
        // 	"", // everyone
        // 	""
        // };

        private readonly IGraphBaseService _graphService;
        private readonly IGraphGroupService _graphGroupService;
        private readonly IAppInsightsService _appInsightsService;
        private readonly AuthorizationConfiguration _authorizationConfiguration;

        public GraphUserService(IGraphBaseService graphService, IGraphGroupService graphGroupService,
            IAppInsightsService appInsightsService, AuthorizationConfiguration authorizationConfiguration)
        {
            _graphService = graphService;
            _appInsightsService = appInsightsService;
            _graphGroupService = graphGroupService;
            _authorizationConfiguration = authorizationConfiguration;
        }

        public async Task<IEnumerable<BasicUser>> GetAllUsers(UserScope scope)
        {
            // TODO: check in the future if office location is supported as a graph filter attribute. Try something like $filter=officeLocation+eq+'Munich'. Currently this is not supported.
            var queryOptions = new List<QueryOption>
            {
                new QueryOption("$select", GraphUserSelectTerm)
            };
            var users = await _graphService.Client.Users.Request(queryOptions).GetAsync();

            // only return users where location is not empty
            var filteredUsers = users.Where(u => !string.IsNullOrWhiteSpace(u.OfficeLocation));

            // if the user scope is "full" we have to get group memberships for each user as well
            if (scope == UserScope.Full)
            {
                return await AddFullScope(filteredUsers);
            }

            return GraphHelper.MapBasicUsers(filteredUsers);
        }

        public async Task<IEnumerable<FullUser>> AddFullScope(IEnumerable<User> graphUsers)
        {
            var authGroups = await _graphGroupService.GetGroupMembersOfAuthorizationGroups();
            return GraphHelper.MapFullUsers(graphUsers, authGroups);
        }

        public async Task<FullUser> AddFullScope(User graphUser)
        {
            var authGroups = await _graphGroupService.GetGroupMembersOfAuthorizationGroups();
            return GraphHelper.MapFullUser(graphUser, authGroups);
        }

        public async Task<BasicUser> GetBasicUserById(Guid userId, UserScope scope)
        {
            var user = await GetGraphUserById(userId);

            if (scope == UserScope.Basic)
                return BasicUser.FromGraphUser(user);

            return await AddFullScope(user);
        }

        public async Task<User> GetGraphUserById(Guid userId)
        {
            User user;
            try
            {
                user = await _graphService.Client.Users[userId.ToString()].Request().GetAsync();
            }
            catch (ServiceException e)
            {
                throw new MccNotFoundException(
                    $"Could not find user with id {userId}. Please see inner exception for details", e);
            }

            return user;
        }

        public async Task<User> GetLeadForCampus(Guid campusId)
        {
            var queryOptions = new List<QueryOption>
            {
                new QueryOption("$filter", $@"jobTitle eq '{UserJobTitleCampusLead}' AND department eq '{campusId}'"),
                new QueryOption("$select", GraphUserSelectTerm)

            };

            var userResult = await _graphService.Client.Users.Request(queryOptions).GetAsync();
            if (userResult.Count != 1)
                return null;
            return userResult[0];
        }

      
        /// <inheritdoc />
        public async Task<Guid> GetCampusIdForUser(Guid userId)
        {
            var user = await FindById(userId);

            // return campus id if guid can be parsed
            if (Guid.TryParse(user.Department, out var campusId))
                return campusId;
            return Guid.Empty;
        }

        private async Task AssignLicense(User user)
        {
            var addLicenses = new List<AssignedLicense>()
            {
                new AssignedLicense
                {
                    DisabledPlans = new List<Guid>(),
                    SkuId = SkuId
                }
            };

            await _graphService.Client.Users[user.Id]
                .AssignLicense(addLicenses, new List<Guid>())
                .Request()
                .PostAsync();
        }

        public async Task DefineCampusLead(Guid userId, Guid campusId)
        {
            // get the user and campus
            var user = await FindById(userId);
            var campus = await _graphGroupService.GetGroupById(campusId);

            // find existing lead of campus (if possible)
            User existingLead = await GetLeadForCampus(campus.Id);
            if (existingLead != null)
                throw new MccBadRequestException(
                    $"Unable to assign campus lead because there is already an existing lead defined ({existingLead.Mail})");

            // make sure the user has the correct job title and campus assigned
            var userUpdate = new User()
            {
                Id = userId.ToString(),
                JobTitle = UserJobTitleCampusLead,
                Department = campus.Id.ToString(),
                CompanyName = campus.Name
            };
            await _graphService.Client.Users[user.Id].Request().UpdateAsync(userUpdate);

            // add the campus lead to the campusLeads group
            await _graphGroupService.AddUserToGroup(user, _authorizationConfiguration.CampusLeadsGroupId);


            // change the manager of all members of the group to the new campus lead
            var campusMembers = await _graphGroupService.GetGroupMembers(campusId);

            // where() -> don't change the manager of the lead itself.
            foreach (var member in campusMembers.Where(m => m.Id != user.Id))
                try
                {
                    await AssignManager(member, user.Id);
                }
                catch (Exception e)
                {
                    _appInsightsService.TrackException(null,
                        new Exception($"Could not assign manager {user.Id} to user {user.Id} ({user.MailNickname}).",
                            e), Guid.Empty);
                }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newLead"></param>
        /// <param name="campusLeads">Ids of campus leads under hub</param>
        /// <param name="hubId"></param>
        /// <returns></returns>
        public async Task DefineHubLead(Guid newLead, IEnumerable<Guid> campusLeads, Guid hubId)
        {
            var user = await FindById(newLead);
            var hub = await _graphGroupService.GetGroupById(hubId);
            
            // make sure the user has the correct job title
            var userUpdate = new User()
            {
                Id = newLead.ToString(),
                JobTitle = UserJobTitleHubLead
            };
            await _graphService.Client.Users[user.Id].Request().UpdateAsync(userUpdate);

            // add the campus lead to the campusLeads group
            await _graphGroupService.AddUserToGroup(user, _authorizationConfiguration.HubLeadsGroupId);

            // change the manager of all members of the group to the new campus lead

            // where() -> don't change the manager of the lead itself.
            foreach (var member in campusLeads.Where(m => m.ToString() != user.Id))
                try
                {
                    await AssignManager(member, user.Id);
                }
                catch (Exception e)
                {
                    _appInsightsService.TrackException(null,
                        new Exception($"Could not assign manager {user.Id} to user {user.Id} ({user.MailNickname}).",
                            e), Guid.Empty);
                }
        }

        public Task AssignManager(User user, string managerId)
        {
            // do not assign manager to himself
            if (user.Id == managerId) return Task.CompletedTask;

            return _graphService
                .Client
                .Users[user.Id]
                .Manager
                .Reference
                .Request()
                .PutAsync(managerId);
        }

        public Task AssignManager(Guid userId, string managerId)
        {
            // do not assign manager to himself
            if (userId.ToString() == managerId) return Task.CompletedTask;

            return _graphService
                .Client
                .Users[userId.ToString()]
                .Manager
                .Reference
                .Request()
                .PutAsync(managerId);
        }

        private async Task<User> FindByAlias(string alias)
        {
            var queryOptions = new List<QueryOption>
            {
                new QueryOption("$filter", $@"mailNickname eq '{alias}'"),
                new QueryOption("$select", GraphUserSelectTerm)
            };

            var userResult = await _graphService.Client.Users.Request(queryOptions).GetAsync();
            if (userResult.Count != 1)
                throw new ApplicationException($"Unable to find a user with the alias {alias}");
            return userResult[0];
        }

        private async Task<User> FindById(Guid userId)
        {
            var queryOptions = new List<QueryOption>
            {
                new QueryOption("$select", GraphUserSelectTerm)
            };
            var userResult = await _graphService.Client.Users[userId.ToString()].Request(queryOptions).GetAsync();
            if (userResult == null)
                throw new ApplicationException($"Unable to find a user with the id {userId}");
            return userResult;
        }

        public async Task SendMail(string subject, string body, User fromUser, string to)
        {
            var message = new Message
            {
                Subject = subject,
                Sender = new Recipient()
                {
                    EmailAddress = new EmailAddress()
                    {
                        Name = fromUser.DisplayName,
                        Address = fromUser.Mail
                    }
                },
                From = new Recipient()
                {
                    EmailAddress = new EmailAddress()
                    {
                        Name = fromUser.DisplayName,
                        Address = fromUser.Mail
                    }
                },
                Body = new ItemBody
                {
                    ContentType = BodyType.Text,
                    Content = body
                },
                ToRecipients = new List<Recipient>()
                {
                    new Recipient
                    {
                        EmailAddress = new EmailAddress
                        {
                            Address = to
                        }
                    }
                }
            };
            var r = _graphService.Client.Users[fromUser.Id]
                .SendMail(message, false)
                .Request();
            await r.PostAsync();
        }


    }
}
