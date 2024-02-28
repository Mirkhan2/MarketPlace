﻿using System.Linq;
using MarketPlace.Data.Entities.Account;
using MarketPlace.Data.Entities.Contacts;
using MarketPlace.Data.Entities.Site;
using Microsoft.EntityFrameworkCore;

namespace MarketPlace.Data.Context
{

    public class MarketPlaceDbContext : DbContext
	{
		public MarketPlaceDbContext(DbContextOptions<MarketPlaceDbContext> options) : base(options) { }

		#region account

		public DbSet<User> Users { get; set; }
      
        #endregion
        #region site

        public DbSet<SiteSetting> SiteSettings { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<SiteBanner> SiteBanners { get; set; }

        #endregion
        #region contacts
        public DbSet<ContactUs> ContactUs { get; set; }
        #endregion

        #region on model creating

        protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(s => s.GetForeignKeys()))
			{
				relationship.DeleteBehavior = DeleteBehavior.Cascade;
			}

			base.OnModelCreating(modelBuilder);
		}

		#endregion
	}
}