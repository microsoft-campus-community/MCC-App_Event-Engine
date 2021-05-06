using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Text;

namespace Microsoft.CampusCommunity.EventEngine.Api.Public
{
    public static class V1HeroImageGetSingle
    {
        [FunctionName("V1HeroImageGetSingle")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "v1/public/heroimages/{heroimagename}")] HttpRequest req,
            ILogger log,
            String heroimagename)
        {


            string svg = "<svg xmlns=\"http://www.w3.org/2000/svg\" viewBox=\"-6318 -51 246.209 42\"><defs><style>.b{fill:#f60}.d{fill:#0066b3;fill-rule:evenodd}</style></defs><path fill=\"#fff\" d=\"M-6318-50.792h59.43V-9H-6318z\"/><path class=\"b\" d=\"M-6258.57-16.428h-22.572c-.622 3.4-1.133 6.294-1.333 7.428h23.905zM-6318-16.428V-9h23.905l-1.6-7.428z\"/><path d=\"M-6306.314-33.714l-1.755-7.613h14.819l4.821 21.775h.2l3-13.884c1.511-6.364 2.088-7.891 10.664-7.891h6.154s-1.244 5.878-2.8 12.264c-.6 2.43-5.732 2.152-5.488-.116.022-.231.267-1.944.467-3.17.289-1.759-1.666-1.967-2.022-.231-.244 1.25-1.355 7.243-2.4 13.051h22.084v-31.24H-6318v31.24h21.573l-3.044-14.185z\" fill=\"#0066b3\"/><g><path class=\"d\" d=\"M-6242.329-49.75h-3.155l5.065 15.388h3.644l5.022-15.388h-2.931l-2.622 8.562a138.267 138.267 0 0 0-1.155 4.026c-.222-.74-.356-1.3-.622-2.036-.222-.787-.356-1.342-.622-2.129l-2.624-8.423zM-6226.267-45.979a6.237 6.237 0 0 0-3.022.787 6.637 6.637 0 0 0-1.022.787 6.131 6.131 0 0 0-1.644 4.3 5.66 5.66 0 0 0 3.644 5.6 6.107 6.107 0 0 0 2.044.324 6.289 6.289 0 0 0 2.222-.37 5.549 5.549 0 0 0 3.466-5.507 6.381 6.381 0 0 0-.178-1.574 5.506 5.506 0 0 0-3.021-3.841 5.721 5.721 0 0 0-2.488-.509zm.044 2.175a2.264 2.264 0 0 1 1.688.74 4.009 4.009 0 0 1 .933 2.823 4.105 4.105 0 0 1-.978 3.193 2.227 2.227 0 0 1-1.644.694 2.079 2.079 0 0 1-1.111-.278 2.164 2.164 0 0 1-.8-.694 4.558 4.558 0 0 1-.8-2.823 3.948 3.948 0 0 1 1.066-3.008 2.523 2.523 0 0 1 .755-.463 1.922 1.922 0 0 1 .889-.185zM-6218.179-51v16.638h2.844V-51zM-6209.447-51h-2.8v16.638h2.8v-5.646c.489.833.844 1.388 1.378 2.175l2.4 3.471h3.466l-4.577-6.387 4.088-5h-3.288l-2.444 3.193c-.4.509-.666.879-1.022 1.388V-51zM-6194.252-45.515a12.434 12.434 0 0 0-2.886-.416 5.131 5.131 0 0 0-3.466 1.2 3.074 3.074 0 0 0-.933 2.314 3.127 3.127 0 0 0 .711 2.036 6.29 6.29 0 0 0 2.888 1.53c.489.185 1.333.463 1.333 1.2s-.8 1.25-1.955 1.25a5.934 5.934 0 0 1-2.711-.74l-.178 2.453a11.232 11.232 0 0 0 3.2.509 7.282 7.282 0 0 0 2.088-.278 4.016 4.016 0 0 0 1.733-1.3 3.148 3.148 0 0 0 .667-2.083 3.194 3.194 0 0 0-1.777-2.962c-.311-.139-.8-.278-1.511-.509a4.654 4.654 0 0 1-1.422-.694.734.734 0 0 1-.267-.555c0-.787.8-1.2 1.955-1.2a8.7 8.7 0 0 1 2.355.417l.178-2.175zM-6191.453-51v16.638h2.711v-1.759a5.17 5.17 0 0 0 .4.694 3.591 3.591 0 0 0 2.977 1.25 4.893 4.893 0 0 0 1.866-.417 2.808 2.808 0 0 0 .933-.6 6.452 6.452 0 0 0 1.777-5.045 8.537 8.537 0 0 0-.311-2.407 4.075 4.075 0 0 0-4.177-3.286 3.634 3.634 0 0 0-2.844 1.2 3.779 3.779 0 0 0-.533.74V-51zm5.443 7.428a2.023 2.023 0 0 1 1.466.648 2.245 2.245 0 0 1 .444.694 5.234 5.234 0 0 1 .447 2.13 4.283 4.283 0 0 1-1.111 3.055 1.939 1.939 0 0 1-1.333.509 1.838 1.838 0 0 1-1.6-.694 2.234 2.234 0 0 1-.533-.787 4.363 4.363 0 0 1-.4-1.944c0-1.018.267-2.777 1.777-3.425a2.772 2.772 0 0 1 .844-.185zM-6169.213-34.361c-.044-1.388-.089-2.36-.089-3.749v-2.685a9.2 9.2 0 0 0-.266-2.684 3.671 3.671 0 0 0-.933-1.435 5.215 5.215 0 0 0-3.51-1.064 9.168 9.168 0 0 0-3.732.787l.089 2.545a4.091 4.091 0 0 1 1.111-.694 5.113 5.113 0 0 1 2.222-.509h.266a2.085 2.085 0 0 1 1.777.833 3.29 3.29 0 0 1 .4 1.666c-.533-.046-.933-.046-1.466-.046h-.133c-2.177 0-3.643.417-4.532 1.25a3.473 3.473 0 0 0 2.621 5.97 4.359 4.359 0 0 0 2.444-.6 3.041 3.041 0 0 0 1.2-1.25v1.666h2.533zm-2.577-5.137s-.044.509-.089.833a2.634 2.634 0 0 1-2.577 2.5 1.518 1.518 0 0 1-1.688-1.574 1.3 1.3 0 0 1 .133-.694c.489-.879 1.6-1.111 3.111-1.111a8.569 8.569 0 0 1 1.111.046zM-6156.082-34.362v-5.831c-.044-2.083-.044-3.286-.711-4.212a3.038 3.038 0 0 0-.933-.972 3.883 3.883 0 0 0-2.133-.555 4.026 4.026 0 0 0-2.31.6 4.144 4.144 0 0 0-1.066.833c-.133.185-.267.324-.4.509v-1.757h-2.712c.044 1.018.044 1.712.044 2.731v8.654h2.888v-5.322c0-1.342.089-2.545 1.111-3.332a1.846 1.846 0 0 1 1.422-.509 2.545 2.545 0 0 1 .8.139 2.139 2.139 0 0 1 .844.833 5.884 5.884 0 0 1 .356 2.684v5.507zM-6150.217-51h-2.844v16.638h2.844v-5.646c.488.833.8 1.388 1.333 2.175l2.4 3.471h3.511l-4.577-6.387 4.044-5h-3.244l-2.488 3.193c-.355.509-.622.879-.977 1.388V-51zM-6132.022-39.036a10.453 10.453 0 0 0 .045-1.157c0-3.055-1.377-5.785-4.932-5.785a4.875 4.875 0 0 0-4 1.851 5.767 5.767 0 0 0-.4.555c0 .046-.222.417-.311.648a7.287 7.287 0 0 0-.578 3.008c0 3.055 1.555 4.674 3.288 5.369a6.843 6.843 0 0 0 2.444.417 8.577 8.577 0 0 0 3.777-.833l-.044-2.545a6.281 6.281 0 0 1-.844.463 5.324 5.324 0 0 1-2.577.648 3.194 3.194 0 0 1-2.888-1.527 3.444 3.444 0 0 1-.4-1.111zm-7.42-2.083a3.48 3.48 0 0 1 .489-1.666 2.406 2.406 0 0 1 2.044-1.111 2.064 2.064 0 0 1 1.777.926 3.287 3.287 0 0 1 .533 1.851zM-6119.38-34.362v-5.831c-.045-2.083-.089-3.286-.711-4.212a4.442 4.442 0 0 0-.933-.972 3.937 3.937 0 0 0-2.177-.555 4.025 4.025 0 0 0-2.31.6 3.518 3.518 0 0 0-1.022.833c-.178.185-.266.324-.444.509v-1.757h-2.712c.045 1.018.089 1.712.089 2.731v8.654h2.844v-5.322c.045-1.342.089-2.545 1.111-3.332a1.937 1.937 0 0 1 1.422-.509 2.9 2.9 0 0 1 .844.139 1.78 1.78 0 0 1 .8.833 5.888 5.888 0 0 1 .355 2.684v5.507z\"/></g><g><path class=\"d\" d=\"M-6234.619-15.804c-.222-.6-.622-1.573-1.378-1.851a6.205 6.205 0 0 0-.533-.093 4.077 4.077 0 0 0 1.377-.37 1.274 1.274 0 0 0 .444-.278 3.468 3.468 0 0 0 1.378-2.962 4.355 4.355 0 0 0-.444-1.99 2.691 2.691 0 0 0-.489-.694c-1.244-1.342-3.377-1.342-4.665-1.342h-4.533v15.342h2.977v-6.387a13.409 13.409 0 0 1 1.6.046 3.675 3.675 0 0 1 .622.278 5.445 5.445 0 0 1 1.2 2.221l1.422 3.841h3.288l-2.266-5.762zm-5.865-7.22h1.378c.222 0 .311.046.533.046a2.1 2.1 0 0 1 1.822.787 1.957 1.957 0 0 1 .355 1.157 2.369 2.369 0 0 1-.355 1.2 2.518 2.518 0 0 1-2.4.972h-1.333v-4.162zM-6221.312-10.065c-.044-1.388-.089-2.36-.089-3.7v-2.731a9.19 9.19 0 0 0-.267-2.684 3.67 3.67 0 0 0-.933-1.435 5.215 5.215 0 0 0-3.51-1.064 9.168 9.168 0 0 0-3.732.787l.089 2.545a4.089 4.089 0 0 1 1.111-.694 5.114 5.114 0 0 1 2.222-.509h.267a2.085 2.085 0 0 1 1.777.833 3.288 3.288 0 0 1 .4 1.666c-.533-.046-.933-.046-1.466-.046h-.133c-2.177 0-3.644.417-4.532 1.25a3.473 3.473 0 0 0 2.622 5.97 4.359 4.359 0 0 0 2.444-.6 3.039 3.039 0 0 0 1.2-1.25v1.666zm-2.577-5.137s-.044.509-.089.833a2.634 2.634 0 0 1-2.577 2.5 1.55 1.55 0 0 1-1.733-1.527 1.4 1.4 0 0 1 .178-.74c.489-.879 1.6-1.111 3.11-1.111a8.57 8.57 0 0 1 1.111.046zM-6218.423-26.263v2.731h2.844v-2.731zm0 4.813v11.387h2.844V-21.45zM-6206.158-26.541a8.911 8.911 0 0 0-1.555-.185 4.423 4.423 0 0 0-1.955.417 2.989 2.989 0 0 0-1.644 1.99 10.528 10.528 0 0 0-.311 2.869h-2.135v2.224h2.133v9.164h2.844v-9.164h2.444v-2.221h-2.444c0-1.157.044-1.944.533-2.453a1.528 1.528 0 0 1 1.111-.417 5.552 5.552 0 0 1 .8.139l.178-2.36zM-6197.693-26.541a8.914 8.914 0 0 0-1.555-.185 4.424 4.424 0 0 0-1.955.417 2.99 2.99 0 0 0-1.644 1.99 10.521 10.521 0 0 0-.311 2.869h-2.135v2.224h2.133v9.164h2.844v-9.164h2.444v-2.221h-2.444c0-1.157.044-1.944.533-2.453a1.528 1.528 0 0 1 1.111-.417 5.56 5.56 0 0 1 .8.139l.178-2.36zM-6186.542-14.739v-1.157c0-3.055-1.377-5.785-4.888-5.785a4.812 4.812 0 0 0-4 1.851 1.589 1.589 0 0 0-.4.555c-.044.046-.222.417-.355.648a7.288 7.288 0 0 0-.578 3.008c0 3.055 1.555 4.674 3.288 5.369a6.843 6.843 0 0 0 2.444.417 8.259 8.259 0 0 0 3.777-.833l-.044-2.545c-.044.046-.489.324-.8.509a5.828 5.828 0 0 1-2.577.6 3.165 3.165 0 0 1-2.933-1.527 5.18 5.18 0 0 1-.4-1.111zm-7.465-2.083a4.493 4.493 0 0 1 .533-1.666 2.406 2.406 0 0 1 2.044-1.111 2 2 0 0 1 1.733.926 3.349 3.349 0 0 1 .578 1.851zM-6184.187-26.263v2.731h2.844v-2.731zm0 4.813v11.387h2.844V-21.45zM-6171.768-21.172a10.453 10.453 0 0 0-2.888-.463 5.283 5.283 0 0 0-3.51 1.2 3.252 3.252 0 0 0-.933 2.314 2.916 2.916 0 0 0 .755 2.036 5.886 5.886 0 0 0 2.888 1.527c.489.185 1.333.463 1.333 1.2s-.8 1.25-1.955 1.25a6.349 6.349 0 0 1-2.755-.741l-.133 2.453a10.989 10.989 0 0 0 3.2.509 6.87 6.87 0 0 0 2.044-.278 3.826 3.826 0 0 0 1.777-1.3 3.146 3.146 0 0 0 .667-2.083 3.266 3.266 0 0 0-1.822-2.962c-.311-.139-.756-.278-1.467-.509a8.265 8.265 0 0 1-1.466-.648 1.117 1.117 0 0 1-.222-.6c0-.787.8-1.2 1.955-1.2a8.108 8.108 0 0 1 2.311.417l.222-2.129zM-6159.459-14.739a10.468 10.468 0 0 0 .044-1.157c0-3.055-1.378-5.785-4.932-5.785a4.875 4.875 0 0 0-4 1.851 5.719 5.719 0 0 0-.4.555c0 .046-.222.417-.311.648a7.284 7.284 0 0 0-.578 3.008c0 3.055 1.555 4.674 3.288 5.369a6.714 6.714 0 0 0 2.444.417 8.579 8.579 0 0 0 3.777-.833l-.044-2.545c-.089.046-.489.324-.844.509a5.828 5.828 0 0 1-2.577.6 3.1 3.1 0 0 1-2.888-1.527 3.442 3.442 0 0 1-.4-1.111zm-7.421-2.083a3.479 3.479 0 0 1 .489-1.666 2.406 2.406 0 0 1 2.044-1.111 2.064 2.064 0 0 1 1.778.926 3.286 3.286 0 0 1 .533 1.851zM-6146.818-10.065v-5.831c-.045-2.083-.089-3.286-.711-4.212a4.441 4.441 0 0 0-.933-.972 3.938 3.938 0 0 0-2.177-.555 4.423 4.423 0 0 0-2.311.6 3.518 3.518 0 0 0-1.022.833c-.178.185-.267.324-.444.509v-1.759h-2.733c.045 1.018.089 1.712.089 2.73v8.655h2.844v-5.322c.044-1.342.089-2.545 1.111-3.332a1.937 1.937 0 0 1 1.422-.509 1.779 1.779 0 0 1 .844.185 1.512 1.512 0 0 1 .8.787 5.886 5.886 0 0 1 .355 2.684v5.507zM-6143.796-26.68v16.638h2.666v-1.759c.044.046.222.417.444.694a3.53 3.53 0 0 0 2.977 1.25 4.675 4.675 0 0 0 1.866-.37 4.246 4.246 0 0 0 .889-.648 6.62 6.62 0 0 0 1.822-5.045 6.88 6.88 0 0 0-.355-2.407 4.035 4.035 0 0 0-4.132-3.286 3.813 3.813 0 0 0-2.888 1.2c-.133.185-.267.37-.489.741v-6.988h-2.8zm5.421 7.4a2.209 2.209 0 0 1 1.466.648 3.534 3.534 0 0 1 .444.694 5.171 5.171 0 0 1 .4 2.129 4.262 4.262 0 0 1-1.111 3.1 2.171 2.171 0 0 1-2.933-.231 2.881 2.881 0 0 1-.489-.787 4.363 4.363 0 0 1-.4-1.944c0-1.018.266-2.777 1.733-3.425a3.046 3.046 0 0 1 .888-.185zM-6121.602-10.065c-.044-1.388-.044-2.36-.044-3.7v-2.731a7.94 7.94 0 0 0-.311-2.684 3.673 3.673 0 0 0-.933-1.435 5.1 5.1 0 0 0-3.466-1.064 8.985 8.985 0 0 0-3.732.787l.044 2.545a6.145 6.145 0 0 1 1.111-.694 5.494 5.494 0 0 1 2.222-.509h.311a2.148 2.148 0 0 1 1.777.833 3.293 3.293 0 0 1 .4 1.666c-.533-.046-.933-.046-1.466-.046h-.133c-2.177 0-3.644.417-4.577 1.25a3.2 3.2 0 0 0-1.022 2.453c-.044 1.388.667 3.517 3.688 3.517a4.359 4.359 0 0 0 2.444-.6 3.383 3.383 0 0 0 1.2-1.25v1.666zm-2.577-5.137a7.828 7.828 0 0 1-.044.833 2.661 2.661 0 0 1-2.577 2.5 1.55 1.55 0 0 1-1.733-1.527 1.4 1.4 0 0 1 .178-.74c.489-.879 1.6-1.111 3.11-1.111a8.223 8.223 0 0 1 1.066.046zM-6108.427-10.065v-5.831c-.045-2.083-.045-3.286-.711-4.212a3.031 3.031 0 0 0-.933-.972 3.88 3.88 0 0 0-2.133-.555 4.223 4.223 0 0 0-2.311.6 3.045 3.045 0 0 0-1.066.833 3.986 3.986 0 0 0-.4.509v-1.759h-2.755c.045 1.018.089 1.712.089 2.73v8.655h2.822v-5.322c.044-1.342.133-2.545 1.111-3.332a1.937 1.937 0 0 1 1.422-.509 1.644 1.644 0 0 1 .844.185 1.511 1.511 0 0 1 .8.787 6.422 6.422 0 0 1 .4 2.684v5.507zM-6102.607-26.68h-2.8v16.638h2.8v-5.646c.489.833.844 1.388 1.378 2.175l2.4 3.471h3.51l-4.621-6.387 4.088-5h-3.288l-2.444 3.193c-.4.509-.666.879-1.022 1.388v-9.832zM-6084.366-14.739v-1.157c0-3.055-1.378-5.785-4.888-5.785a4.812 4.812 0 0 0-4 1.851 2.49 2.49 0 0 0-.4.555c-.044.046-.222.417-.356.648a8.309 8.309 0 0 0-.533 3.008c0 3.055 1.511 4.674 3.244 5.369a7.041 7.041 0 0 0 2.488.417 8.2 8.2 0 0 0 3.733-.833v-2.545c-.089.046-.533.324-.845.509a5.827 5.827 0 0 1-2.577.6 3.165 3.165 0 0 1-2.933-1.527 5.2 5.2 0 0 1-.4-1.111zm-7.487-2.083a4.481 4.481 0 0 1 .533-1.666 2.4 2.4 0 0 1 2.044-1.111 2.107 2.107 0 0 1 1.778.926 3.679 3.679 0 0 1 .533 1.851zM-6071.791-10.065v-5.831c-.044-2.083-.044-3.286-.711-4.212a3.031 3.031 0 0 0-.933-.972 3.882 3.882 0 0 0-2.133-.555 4.423 4.423 0 0 0-2.31.6 4.144 4.144 0 0 0-1.066.833c-.134.185-.267.324-.4.509v-1.759h-2.711c.044 1.018.044 1.712.044 2.73v8.655h2.889v-5.322c0-1.342.089-2.545 1.111-3.332a1.846 1.846 0 0 1 1.422-.509 1.579 1.579 0 0 1 .8.185 1.78 1.78 0 0 1 .844.787 5.9 5.9 0 0 1 .354 2.684v5.507z\"/></g></svg>";
            return new FileContentResult(Encoding.UTF8.GetBytes(svg), new Net.Http.Headers.MediaTypeHeaderValue("image/svg+xml"));
        }
    }
}
