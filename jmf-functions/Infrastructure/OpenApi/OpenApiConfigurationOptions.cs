using System;
using System.Collections.Generic;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Abstractions;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.OpenApi.Models;

namespace JmfFunctions.Infrastructure.OpenApi
{
    public sealed class OpenApiConfigurationOptions : IOpenApiConfigurationOptions
    {
        public OpenApiInfo Info { get; set; } = new OpenApiInfo
        {
            Title = "JMF Testing Functions Service",
            Version = "1.0.0",
            Description = "Azure Functions made for testing purposses"
        };

        public List<OpenApiServer> Servers { get; set; } = new();

        public OpenApiVersionType OpenApiVersion { get; set; } = OpenApiVersionType.V3;

        public bool IncludeRequestingHostName { get; set; } = false;

        public bool ForceHttp { get; set; } = !string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("LocalDevelopment"));

        public bool ForceHttps { get; set; } = string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("LocalDevelopment"));

        public List<IDocumentFilter> DocumentFilters { get; set; } = new List<IDocumentFilter>();
    }
}
