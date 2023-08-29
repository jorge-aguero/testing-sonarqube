using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using SecurityHelper.Algorithms;

namespace JmfFunctions.Functions
{
    public class GenerateEncodingKeys
    {
        private readonly ILogger<GenerateEncodingKeys> logger;
        private readonly IEcdsaKeyGenerator ecdsaKeyGenerator;

        public GenerateEncodingKeys(ILogger<GenerateEncodingKeys> logger, IEcdsaKeyGenerator ecdsaKeyGenerator)
        {
            this.logger = logger;
            this.ecdsaKeyGenerator = ecdsaKeyGenerator;
        }

        [FunctionName("GenerateEncodingKeys")]
        [OpenApiOperation(operationId: "GenerateEncodingKeys", tags: new[] { "Security" })]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(KeysResponse), Description = "The OK response")]
        public HttpResponseMessage Run(
#pragma warning disable RCS1163 // Unused parameter.
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "security/ecdsa/generate")] HttpRequest req)
#pragma warning restore RCS1163 // Unused parameter.
        {
            logger.LogInformation("C# HTTP trigger function processed a request.");

            ecdsaKeyGenerator.GenerateEcdsaKeys(out var privateKey, out var publicKey);

            return new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = JsonContent.Create(new KeysResponse
                {
                    PublicKey = publicKey,
                    PrivateKey = privateKey
                })
            };
        }

        internal class KeysResponse
        {
            public string PublicKey { get; set; }
            public string PrivateKey { get; set; }
        }
    }
}

