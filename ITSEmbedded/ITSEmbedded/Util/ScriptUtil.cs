using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITSEmbedded.Util
{
    public class ScriptUtil
    {
        public static string GetScript()
        {
            Stream stream0 = SystemReflectionUtil.GetAssemblyResourceStream(SystemReflectionUtil.ITSHtmlTemplateNameSpace + ".Scripts.test.js");
            string htmlString = "";
            using (StreamReader textStreamReader = new StreamReader(stream0))
            {
                htmlString += textStreamReader.ReadToEnd();
            }
            return htmlString;
        }
    }
}
