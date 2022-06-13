using Stock.Application.Descriptions;
using Stock.Application.IRepositories;
using Stock.Application.IServices;
using Stock.Application.Mapping;
using Stock.Application.Paging;
using Stock.Core.Entities;
using Syncfusion.Drawing;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Grid;

namespace Stock.Infrastructure.Services
{
    public class ItemsService : IItemsService
    {
        private readonly IGenericRepository<Item> _itemsRepository;

        private readonly Mapper _mapper = new();

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
                    dbItem.Name = item.Name;
                    dbItem.Price = item.Price;
                    dbItem.Unit = item.Unit;
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
            var checkedItems = new List<Item>();
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
                    checkedItems.Add(dbItem);
                }
                else
                {
                    details.AddError($"Can't update {dbItem.Name} because there are only {dbItem.Count} in stock.");
                }
            }

            if (details.Succeeded)
            {
                await this._itemsRepository.UpdateRangeAsync(checkedItems);
            }

            return details;
        }

        public void GenerateInvoicePDF(IEnumerable<Item> items, string path)
        {
            var invoice = new PdfDocument();
            var page = invoice.Pages.Add();

            var font = new PdfStandardFont(PdfFontFamily.Helvetica, 20);
            var graphics = page.Graphics;
            graphics.DrawString("Invoice", font, PdfBrushes.Black, new PointF(220, 23));
            var total = items.Sum(i => i.Price * i.Count);
            var formattedTotal = String.Format("{0:0.00}", total);
            graphics.DrawString($"Total: {formattedTotal}", font, PdfBrushes.Black, new PointF(350, 700));
            
            var grid = new PdfGrid();
            grid.Style.Font = new PdfStandardFont(PdfFontFamily.Helvetica, 14);
            var dtos = this._mapper.Map(items);
            grid.DataSource = dtos;
            grid.Draw(page, new PointF(0, 60));

            using (var fs = File.Create(path))
            {
                invoice.Save(fs);
            }
        }
    }
}
