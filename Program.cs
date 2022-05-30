using System;
using System.IO;
using System.Threading;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ApiServer
{
    public class Program
    {
        public static string MainRemoveString = "";
        public static string defultext = string.Empty;
        public static string PlaylistUploadUrl { get; set; }

        static void Main(string[] args)
        {
            string someString = "something";
            char[] stringArray = someString.ToCharArray();
            //var linkTimeLocal = Assembly.GetExecutingAssembly().GetLinkerTime();
            if (args.Length > 0)
            {
                if (args[0] == "--version" || args[0] == "-v")
                {
                    Console.WriteLine($"CLOUD SERVER Version: {System.Reflection.Assembly.GetExecutingAssembly().GetName().Version}");
                    //Console.WriteLine("Build Date: " + linkTimeLocal);
                    Environment.Exit(Environment.ExitCode);
                }
            }
            Console.WriteLine($"CLOUD SERVER Version: {System.Reflection.Assembly.GetExecutingAssembly().GetName().Version}");
            //Console.WriteLine("Build Date: " + linkTimeLocal);
            try
            {
                var config = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                    .AddEnvironmentVariables().Build();
                MainRemoveString = $"{config["CloudServerConfig:CloudServerUrl"]}";
                Console.WriteLine("Version - " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version);
                //Globals.ApiStarted = 1;
                System.Console.WriteLine("Application Running");
                var host = new WebHostBuilder()
                    .UseKestrel()
                    .UseUrls(MainRemoveString)
                    .UseConfiguration(config)
                    .UseContentRoot(Directory.GetCurrentDirectory())
                    .UseIISIntegration()
                    .UseStartup<Startup>()
                    .Build();
                host.Run();
                //Console.ReadLine();
            }
            catch (Exception x)
            {
                Console.WriteLine(x.Message);
                //Log.Write(LogType.Error, "started", x.ExtractError());
                Environment.Exit(Environment.ExitCode);
            }
        }
    }

}
