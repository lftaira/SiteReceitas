using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace ReceitasDeSucesso
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(option => option.EnableEndpointRouting = false);
            
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