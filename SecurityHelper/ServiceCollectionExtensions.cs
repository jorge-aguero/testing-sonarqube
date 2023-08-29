using JWT;
using JWT.Algorithms;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SecurityHelper.Algorithms;
using SecurityHelper.Options;

namespace SecurityHelper
{
    public static class ServiceCollectionExtensions
    {
        private const string OptionsSectionName = "Security";
        private const string ServiceNameKey = "ServiceName";
        public static IServiceCollection AddSecurityDependenciesForAzureFunction(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<Es256AlgorithmFactory>();
            var serviceProvider = services.BuildServiceProvider();

            var es256Factory = serviceProvider.GetService<Es256AlgorithmFactory>();
            services.AddSingleton<IJwtAlgorithm>(_ => es256Factory!.Build());

            services.AddOptions<JwtOptions>()
                .Configure<IConfiguration>((settings, configuration) =>
                {
                    configuration
                    .GetRequiredSection(nameof(JwtOptions)).Bind(settings);
                })
                .ValidateDataAnnotations();
            services.AddOptions<EcdsaOptions>()
                .Configure<IConfiguration>((settings, configuration) =>
                {
                    configuration
                    .GetRequiredSection(nameof(EcdsaOptions)).Bind(settings);
                })
                .ValidateDataAnnotations();

            services.PostConfigure<JwtOptions>(jwt =>
            {
                jwt.Audience = configuration.GetValue<string>(ServiceNameKey);
            });

            services.AddTransient<IJwtDecoder, JwtDecoder>();
            services.AddTransient<IJwtEncoder, JwtEncoder>();
            services.AddTransient<IEcdsaKeyGenerator, EcdsaKeyGenerator>();

            return services;
        }
    }
}