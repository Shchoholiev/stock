using Stock.Application.Descriptions;
using Stock.Application.Paging;
using Stock.Core.Entities;

namespace Stock.Application.IServices
{
    public interface IItemsService
    {
        Task<PagedList<Item>> GetItemsPageAsync(PageParameters pageParameters);

        Task ProcessIncomeInvoiceAsync(IEnumerable<Item> items);

        Task<OperationDetails> ProcessSalesInvoiceAsync(IEnumerable<Item> items);
    }
}
