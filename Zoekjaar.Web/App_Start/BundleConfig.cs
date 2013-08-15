using System.Collections.Generic;
using System.Web.Optimization;
using BundleTransformer.Core.Transformers;
using BundleTransformer.Core.Translators;
using BundleTransformer.Less.Translators;
using BundleTransformer.MicrosoftAjax.Minifiers;
using BundleTransformer.TypeScript.Translators;

namespace Zoekjaar.Web
{
	public class BundleConfig
	{
		// For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
		public static void RegisterBundles(BundleCollection bundles, CssTransformer cssTransformer, JsTransformer jsTransformer)
		{
			bundles.Add(new Bundle("~/bundles/jquery", jsTransformer).Include(
						"~/Scripts/jquery-{version}.js"));

			bundles.Add(new Bundle("~/bundles/jqueryui", jsTransformer).Include(
						"~/Scripts/jquery-ui-{version}.js"));

			bundles.Add(new Bundle("~/bundles/jqueryval", jsTransformer).Include(
						"~/Scripts/jquery.validate*"));

			// Use the development version of Modernizr to develop with and learn from. Then, when you're
			// ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
			bundles.Add(new Bundle("~/Content/modernizr", jsTransformer).Include(
						"~/Content/unify/modernizr-*"));

			bundles.Add(new Bundle("~/Content/bootstrap", cssTransformer).Include(
					"~/Content/bootstrap.css",
					"~/Content/bootstrap-responsive.css"
				));

			bundles.Add(new Bundle("~/Content/font-awesome", cssTransformer).Include(
				"~/Content/font-awesome*"));

			bundles.Add(new Bundle("~/Content/unifycss", cssTransformer).Include(
				"~/Content/unify/css/style.css",
				"~/Content/unify/css/headers/header1.css",
				"~/Content/unify/css/style_responsive.css",
				"~/Content/unify/plugins/flexslider.css",
				"~/Content/unify/plugins/parallax-slider/css/parallax-slider.css"//,
				//"~/Content/unify/css/themes/default.css",
				//"~/Content/unify/css/themes/headers/default.css"
				));

			bundles.Add(new Bundle("~/Content/upload", cssTransformer).Include(
						"~/Content/FileUpload/css/jquery.fileupload-ui.css"));

			bundles.Add(new Bundle("~/Content/style", cssTransformer).Include("~/Content/style.css"));

			bundles.Add(new Bundle("~/bundles/bootstrap", jsTransformer).Include("~/Scripts/bootstrap*",
				"~/Scripts/bootstrap-datepicker.js"));
			bundles.Add(new Bundle("~/bundles/ts", jsTransformer).Include("~/Scripts/Utility.ts"));
			bundles.Add(new Bundle("~/bundles/wysiwyg", jsTransformer).Include(
				"~/Scripts/jquery.hotkeys.js",
				"~/Scripts/bootstrap-wysiwyg.js"));

			bundles.Add(new Bundle("~/bundles/unifyjs", jsTransformer).Include(
				"~/Content/unify/plugins/flexslider/jquery.flexslider-min.js",
				"~/Content/unify/plugins/parallax-slider/js/modernizr.js",
				"~/Content/unify/plugins/parallax-slider/js/jquery.cslider.js",
				"~/Content/unify/plugins/back-to-top.js",
				"~/Content/unify/js/app.js",
				"~/Content/unify/plugins/revolution_slider/rs-plugin/js/jquery.themepunch.plugins.min.js",
				"~/Content/unify/plugins/revolution_slider/rs-plugin/js/jquery.themepunch.revolution.min.js"));

			bundles.Add(new Bundle("~/bundles/upload", jsTransformer).Include(
				"~/Scripts/FileUpload/jqueryui/jquery.ui.widget.js",
				"~/Scripts/FileUpload/jquery.iframe-transport.js",
				"~/Scripts/FileUpload/jquery.fileupload.js"));
			BundleTable.EnableOptimizations = true;
		}

		public static void RegisterBundles(BundleCollection bundles)
		{
			var cssTransformer = new CssTransformer(new MicrosoftAjaxCssMinifier(),
													new List<ITranslator> { new LessTranslator(), new NullTranslator() });
			var jsTransformer = new JsTransformer(new MicrosoftAjaxJsMinifier(),
												  new List<ITranslator> { new TypeScriptTranslator(), new NullTranslator() });

			RegisterBundles(bundles, cssTransformer, jsTransformer);
		}
	}
}