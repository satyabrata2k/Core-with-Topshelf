using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Siemens.Backend.Core.ConfigurationRepository;
using System;
using System.IO;
using Topshelf;

namespace TestMvc
{
    public class CustomMvcService
    {
        private Bootstrapper bootstrapper;
        public static void Main(string[] args)
        {
            HostFactory.Run(x =>
            {
                x.Service<CustomMvcService>(s =>
                {
                    s.ConstructUsing(service => new CustomMvcService());
                    s.WhenStarted(p => p.Start(args));
                    s.WhenStopped(p => p.Stop());
                })
                .RunAsLocalSystem()
                .StartManually()
                .DependsOnMsSql();
            });            
        }

        public CustomMvcService()
        {
            bootstrapper = new Bootstrapper();
            var repo = bootstrapper.Resolve<ISiteRepository>();
            try
            {
                Console.WriteLine("Getting Site Details...");
                var site = repo.GetSiteSimple();
                Console.WriteLine("Site Name :" + site.Name);
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error----------------------------------------------->" + ex.Message);
            }
        }

        public void Start(string[] args)
        {
            Console.WriteLine("Starting Custom MVC service-------->");
            BuildWebHost(args).Run();
        }

        public void Stop()
        {
            bootstrapper.Dispose();
        }

        public static void ConfigureAndStart(string[] args)
        {
            BuildWebHost(args).Run();
        }

        //@"D:\CodeRepos\TestProjs\AspNetCore2MVCIntro\MVCIntro\TestMvc"
        public static IWebHost BuildWebHost(string[] args) =>

            WebHost.CreateDefaultBuilder(args)
                .UseKestrel()
                .UseUrls("http://localhost:53354/")
                .UseContentRoot(@"D:\CodeRepos\TestProjs\AspNetCore2MVCIntro\MVCIntro\TestMvc")
                .UseStartup<Startup>()
                .Build();
    }
}
