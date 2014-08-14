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
            var pageAssetsBuilder = EngineContext.Current.Resolve<IPageAssetsBuilder>();
            pageAssetsBuilder.AddCssFileParts(location, excludeFromBundling, parts);
        }

        public static void AppendCssFileParts(this HtmlHelper html, params string[] parts)
        {
            AppendCssFileParts(html, ResourceLocation.Head, false, parts);
        }

        public static void AppendCssFileParts(this HtmlHelper html, ResourceLocation location, params string[] parts)
        {
            AppendCssFileParts(html, location, false, parts);
        }

        public static void AppendCssFileParts(this HtmlHelper html, bool excludeFromBundling, params string[] parts)
        {
            AppendCssFileParts(html, ResourceLocation.Head, excludeFromBundling, parts);
        }

        //public static void AppendCssFileParts(this HtmlHelper html, ResourceLocation location, bool excludeFromBundling, params string[] parts)
        //{
        //    var pageAssetsBuilder = EngineContext.Current.Resolve<IPageAssetsBuilder>();
        //    pageAssetsBuilder.AppendCssFileParts(location, excludeFromBundling, parts);
        //}

        //public static MvcHtmlString SmartCssFiles(this HtmlHelper html, UrlHelper urlHelper, ResourceLocation location, bool? enableBundling = null)
        //{
        //    var pageAssetsBuilder = EngineContext.Current.Resolve<IPageAssetsBuilder>();
        //    return MvcHtmlString.Create(pageAssetsBuilder.GenerateCssFiles(urlHelper, location, enableBundling));
        //}
    }
}
