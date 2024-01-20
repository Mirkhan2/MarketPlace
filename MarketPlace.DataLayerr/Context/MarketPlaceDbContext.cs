using System.Linq;
using MarketPlace.DataLayerr.Entities.Account;
using MarketPlace.DataLayerr.Entities.ContactUs;
using MarketPlace.DataLayerr.Entities.Site;
using Microsoft.EntityFrameworkCore;

namespace MarketPlace.DataLayerr.Context
{

    public class MarketPlaceDbContext : DbContext
	{
		public MarketPlaceDbContext(DbContextOptions<MarketPlaceDbContext> options) : base(options) { }

		#region account

		public DbSet<User> Users { get; set; }
      
        #endregion
        #region site

        public DbSet<SiteSetting> SiteSettings { get; set; }

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
