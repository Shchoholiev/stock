using Stock.Application.Paging;
using Stock.Core.Entities;
using System.Linq.Expressions;

namespace Stock.Application.IRepositories
{
    public interface IGenericRepository<TEntity> where TEntity : EntityBase
    {
        Task AddAsync(TEntity entity);

        Task UpdateAsync(TEntity entity);

        Task UpdateRangeAsync(IEnumerable<TEntity> entities);

        Task DeleteAsync(TEntity entity);

        Task<TEntity> GetOneAsync(int id);

        Task<PagedList<TEntity>> GetPageAsync(PageParameters pageParameters);

        Task<PagedList<TEntity>> GetPageAsync(PageParameters pageParameters, 
                                              Expression<Func<TEntity, bool>> predicate);
    }
}
