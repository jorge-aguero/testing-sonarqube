using System.ComponentModel.DataAnnotations;
using JWT.Algorithms;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JmfJwtHelper
{
    public static class ServiceCollectionExtensions
    {
        private const string OptionsSectionName = "JmfJwtHelper";
        private const string ServiceNameKey = "ServiceName";
        public static IServiceCollection AddSecurityDependenciesForAzureFunction(this IServiceCollection services)
        {
            services.AddSingleton<Es256AlgorithmFactory>();
            var serviceProvider = services.BuildServiceProvider();

            var es256Factory = serviceProvider.GetService<Es256AlgorithmFactory>();
            services.AddSingleton<IJwtAlgorithm>(_ => es256Factory!.Build());
                        
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();            
            var configs = configuration.GetRequiredSection(OptionsSectionName);
            services.AddOptions<JwtOptions>()
                .Bind(configs.GetRequiredSection(JwtOptions.SectionName))
                .ValidateDataAnnotations();
            services.AddOptions<EcdsaOptions>()
                .Bind(configs.GetRequiredSection(EcdsaOptions.SectionName))
                .ValidateDataAnnotations();
            
            services.PostConfigure<JwtOptions>(o =>
            {
                o.Audience = configuration.GetValue<string>(ServiceNameKey);
            });

            services.AddTransient<IJwtHelper, JwtHelper>();
            services.AddTransient<IEcdsaKeyGenerator, EcdsaKeyGenerator>();

            return services;
        }

    }

    public class JwtOptions
    {
        public const string SectionName = "Jwt";

        [Required]
        public string Secret { get; set; } = null!;
        public string Audience { get; set; } = null!;
        [Range(0, 30000)]
        public int TimeoutInSeconds { get; set; } = 60;
    }

    public class EcdsaOptions
    {
        public const string SectionName = "Ecdsa";

        [Required]
        public string PublicKey { get; set; } = null!;
        [Required]
        public string PrivateKey { get; set; } = null!;
    }
}
