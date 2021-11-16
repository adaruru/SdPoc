using AspNetCore.Proxy;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCore.Proxy.Options;

namespace ProxyHost
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddProxies();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            #region middleware

            //redirect
            //app.RunProxy(proxy => proxy.UseHttp("https://localhost:44391/Ap1Service"));

            #endregion

            app.UseEndpoints(endpoints =>
            {
                //僅判斷Controller為路由
                //endpoints.MapControllers();

                //設定路由模式
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            //app.RunProxy(proxy => proxy
            //  .UseHttp((context, args) =>
            //  {
            //      if (context.Request.Path.StartsWithSegments("/test"))
            //      {
            //          return "https://localhost:44391/Ap1Service";
            //      }
            //      return "https://localhost:44391/Ap1Service";
            //  }));

            app.UseProxies(proxies =>
            {
                // Bare string.
                proxies.Map("Ap1", proxy => proxy.UseHttp("https://localhost:49173/Ap1Service"));
                proxies.Map("Ap2", proxy => proxy.UseHttp("https://localhost:49175/Ap2Service"));

                // Computed to task.
                proxies.Map("api/comments/task/{postId}", proxy => proxy.UseHttp((_, args) => new ValueTask<string>($"https://jsonplaceholder.typicode.com/comments/{args["postId"]}")));

                // Computed to string.
                proxies.Map("api/comments/string/{postId}", proxy => proxy.UseHttp((_, args) => $"https://jsonplaceholder.typicode.com/comments/{args["postId"]}"));

                proxies.Map("/api/v1/{Ap1}", proxy => proxy.UseHttp((context, args) => $"https://jsonplaceholder.typicode.com/comments/{args["postId"]}", builder => builder.WithHttpClientName("myClientName")));
            });


        }
    }
}
