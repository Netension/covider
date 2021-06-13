using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Netension.Covider.Application.CommandHandlers;
using Netension.Covider.Commands;
using Netension.Covider.Web.Extensions;
using Netension.Request.Hosting.LightInject.Builders;
using Serilog;
using System.Reflection;

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
                        .AddApplicationPart(Assembly.GetExecutingAssembly())
                        .AddApiExplorer();

                    services.AddSwaggerGen(c =>
                    {
                        c.SwaggerDoc("v1", new OpenApiInfo { Title = "Covider API", Version = "v1" });
                    });

                    services.UseCouchDB("Services:CouchDb");
                })
                .UseRequesting(builder =>
                {
                    builder.RegistrateCorrelation();
                    builder.RegistrateHandlers<CreateApplicationCommandHandler>();
                    builder.RegistrateValidators<CreateApplicationCommandValidator>();

                    builder.RegistrateRequestSenders(builder => builder.RegistrateLoopbackSender(builder => builder.UseCorrelation(), request => true));

                    builder.RegistrateRequestReceivers(register =>
                    {
                        register.RegistrateLoopbackRequestReceiver(builder => builder.UseCorrelation());
                        register.RegistrateHttpRequestReceiver(builder => builder.UseCorrelation());
                    });
                })
                .ConfigureWebHostDefaults(builder =>
                {
                    builder.Configure((context, app)  =>
                    {
                        app.UseErrorHandling();

                        if (context.HostingEnvironment.IsDevelopment())
                        {
                            app.UseSwagger();
                            app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v1/swagger.json", "Covider API"));
                        }

                        app.UseRouting();

                        app.UseEndpoints(builder =>
                        {
                            builder.MapControllers();
                        });
                    })
                    .UseKestrel();
                });
    }
}
