using Stock.Application.Descriptions;
using Stock.Application.IRepositories;
using Stock.Application.IServices;
using Stock.Application.Paging;
using Stock.Core.Entities;

namespace Stock.Infrastructure.Services
{
    public class ItemsService : IItemsService
    {
        private readonly IGenericRepository<Item> _itemsRepository;

        public ItemsService(IGenericRepository<Item> itemsRepository)
        {
            this._itemsRepository = itemsRepository;
        }

        public async Task<PagedList<Item>> GetItemsPageAsync(PageParameters pageParameters)
        {
            return await this._itemsRepository.GetPageAsync(pageParameters);
        }

        public async Task<PagedList<Item>> GetItemsPageAsync(PageParameters pageParameters, string filter)
        {
            return await this._itemsRepository.GetPageAsync(pageParameters, i => i.Name.StartsWith(filter));
        }

        public async Task ProcessIncomeInvoiceAsync(IEnumerable<Item> items)
        {
            foreach (var item in items)
            {
                if (item.Id > 0)
                {
                    var dbItem = await this._itemsRepository.GetOneAsync(item.Id);
                    dbItem.Count += item.Count;
                    dbItem.LastAppeal = DateTime.Now;
                    await this._itemsRepository.UpdateAsync(dbItem);
                }
                else
                {
                    item.LastAppeal = DateTime.Now;
                    await this._itemsRepository.AddAsync(item);
                }
            }
        }

        public async Task<OperationDetails> ProcessSalesInvoiceAsync(IEnumerable<Item> items)
        {
            var details = new OperationDetails();
            foreach (var item in items)
            {
                var dbItem = await this._itemsRepository.GetOneAsync(item.Id);
                if (dbItem == null)
                {
                    details.AddError($"{item.Name} doesnt exist.");
                    continue;
                }

                var count = dbItem.Count - item.Count;
                if (count >= 0)
                {
                    dbItem.Count = count;
                    dbItem.LastAppeal = DateTime.Now;
                    await this._itemsRepository.UpdateAsync(dbItem);
                }
                else
                {
                    details.AddError($"Can't update {dbItem.Name} because there are only {dbItem.Count} in stock.");
                }
            }

            return details;
        }
    }
}
