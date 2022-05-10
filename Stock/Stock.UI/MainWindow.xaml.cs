using Stock.Application.IServices;
using Stock.Application.Paging;
using Stock.Core.Entities;
using Stock.Infrastructure.DataInitializer;
using Stock.Infrastructure.EF.EducationalPortal.Infrastructure.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Stock.UI
{
    public partial class MainWindow : Window
    {
        private readonly IItemsService _itemsService;

        private PageParameters _pageParameters = new ();

        private PagedList<Item> _items = new();

        private PagedList<Item> _invoiceItems = new();

        public MainWindow(IItemsService itemsService)
        {
            this._itemsService = itemsService;

            var context = new ApplicationContext();
            DbInitializer.Initialize(context);

            InitializeComponent();
            new Action(async () => await this.SetPage(1))();
            this.invoice.ItemsSource = this._invoiceItems;
        }

        private async Task SetPage(int pageNumber)
        {
            this._pageParameters.PageNumber = pageNumber;
            this._items = await this._itemsService.GetItemsPageAsync(this._pageParameters);
            this.stock.ItemsSource = this._items;
            pagingInfo.Content = $"{this._items.PageNumber} of {this._items.TotalPages}";
        }

        private async void btnFirst_Click(object sender, RoutedEventArgs e)
        {
            await this.SetPage(1);
        }

        private async void btnPrev_Click(object sender, RoutedEventArgs e)
        {
            if (this._items.PageNumber > 1)
            {
                await this.SetPage(this._items.PageNumber - 1);
            }
        }

        private async void btnNext_Click(object sender, RoutedEventArgs e)
        {
            if (this._items.PageNumber < this._items.TotalPages)
            {
                await this.SetPage(this._items.PageNumber + 1);
            }
        }

        private async void btnLast_Click(object sender, RoutedEventArgs e)
        {
            await this.SetPage(this._items.TotalPages);
        }

        private void AddToInvoice(object sender, RoutedEventArgs e)
        {
            var itemId = Convert.ToInt32((sender as Button)?.Tag);
            if (!this._invoiceItems.Any(i => i.Id == itemId))
            {
                var item = this._items.FirstOrDefault(i => i.Id == itemId);
                item.Count = 0;
                this._invoiceItems.Add(item);
                this.RefreshInvoiceGrid();
            }
        }

        private void RemoveFromInvoice(object sender, RoutedEventArgs e)
        {
            var itemId = Convert.ToInt32((sender as Button)?.Tag);
            var item = this._invoiceItems.FirstOrDefault(i => i.Id == itemId);
            this._invoiceItems.Remove(item);
            this.RefreshInvoiceGrid();
        }

        private void RefreshInvoiceGrid()
        {
            this.invoice.Items.Refresh();
        }
    }
}
