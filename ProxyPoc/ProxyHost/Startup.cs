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
using Microsoft.AspNetCore.Http;
using ProxyHost.Middleware;
using Microsoft.AspNetCore.HttpOverrides;
using ProxyHost.Model;

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
            services.AddControllers(options => options.EnableEndpointRouting = false);

            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders =
                    ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            });
            services.AddProxies();//solution 2 third party AspNetCore.Proxy
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            // Endpoint aware middleware. 
            // Middleware can use metadata from the matched endpoint.
            app.UseAuthentication();
            app.UseAuthorization();

            #region middleware template
            //app.UseMiddleware<ApRouterMiddleware>();
            //app.UseMiddleware<TestMiddleware>();

            //app.Use(async (context, next) =>
            //{
            //    await context.Response.WriteAsync($"{nameof(ProxyMiddleware)} in 1. \r\n");
            //    await next.Invoke();
            //    await context.Response.WriteAsync($"{nameof(ProxyMiddleware)} out 4. \r\n");
            //});
            //app.Use(async (context, next) =>
            //{
            //    await context.Response.WriteAsync($"{nameof(ProxyMiddleware)} in 2. \r\n");
            //    await next.Invoke();
            //    await context.Response.WriteAsync($"{nameof(ProxyMiddleware)} out 3. \r\n");
            //});
            #endregion

            #region Core.Proxy
            //url rewirte 與 UseProxies 須擇一使用
            //app.RunProxy(proxy => proxy.UseHttp("https://localhost:49157/Ap1Service"));

            //url rewirte on condition 與 UseProxies 須擇一使用
            //app.RunProxy(proxy => proxy
            //  .UseHttp((context, args) =>
            //  {
            //      return "https://localhost:49157/Ap1Service";
            //      if (context.Request.Path.StartsWithSegments("/Ap1"))
            //      {
            //          return "https://localhost:49157/Ap1Service";
            //      }
            //      else
            //      {
            //          return "https://localhost:49159/Ap2Service";
            //      }

            //  }));

            //MVC route ( services.AddControllers(options => options.EnableEndpointRouting = false);)
            //app.UseMvc(routes =>
            //{
            //    routes.MapRoute(name: "default", template: "{controller=Home}/{action=Index}/{id?}");
            //});
            //等同於
            app.UseMvcWithDefaultRoute(); //用於 proxyHost 自己的 api

            app.UseProxies(proxies =>
            {
                // Bare string path mapping
                //proxies.Map("Ap1", proxy => proxy.UseHttp("https://localhost:49157/Ap1Service"));
                //proxies.Map("Ap2", proxy => proxy.UseHttp("https://localhost:49159/Ap2Service"));

                //Request Header mapping
                proxies.Map(proxy => proxy
                       .UseHttp((context, args) =>
                       {
                           var serviceLine = context.Request.Headers["ServiceLine"];

                           //test route config
                           var proxyConfig = new ProxyConfig();
                           proxyConfig.RouteSetting = new List<RouteSetting>() {
                                                          new RouteSetting() {
                                                              ClientName ="Ap1",
                                                              ServiceLine="Ap1",
                                                              BusinesNo="Ap1",
                                                              TargetUri = "https://localhost:49157/Ap1Service"
                                                          },
                                                          new RouteSetting() {
                                                              ClientName ="Ap2",
                                                              ServiceLine="Ap2",
                                                              BusinesNo="Ap2",
                                                              TargetUri = "https://localhost:49159/Ap2Service"
                                                          }
                                                      };

                           var uri = proxyConfig.RouteSetting.FirstOrDefault(c => c.ServiceLine == serviceLine)?.TargetUri ?? "";

                           if (context.Request.Path.StartsWithSegments("", out var remainingPath))
                           {
                               uri = uri + remainingPath;
                           }
                           return uri;
                       }));

                //Manipulate response
                //proxies.Map("ResponseTest", proxy => proxy
                //       .UseHttp((context, _) =>
                //       {
                //           context.Response.WriteAsync($"{nameof(ProxyMiddleware)} in user Proxy. \r\n");
                //           return "https://localhost:49157/Ap1Service";
                //       }));

                // Computed to task.
                proxies.Map("api/comments/task/{postId}", proxy => proxy.UseHttp((_, args) => new ValueTask<string>($"https://jsonplaceholder.typicode.com/comments/{args["postId"]}")));

                // Computed to string.
                proxies.Map("api/comments/string/{postId}", proxy => proxy.UseHttp((_, args) => $"https://jsonplaceholder.typicode.com/comments/{args["postId"]}"));

                proxies.Map("/api/v1/{Ap1}", proxy => proxy.UseHttp((context, args) => $"https://jsonplaceholder.typicode.com/comments/{args["postId"]}", builder => builder.WithHttpClientName("myClientName")));
            });
            #endregion

            //app.UseEndpoints(endpoints =>
            //{
            //    //僅判斷Controller為路由
            //    //endpoints.MapControllers();

            //    //設定路由模式
            //    endpoints.MapControllerRoute(
            //        name: "default",
            //        pattern: "{controller=Home}/{action=Index}/{id?}");
            //});

        }
    }
}
