using System.Web.Optimization;

namespace Zoekjaar.Web.Extensions
{
    public static class BundleExtensions
    {
        public static Bundle AddTransform(this Bundle bundle, IBundleTransform transform)
        {
            bundle.Transforms.Add(transform);
            return bundle;
        }
    }
}