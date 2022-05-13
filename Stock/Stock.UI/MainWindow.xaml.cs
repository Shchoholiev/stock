using Stock.Application.IServices;
using Stock.Application.Paging;
using Stock.Core.Entities;
using Stock.Infrastructure.DataInitializer;
using Stock.Infrastructure.EF.EducationalPortal.Infrastructure.EF;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

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

        private async Task Search(int pageNumber)
        {
            this._pageParameters.PageNumber = pageNumber;
            this._items = await this._itemsService.GetItemsPageAsync(this._pageParameters, filter.Text);
            this.stock.ItemsSource = this._items;
            pagingInfo.Content = $"{this._items.PageNumber} of {this._items.TotalPages}";
        }

        private async void btnFirst_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(filter.Text))
            {
                await this.SetPage(1);
            }
            else
            {
                await this.Search(1);
            }
        }

        private async void btnPrev_Click(object sender, RoutedEventArgs e)
        {
            if (this._items.PageNumber > 1)
            {
                if (string.IsNullOrEmpty(filter.Text))
                {
                    await this.SetPage(this._items.PageNumber - 1);
                }
                else
                {
                    await this.Search(this._items.PageNumber - 1);
                }
            }
        }

        private async void btnNext_Click(object sender, RoutedEventArgs e)
        {
            if (this._items.PageNumber < this._items.TotalPages)
            {
                if (string.IsNullOrEmpty(filter.Text))
                {
                    await this.SetPage(this._items.PageNumber + 1);
                }
                else
                {
                    await this.Search(this._items.PageNumber + 1);
                }
            }
        }

        private async void btnLast_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(filter.Text))
            {
                await this.SetPage(this._items.TotalPages);
            }
            else
            {
                await this.Search(this._items.TotalPages);
            }
        }

        private void AddToInvoice(object sender, RoutedEventArgs e)
        {
            var itemId = Convert.ToInt32((sender as Button)?.Tag);
            if (!this._invoiceItems.Any(i => i.Id == itemId))
            {
                var item = this._items.FirstOrDefault(i => i.Id == itemId);
                var invoiceItem = new Item
                {
                    Id = item.Id,
                    Name = item.Name,
                    Price = item.Price,
                    Unit = item.Unit,
                    LastAppeal = item.LastAppeal,
                    Count = 0,
                };
                this._invoiceItems.Add(invoiceItem);
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

        private async void ProcessInvoice(object sender, RoutedEventArgs e)
        {
            if (this._invoiceItems.Count() < 1)
            {
                MessageBox.Show("Choose items!", "Alert", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            var invoiceType = (this.type.SelectedItem as ComboBoxItem)?.Content.ToString();
            switch (invoiceType)
            {
                case "Income":
                    await this._itemsService.ProcessIncomeInvoiceAsync(this._invoiceItems);
                    this.ClearInvoice();
                    await this.SetPage(this._items.PageNumber);
                    break;

                case "Sales":
                    var result = await this._itemsService.ProcessSalesInvoiceAsync(this._invoiceItems);
                    if (!result.Succeeded)
                    {
                        var message = "";
                        foreach (var error in result.Messages)
                        {
                            message += error + "\n";
                        }
                        MessageBox.Show(message, "Alert", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    this.ClearInvoice();
                    await this.SetPage(this._items.PageNumber);
                    break;

                default:
                    MessageBox.Show("Choose invoice type!", "Alert", MessageBoxButton.OK, MessageBoxImage.Error);
                    break;
            }
        }

        private void RefreshInvoiceGrid()
        {
            this.invoice.Items.Refresh();
        }

        private void ClearInvoice()
        {
            this._invoiceItems.Clear();
            this.RefreshInvoiceGrid();
        }

        private void ClearInvoice(object sender, RoutedEventArgs e)
        {
            this.ClearInvoice();
        }
    }
}
