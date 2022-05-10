using Stock.Application.Paging;
using Stock.Core.Entities;

namespace Stock.Application.IRepositories
{
    public interface IGenericRepository<TEntity> where TEntity : EntityBase
    {
        Task AddAsync(TEntity entity);

        Task UpdateAsync(TEntity entity);

        Task DeleteAsync(TEntity entity);

        Task<TEntity> GetOneAsync(int id);

        Task<PagedList<TEntity>> GetPageAsync(PageParameters pageParameters);
    }
}
