using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using SecurityHelper.Jwt;

namespace JmfFunctions.Functions
{
    public class GenerateJwtToken
    {
        private readonly ILogger<GenerateJwtToken> logger;
        private readonly IConfiguration configuration;
        private readonly IJwtEncoder jwtHelper;

        public GenerateJwtToken(ILogger<GenerateJwtToken> logger, IConfiguration configuration, IJwtEncoder jwtHelper)
        {
            this.logger = logger;
            this.configuration = configuration;
            this.jwtHelper = jwtHelper;
        }

        [FunctionName("GenerateJwtToken")]
        [OpenApiOperation(operationId: "GenerateJwtToken", tags: new[] { "Security" })]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "The OK response")]
        public HttpResponseMessage Run(
#pragma warning disable RCS1163 // Unused parameter.
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "security/jwt/generate")] HttpRequest req)
#pragma warning restore RCS1163 // Unused parameter.
        {
            logger.LogInformation("C# HTTP trigger function processed a request.");

            var token = jwtHelper.Encode(configuration["ServiceName"], 300);
            return new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(token)
            };
        }
    }
}
