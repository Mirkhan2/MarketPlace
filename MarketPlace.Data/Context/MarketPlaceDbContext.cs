﻿using System.Linq;
using MarketPlace.Data.DTO.Products;
using MarketPlace.Data.Entities.Account;
using MarketPlace.Data.Entities.Contacts;
using MarketPlace.Data.Entities.ProductOrder;
using MarketPlace.Data.Entities.Products;
using MarketPlace.Data.Entities.Site;
using MarketPlace.Data.Entities.Store;
using MarketPlace.Data.Entities.Wallet;
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

        public DbSet<ContactUs> ContactUses { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<TicketMessage> TicketMessages { get; set; }

        #endregion

        #region store

        public DbSet<Seller> Sellers { get; set; }

        #endregion

        #region products

        public DbSet<ProductCategory> ProductCategories { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<ProductGallery> ProductGalleries { get; set; }

        public DbSet<ProductSelectedCategory> ProductSelectedCategories { get; set; }

        public DbSet<ProductColor> ProductColors { get; set; }

        public DbSet<ProductFeature> ProductFeatures { get; set; }

        #endregion

        #region propduct discount

        public DbSet<ProductDiscount> ProductDiscounts { get; set; }

        public DbSet<ProductDiscountUse> ProductDiscountUses { get; set; }

        #endregion

        #region order

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderDetail> OrderDetails { get; set; }

        #endregion

        #region wallet

        public DbSet<SellerWallet> SellerWallets { get; set; }

        #endregion

        #region on model creating

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(s => s.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            base.OnModelCreating(modelBuilder);
        }

        #endregion
    }
}
