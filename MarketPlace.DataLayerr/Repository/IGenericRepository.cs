using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketPlace.DataLayerr.Entities.Commen;

namespace MarketPlace.DataLayerr.Repository
{
	public interface IGenericRepository<TEntity> : IAsyncDisposable where TEntity : BaseEntity 
	{
		Task AddEntity(TEntity entity);
		Task<TEntity> GetEntityById(long entityId);
		void EditEntity(TEntity entity);
		void DeleteEntity(TEntity entity);
		void DeleteEntity(long entityId);
		void DeletePermanent(TEntity entity);
		
		Task DeleteParmenant(long entityId);

	}
}
