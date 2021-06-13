using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Netension.Covider.Application.Clients;
using Netension.Covider.Application.Options;
using System;
using System.Net.Http.Headers;
using System.Text;

namespace Netension.Covider.Web.Extensions
{
    public static class WireupExtensions
    {
        public static void UseCouchDB(this IServiceCollection services, string section)
        {
            services.AddOptions<CouchDbOptions>()
                .Configure<IConfiguration>((options, configuration) => configuration.GetSection(section).Bind(options))
                .ValidateDataAnnotations();

            services.AddHttpClient<IApplicationRepository, CouchDbApplicationRepository>((provider, client) =>
            {
                var options = provider.GetRequiredService<IOptions<CouchDbOptions>>().Value;
                client.BaseAddress = options.Url;

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes($"{options.UserName}:{options.Password}")));
            });
        }
    }
}
