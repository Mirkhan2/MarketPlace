using System;
using System.Linq;
using System.Threading.Tasks;
using MarketPlace.Data.Entities.Commen;
using MarketPlace.Data.Entities.Contacts;

namespace MarketPlace.Data.Repository
{
	public interface IGenericRepository<TEntity> : IAsyncDisposable where TEntity : BaseEntity
	{
		IQueryable<TEntity> GetQuery();
		Task AddEntity(TEntity entity);
		Task<TEntity> GetEntityById(long entityId);
		void EditEntity(TEntity entity);
		void DeleteEntity(TEntity entity);
		Task DeleteEntity(long entityId);
		void DeletePermanent(TEntity entity);
		Task DeletePermanent(long entityId);
		Task SaveChanges();
        Task AddEntity(Ticket newTicket);
    }
}
