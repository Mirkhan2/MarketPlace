using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketPlace.App.Utils;
using MarketPlace.Data.Entities.Site;

namespace MarketPlace.App.EntitiExtensions
{
    public static class BannerExtension
    {
        public static string GetBannerMainImageAddress(this SiteBanner banner)
        { 
            return PathExtension.BannerOrigin + banner.ImageName;
        }
    }
}
