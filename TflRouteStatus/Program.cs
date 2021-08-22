using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TflRouteStatus.Action;
using TflRouteStatus.Interfaces;

namespace TflRouteStatus
{
    /// <summary>
    /// Program Class - Execution starts here
    /// </summary>
    class Program
    {
        /// <summary>
        /// Main method
        /// </summary>
        /// <param name="args"></param>
        static async Task Main(string[] args)
        {
            if (args.Length == 0 || args.Length > 1)
            {
                Console.WriteLine(Resources.EmptyRouteErrorMsg);
            }
            else
            {
                try
                {
                    var route = args[0];

                    using IHost host = CreateHostBuilder(args).Build();
                    using IServiceScope serviceScope = host.Services.CreateScope();
                    IServiceProvider provider = serviceScope.ServiceProvider;

                    var routeStatus = provider.GetRequiredService<IRouteStatus>();

                    //add config file
                    IConfiguration Config = new ConfigurationBuilder()
                                                .AddJsonFile(Common.CONFIG_FILE_NAME)
                                                .Build();

                    var url = Config.GetSection(Common.ROUTE_API_URL).Value;
                    var app_Id = Config.GetSection(Common.APP_INFO)[Common.APP_ID];
                    var app_Key = Config.GetSection(Common.APP_INFO)[Common.APP_KEY];

                    if (routeStatus.ValidateInputs(url, app_Id, app_Key))
                    {
                        var result = await routeStatus.FindRouteStatus(url, app_Id, app_Key, route);
                        Console.WriteLine(result);
                    }
                    else
                    {
                        Console.WriteLine(Resources.ValidationFailedMsg);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(string.Format(Resources.ExceptionMsg, ex.Message));
                }
            }
        }

        /// <summary>
        /// Create Host Builder and set up dependency injection
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        static IHostBuilder CreateHostBuilder(string[] args) =>
           Host.CreateDefaultBuilder(args)
               .ConfigureServices((_, services) =>
                   services.AddScoped<ICallAPI, CallAPI>()
                           .AddScoped<IRouteStatus, RouteStatus>()
                           );
    }
}
