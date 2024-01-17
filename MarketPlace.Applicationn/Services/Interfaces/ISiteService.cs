using System;
using System.Threading.Tasks;
using MarketPlace.DataLayerr.Entities.Site;

namespace MarketPlace.Applicationn.Services.Interfaces
{
    public interface ISiteService : IAsyncDisposable
    {
        #region site settings

        Task<SiteSetting> GetDefaultSiteSetting();

        #endregion
    }
}
