using JmfFunctions.Infrastructure;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using SecurityHelper;

[assembly: FunctionsStartup(typeof(Startup))]
namespace JmfFunctions.Infrastructure;

public class Startup : FunctionsStartup
{
    public override void Configure(IFunctionsHostBuilder builder)
    {
        builder.Services.AddSecurityDependenciesForAzureFunction(builder.GetContext().Configuration);
    }
}
