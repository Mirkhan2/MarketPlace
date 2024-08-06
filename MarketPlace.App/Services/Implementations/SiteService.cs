﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketPlace.App.Services.Interfaces;
using MarketPlace.Data.Entities.Site;
using MarketPlace.Data.Repository;
using Microsoft.EntityFrameworkCore;

namespace MarketPlace.App.Services.Implementations
{
    public class SiteService : ISiteService
    {
		#region constructor

		private readonly IGenericRepository<SiteSetting> _siteSettingRepository;
		private readonly IGenericRepository<Slider> _sliderRepository;
		private readonly IGenericRepository<SiteBanner> _bannerRepository;
		public SiteService(IGenericRepository<SiteSetting> siteSettingRepository, IGenericRepository<Slider> sliderRepository)
		{
			_siteSettingRepository = siteSettingRepository;
			_sliderRepository = sliderRepository;
		}

		#endregion

		#region site settings

		public async Task<SiteSetting> GetDefaultSiteSetting()
		{
			return await _siteSettingRepository.GetQuery().AsQueryable()
				.SingleOrDefaultAsync(s => s.IsDefault && !s.IsDelete);
		}

		#endregion

		#region slider

		public async Task<List<Slider>> GetAllActiveSliders()
		{
			return await _sliderRepository.GetQuery().AsQueryable()
				.Where(s => s.IsActive && !s.IsDelete).ToListAsync();
		}
		public async Task<List<SiteBanner>> GetSiteBannersByPlacement(List<BannerPlacement> placements)
        {
			return await _bannerRepository.GetQuery().AsQueryable()
				.Where(s => placements.Any(f => f== s.BannerPlacement)).ToListAsync();
           
        }

        #endregion

        public Task<EmailSetting> GetDefaultEmail()
        {

            //return await _context.EmailSettings.FirstOrDefaultAsync(s => !s.IsDelete && s.IsDefault);
            throw new NotImplementedException();
        }
        #region dispose

        public async ValueTask DisposeAsync()
		{
			if (_siteSettingRepository != null) await _siteSettingRepository.DisposeAsync();
			if (_sliderRepository != null) await _sliderRepository.DisposeAsync();
			if (_bannerRepository != null) await _bannerRepository.DisposeAsync();
			{

			}
		}

      



        #endregion
    }
}
