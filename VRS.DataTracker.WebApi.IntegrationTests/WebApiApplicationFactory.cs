using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;

namespace VRS.DataTracker.WebApi.IntegrationTests
{
    internal class WebApiApplicationFactory : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseSolutionRelativeContentRoot(AppContext.BaseDirectory);
            builder.UseEnvironment("Test");
        }
    }
}