﻿using Microsoft.EntityFrameworkCore;
using Stock.Application.IRepositories;
using Stock.Application.Paging;
using Stock.Core.Entities;
using Stock.Infrastructure.EF.EducationalPortal.Infrastructure.EF;

namespace Stock.Infrastructure.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : EntityBase
    {
        private readonly ApplicationContext _db;

        private readonly DbSet<TEntity> _table;

        public GenericRepository()
        {
            this._db = new ApplicationContext();
            this._table = _db.Set<TEntity>();
        }

        public async Task AddAsync(TEntity item)
        {
            await this._table.AddAsync(item);
            await this.SaveAsync();
        }

        public async Task UpdateAsync(TEntity item)
        {
            this._table.Update(item);
            await this.SaveAsync();
        }

        public async Task DeleteAsync(TEntity item)
        {
            this._table.Remove(item);
            await this.SaveAsync();
        }

        public async Task<TEntity> GetOneAsync(int id)
        {
            return await this._table.FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<PagedList<TEntity>> GetPageAsync(PageParameters pageParameters)
        {
            var entities = await this._table
                                     .AsNoTracking()
                                     .Skip((pageParameters.PageNumber - 1) * pageParameters.PageSize)
                                     .Take(pageParameters.PageSize)
                                     .ToListAsync();
            var totalCount = await this._table.CountAsync();

            return new PagedList<TEntity>(entities, pageParameters, totalCount);
        }

        private async Task SaveAsync()
        {
            await this._db.SaveChangesAsync();
        }
    }
}
