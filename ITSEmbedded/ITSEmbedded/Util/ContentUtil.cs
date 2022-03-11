using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Optimization;

namespace ITSEmbedded.Util
{
    public static class ContentUtil
    {
        public static MvcHtmlString NewTextBox(this HtmlHelper html, string name)
        {
            //var js = Scripts.Render("~/ITSEmbedded/js").ToString();
            //var css = Styles.Render("~/ITSEmbedded/css").ToString();
            //var textbox = html.TextBox(name).ToString();
            //return MvcHtmlString.Create(textbox + js + css);


            var js = ScriptUtil.GetScript();
            var textbox = html.TextBox(name).ToString();
            return MvcHtmlString.Create(textbox+js); ;
        }
    }

}
