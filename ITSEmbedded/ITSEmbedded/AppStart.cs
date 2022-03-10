using System;
using ITSEmbedded;
using System.Web.Routing;
using System.Web.Optimization;
using System.Web.Hosting;

[assembly: WebActivatorEx.PostApplicationStartMethod(typeof(AppStart), "Start")]

namespace ITSEmbedded
{
    public static class AppStart
    {
        public static void Start()
        {
            RegisterRoutes();
            RegisterBundles();
        }

        private static void RegisterRoutes()
        {
            RouteTable.Routes.Insert(0,
                new Route("ITSEmbedded/{file}.{extension}",
                    new RouteValueDictionary(new { }),
                    new RouteValueDictionary(new { extension = "css|js" }),
                    new EmbeddedResourceRouteHandler()
                ));
        }
        private static void RegisterBundles()
        {
            BundleTable.VirtualPathProvider = new EmbeddedVirtualPathProvider(HostingEnvironment.VirtualPathProvider);

            BundleTable.Bundles.Add(new ScriptBundle("~/ITSEmbedded/js")
                .Include("~/ITSEmbedded/test.js",
                         "~/ITSEmbedded/bootstrap-datepicker.js"));

            BundleTable.Bundles.Add(new StyleBundle("~/ITSEmbedded/css")
                .Include("~/ITSEmbedded/test.css"));


            //Stream stream = SystemReflectionUtil.GetAssemblyResourceStream(SystemReflectionUtil.ITSHtmlTemplateNameSpace + ".Scripts.test.js");
            //using (StreamReader textStreamReader = new StreamReader(stream))
            //{
            //    htmlString += textStreamReader.ReadToEnd();
            //}
        }


    }
}