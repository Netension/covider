using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Moq;
using Netension.Covider.Application.Clients;
using Netension.Covider.Web;

namespace Netension.Covider.Test.Integration.Factory
{
    public class TestApplicationFactory : WebApplicationFactory<Program>
    {
        public Mock<IApplicationRepository> StorageMock { get; } = new Mock<IApplicationRepository>();

        protected override IHostBuilder CreateHostBuilder()
        {
            var builder =  base.CreateHostBuilder();

            builder.ConfigureServices(services =>
            {
                services.AddTransient(provider => StorageMock.Object);
            });

            return builder;
        }
    }
}
