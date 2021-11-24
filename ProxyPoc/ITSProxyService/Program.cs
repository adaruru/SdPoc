using AspNetCore.Proxy;
using ProxyHost.Model;



var builder = WebApplication.CreateBuilder(args);
builder.Configuration.SetBasePath(builder.Environment.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
                .AddJsonFile($"Config/ProxySetting.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();

var Env = builder.Environment;
var Configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddControllers(options => options.EnableEndpointRouting = false);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers(options => options.EnableEndpointRouting = false);
builder.Services.Configure<ProxySetting>(options => builder.Configuration.GetSection("ProxySetting").Bind(options));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
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
//app.RunProxy(proxy => proxy.UseHttp("https://localhost:49163/Ap1Service"));

//url rewirte on condition 與 UseProxies 須擇一使用
//app.RunProxy(proxy => proxy
//  .UseHttp((context, args) =>
//  {
//      return "https://localhost:49163/Ap1Service";
//      if (context.Request.Path.StartsWithSegments("/Ap1"))
//      {
//          return "https://localhost:49163/Ap1Service";
//      }
//      else
//      {
//          return "https://localhost:49165/Ap2Service";
//      }

//  }));

//MVC route ( services.AddControllers(options => options.EnableEndpointRouting = false);)
//app.UseMvc(routes =>
//{
//    routes.MapRoute(name: "default", template: "{controller=Home}/{action=Index}/{id?}");
//});
//等同於
//app.MapControllers(); //Todo 查詢差異
app.UseMvcWithDefaultRoute(); //用於 proxyHost 自己的 api

app.UseProxies(proxies =>
{
    //Bare string path mapping
    //path header 同時設定 以前行規則優先
    proxies.Map("Ap1", proxy => proxy.UseHttp("https://localhost:49163/Ap1Service"));
    //proxies.Map("Ap2", proxy => proxy.UseHttp("https://localhost:49165/Ap2Service"));

    //Request Header mapping
    proxies.Map(proxy => proxy
           .UseHttp((context, args) =>
           {
               var serviceLine = context.Request.Headers["ServiceLine"];
               var section = Configuration.GetSection(nameof(ProxySetting));
               var proxySettings = section.Get<ProxySetting>();
               var uri = proxySettings.RouteSettings.FirstOrDefault(c => c.ServiceLine == serviceLine)?.TargetUri ?? "";
               if (context.Request.Path.StartsWithSegments("", out var remainingPath))
               {
                   uri = uri + remainingPath;
               }
               context.Request.Headers["ServiceLine"] = "Ap1";
               return uri;
           }));

    //Manipulate response
    //proxies.Map("ResponseTest", proxy => proxy
    //       .UseHttp((context, _) =>
    //       {
    //           context.Response.WriteAsync($"{nameof(ProxyMiddleware)} in user Proxy. \r\n");
    //           return "https://localhost:49163/Ap1Service";
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

app.Run();
