using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.App.Utils
{
    public static class PathExtension
    {
        #region default image

        public static string DefaultAvatar = "/img/defaults/avatar.jpg";

        #endregion
        #region user avatar

        public static string UserAvatarOrigin = "/Content/Images/UserAvatar/origin/";
        public static string UserAvatarOriginServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Content/Images/UserAvatar/origin/");

        public static string UserAvatarThumb = "/Content/Images/UserAvatar/Thumb/";
        public static string UserAvatarThumbServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Content/Images/UserAvatar/Thumb/");


        #endregion
      
        #region slider

        public static string SliderOrigin = "/img/slider/";

        #endregion

        #region banner

        public static string BannerOrgin = "/img/bg/";

        #endregion
    }
}
