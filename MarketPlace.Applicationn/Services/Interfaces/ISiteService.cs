using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MarketPlace.DataLayerr.Entities.Site;

namespace MarketPlace.Applicationn.Services.Interfaces
{
    public interface ISiteService : IAsyncDisposable
    {
		#region site settings

		Task<SiteSetting> GetDefaultSiteSetting();

		#endregion

		#region slider

		Task<List<Slider>> GetAllActiveSliders();

        #endregion
        #region site banners
        Task<List<SiteBanner>> GetSiteBannersByPlacement(List<BannerPlacement> placements);
        #endregion

    }
}
