using TestMvc;
using Topshelf;

namespace MyTopShelfServiceHost
{
    class Program
    {
        static void Main(string[] args)
        {
            HostFactory.Run(x =>
            {
                x.Service<Program>(s =>
                {
                    s.ConstructUsing(service => new Program());
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
            CustomMvcService.ConfigureAndStart(args);
        }

        public void Stop()
        {

        }
    }
}
