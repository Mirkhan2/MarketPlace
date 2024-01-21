using MarketPlace.Applicationn.Utils;
using MarketPlace.DataLayerr.Entities.Site;

namespace MarketPlace.Applicationn.EntitiesExtensions
{
    public static class BannerExtension
    {
        public static string GetBannerMainImageAddress(this SiteBanner banner)
        {
            return PathExtension.BannerOrgin + banner.ImageName;
        }
    }
}
