using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketPlace.Applicationn.Services.Interfaces;
using MarketPlace.DataLayerr.Entities.Commen.Site;
using MarketPlace.DataLayerr.Repository;

namespace MarketPlace.Applicationn.Services.Implementations
{
    public class SiteService : ISiteService
    {
        #region constructor
        private readonly IGenericRepository<SiteSetting> _siteSettingRepository;
        public SiteService(IGenericRepository<SiteSetting> siteSetingRepository)
        {
            _siteSettingRepository = siteSetingRepository;
        }
        #endregion
        #region dispose
        public async ValueTask DisposeAsync()
        {
           await _siteSettingRepository.DisposeAsync();
        }
        #endregion
    }
}
