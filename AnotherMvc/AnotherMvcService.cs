using System;
using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Topshelf;

namespace AnotherMvc
{
    public class AnotherMvcService
    {
        public static void Main(string[] args)
        {
            HostFactory.Run(x =>
            {
                x.Service<AnotherMvcService>(s =>
                {
                    s.ConstructUsing(service => new AnotherMvcService());
                    s.WhenStarted(p => p.Start(args));
                    s.WhenStopped(p => p.Stop());
                })
                .RunAsLocalSystem()
                .StartManually()
                .DependsOnMsSql();
            });
        }

        public void Start(string[] args)
        {
            Console.WriteLine("Starting Another MVC service---------------------->");
            BuildWebHost(args).Run();
        }

        public void Stop()
        {

        }
                
        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseKestrel()
                .UseUrls("http://localhost:33168/")
                .UseContentRoot(@"D:\CodeRepos\TestProjs\AspNetCore2MVCIntro\MVCIntro\AnotherMvc")
                .UseStartup<Startup>()
                .Build();
    }
}
