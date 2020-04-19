using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using System;

namespace ReceitasDeSucesso
{
    class Program
    {
        static void Main(string[] args)
        {
            BuilderWebHost(args).Run();
        }

        public static IWebHost BuilderWebHost(string[] args )
        {
            return WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
        }
    }
}
