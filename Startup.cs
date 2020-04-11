using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ReceitasDeSucesso.Client;

namespace ReceitasDeSucesso
{
    public class Startup
    {

        private IConfigurationRoot Config;
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(option => option.EnableEndpointRouting = false);
            services.AddHttpClient<IReceitaClient, ReceitaClient>(client =>
            {
                client.BaseAddress = new Uri(Config["URLAPI"]);
            });


            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            Config = builder.Build();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.EnvironmentName.Equals("Development"))
            {
                app.UseDeveloperExceptionPage();
                
            }

            app.UseStaticFiles();
            app.UseMvc(
                routes =>
                {
                    routes.MapRoute(
                    name: "RotaHome",
                    template: "{controller=Home}/{action=Index}/");

                    routes.MapRoute(
                    name: "RotaReceita",
                    template: "{controller=Receita}/{action=Index}/");

                    routes.MapRoute(
                        name:"CadastroReceita",
                        template:"{controller=Receita}/{action=CadastroReceita}/");
                }
                );
        }
    }
}