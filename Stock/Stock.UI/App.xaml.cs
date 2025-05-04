using Microsoft.Extensions.DependencyInjection;
using Stock.Infrastructure;
using Stock.Infrastructure.DataInitializer;
using System.Windows;

namespace Stock.UI
{
    public partial class App : System.Windows.Application
    {
        private ServiceProvider _serviceProvider;

        public App()
        {
            var services = new ServiceCollection();
            services.AddSingleton<MainWindow>();
            services.AddInfrastructure();
            services.AddServices();
            this._serviceProvider = services.BuildServiceProvider();
        }
        private void OnStartup(object sender, StartupEventArgs e)
        {
            // Uncomment to Initialize DB and fill it with start data
            // DbInitializer.InitializeDb(this._serviceProvider);

            var mainWindow = this._serviceProvider.GetService<MainWindow>();
            mainWindow.Show();
        }
    }
}
