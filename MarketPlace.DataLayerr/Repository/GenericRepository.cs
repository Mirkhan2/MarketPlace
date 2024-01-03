using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketPlace.DataLayerr.Context;
using MarketPlace.DataLayerr.Entities.Commen;
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
			this._dbSet = _context.Set<TEntity>();
		}

		public IQueryable<TEntity> GetQuery()
		{
			return _dbSet.AsQueryable();
		}

		public async Task AddEntity(TEntity entity)
		{
			entity.CreateDate = DateTime.Now;
			await _dbSet.AddAsync(entity);
		}

		public async Task<TEntity> GetEntityById(long entityId)
		{
			return await _dbSet.SingleOrDefaultAsync(s => s.Id == entityId);
		}

		public void EditEntity(TEntity entity)
		{
			entity.LastUpdateDate = DateTime.Now;
			_dbSet.Update(entity);
		}

		public void DeleteEntity(TEntity entity)
		{
			entity.IsDelete = true;
			EditEntity(entity);
		}

		public async Task DeleteEntity(long entityId)
		{
			TEntity entity = await GetEntityById(entityId);
			if (entity != null) DeleteEntity(entity);
		}

		public void DeletePermanent(TEntity entity)
		{
			_dbSet.Remove(entity);
		}

		public async Task DeletePermanent(long entityId)
		{
			TEntity entity = await GetEntityById(entityId);
			if (entity != null) DeletePermanent(entity);
		}

		public async Task SaveChanges()
		{
			await _context.SaveChangesAsync();
		}

		public async ValueTask DisposeAsync()
		{
			if (_context != null)
			{
				await _context.DisposeAsync();
			}
		}
	}
}
