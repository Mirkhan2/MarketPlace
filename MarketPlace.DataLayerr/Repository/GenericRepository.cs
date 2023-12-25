using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketPlace.DataLayerr.Entities.Commen;
using MarketPlace.DataLayerr.Entities.Context;
using Microsoft.EntityFrameworkCore;

namespace MarketPlace.DataLayerr.Repository
{
	public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
	{
       private readonly MarketPlaceDbContext _context;
		private readonly DbSet<TEntity> _dbSet;

        public GenericRepository(MarketPlaceDbContext context)
        {
            _context = context;
            this._dbSet = context.Set<TEntity>();
        }
        public async Task AddEntity(TEntity entity)
        {
			entity.CreateDate = DateTime.Now;
            _dbSet.AddAsync(entity);
        }

	

		public async Task<TEntity> GetEntityById(long entityId)
		{
			return await _dbSet.SingleOrDefaultAsync(s => s.Id == entityId );
		}

		public void EditEntity(TEntity entity)
		{
			entity.LastUpdateDate = DateTime.Now;
			_dbSet.Update(entity);
		}

		public void DeleteEntity(TEntity entity)
		{
			_dbSet.Remove(entity);
		}

		public void DeleteEntity(long entityId)
		{
			throw new NotImplementedException();
		}
		public async ValueTask DisposeAsync()
		{
			if (_context != null)
			{
				await _context.DisposeAsync();
			}

		}

		public void DeletePermanent(TEntity entity)
		{
			_dbSet.Remove(entity);
		}

		public async void DeleteParmanent(long entityId)
		{
			TEntity entity = await GetEntityById(entityId);
		}

		

		private void DeleteParmenat(TEntity entity)
		{
			TEntity entity = await GetEntityById(entityId);
			DeleteParmenat(entity);
		}

		public Task DeleteParmenant(long entityId)
		{
			throw new NotImplementedException();
		}
	}
}
