using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketPlace.DataLayerr.Entities.Account;
using Microsoft.EntityFrameworkCore;

namespace MarketPlace.DataLayerr.Entities.Context
{
	public  class MarketPlaceDbContext: DbContext
	{
        #region dbsets

        public DbSet<User> Users { get; set; }

        #endregion
    }
}
