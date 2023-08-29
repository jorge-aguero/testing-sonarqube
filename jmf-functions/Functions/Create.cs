using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Amazon.Runtime.Internal.Transform;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;
using SecurityHelper.Jwt;

namespace JmfFunctions.Functions
{
    public class Create
    {
        private readonly ILogger<Create> logger;
        private readonly IJwtDecoder jwtDecoder;

        public Create(ILogger<Create> logger, IJwtDecoder jwtDecoder)
        {
            this.logger = logger;
            this.jwtDecoder = jwtDecoder;
        }

        [FunctionName(nameof(Create))]
        [OpenApiOperation(operationId: "Run", tags: new[] { "name" })]
        [OpenApiSecurity("Authorization", SecuritySchemeType.Http, BearerFormat = "JWT", Scheme = OpenApiSecuritySchemeType.Bearer, Name = "Authorization", In = OpenApiSecurityLocationType.Header)]
        [OpenApiParameter(name: "name", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "The **Name** parameter")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "The OK response")]
        public HttpResponseMessage Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequestMessage req)
        {
            logger.LogInformation("C# HTTP trigger function processed a request.");
            
            string name = QueryHelpers.ParseQuery(req.RequestUri.Query)["name"];

            const string connectionUri = "mongodb+srv://admin:fBq0IwsuUn0JMPH6@cluster0.09kl5t5.mongodb.net/?retryWrites=true&w=majority";
            var settings = MongoClientSettings.FromConnectionString(connectionUri);
            // Set the ServerApi field of the settings object to Stable API version 1
            settings.ServerApi = new ServerApi(ServerApiVersion.V1);
            // Create a new client and connect to the server
            var client = new MongoClient(settings);
            // Send a ping to confirm a successful connection
            try
            {
                
                var token = req.Headers.Authorization.Parameter;
                var tokenDecodeResult = jwtDecoder.Decode(token);
                var result = client.GetDatabase("sample_airbnb").RunCommand<BsonDocument>(new BsonDocument("ping", 1));
                Console.WriteLine("Pinged your deployment. You successfully connected to MongoDB!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            string responseMessage = string.IsNullOrEmpty(name)
                ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
                : $"Hello, {name}. This HTTP triggered function executed successfully.";

            return new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(responseMessage)
            };
        }
    }
}
