using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace RepositoryPattern.Service.UI
{
    public static class LayoutExtention
    {
        public static void AddCssFileParts(this HtmlHelper html, params string[] parts)
        {
            AddCssFileParts(html, ResourceLocation.Head, false, parts);
        }
        public static void AddCssFileParts(this HtmlHelper html, ResourceLocation location, params string[] parts)
        {
            AddCssFileParts(html, location, false, parts);
        }
        public static void AddCssFileParts(this HtmlHelper html, bool excludeFromBundling, params string[] parts)
        {
            AddCssFileParts(html, ResourceLocation.Head, excludeFromBundling, parts);
        }

        public static void AddCssFileParts(this HtmlHelper html, ResourceLocation location, bool excludeFromBundling, params string[] parts)
        {
            //var pageAssetsBuilder = EngineContext.Current.Resolve<IPageAssetsBuilder>();
           // pageAssetsBuilder.AddCssFileParts(location, excludeFromBundling, parts);

        }

        public static void AddScriptParts(this HtmlHelper html, params string[] parts)
        {
            AddScriptParts(html, ResourceLocation.Foot, false, parts);
        }

        public static void AddScriptParts(this HtmlHelper html, ResourceLocation location, params string[] parts)
        {
            AddScriptParts(html, location, false, parts);
        }

        public static void AddScriptParts(this HtmlHelper html, bool excludeFromBundling, params string[] parts)
        {
            AddScriptParts(html, ResourceLocation.Foot, excludeFromBundling, parts);
        }

        public static void AddScriptParts(this HtmlHelper html, ResourceLocation location, bool excludeFromBundling, params string[] parts)
        {
            //var pageAssetsBuilder = EngineContext.Current.Resolve<IPageAssetsBuilder>();
            //pageAssetsBuilder.AddScriptParts(location, excludeFromBundling, parts);
        }
    }
}
