using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MarketPlace.Data.Entities.Site;

namespace MarketPlace.App.Services.Interfaces
{
    public interface ISiteService : IAsyncDisposable
    {
        #region site settings

        Task<SiteSetting> GetDefaultSiteSetting();

        #endregion

        #region Email
        Task<EmailSetting?> GetDefaultEmail();
        #endregion

        #region slider

        Task<List<Slider>> GetAllActiveSliders();

        #endregion

        #region site banners
        Task<List<SiteBanner>> GetSiteBannersByPlacement(List<BannerPlacement> placements);
        #endregion

    }
}
