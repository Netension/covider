using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Netension.Covider.Application;
using Netension.Covider.Commands;
using Netension.Request.Hosting.LightInject.Builders;
using Serilog;

namespace Netension.Covider.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseLightInject()
                .UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration))
                .ConfigureServices(services =>
                {
                    services.AddMvcCore()
                        .AddApiExplorer();

                    services.AddSwaggerGen(c =>
                    {
                        c.SwaggerDoc("v1", new OpenApiInfo { Title = "Covider API", Version = "v1" });
                    });
                })
                .UseRequesting(builder =>
                {
                    builder.RegistrateCorrelation();
                    builder.RegistrateHandlers<Wireup>();
                    builder.RegistrateValidators<FakeCommand>();

                    builder.RegistrateRequestSenders(builder => builder.RegistrateLoopbackSender(builder => builder.UseCorrelation(), request => true));

                    builder.RegistrateRequestReceivers(register =>
                    {
                        register.RegistrateLoopbackRequestReceiver(builder => builder.UseCorrelation());
                        register.RegistrateHttpRequestReceiver(builder => builder.UseCorrelation());
                    });
                })
                .ConfigureWebHost(builder =>
                {
                    builder.UseKestrel();

                    builder.Configure((context, app)  =>
                    {
                        if (context.HostingEnvironment.IsDevelopment())
                        {
                            app.UseSwagger();
                            app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v1/swagger.json", "Covider API"));
                        }

                        app.UseErrorHandling();

                        app.UseRouting();

                        app.UseEndpoints(builder =>
                        {
                            builder.MapControllers();
                        });
                    });
                });
    }
}
