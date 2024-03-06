using System.Linq;
using MarketPlace.Data.Entities.Account;
using MarketPlace.Data.Entities.Account.Products;
using MarketPlace.Data.Entities.Contacts;
using MarketPlace.Data.Entities.Site;
using MarketPlace.Data.Entities.Store;
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
        public DbSet<Ticket> Ticket { get; set; }
        public DbSet<TicketMessage> TicketMessages { get; set; }

        #endregion

        #region store

        public DbSet<Seller> Sellers { get; set; }
        #endregion

        #region products
        public DbSet<ProductCategory> ProductCatagories { get; set; }
        public DbSet<Product> Products { get; set; }
        #endregion

        #region on model creating

        protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(s => s.GetForeignKeys()))
			{
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
			}

            modelBuilder.Entity<User>()
                .HasMany(s => s.TicketMessages)
                .WithOne(s => s.Sender)
                .HasForeignKey(s => s.SenderId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
                    .HasMany(s => s.Tickets)
                .WithOne(s => s.Owner)
                .HasForeignKey(s => s.OwnerId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
		}

		#endregion
	}
}
