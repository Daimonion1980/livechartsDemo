using Microsoft.Extensions.DependencyInjection;
using MvvmGen.Events;
using System.Configuration;
using System.Data;
using System.Windows;

namespace WpfApp5
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static ServiceProvider? ServiceProvider { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        { 
            base.OnStartup(e);

            var serviceCollection = new ServiceCollection();

            serviceCollection.AddSingleton<IEventAggregator, EventAggregator>();
            serviceCollection.AddSingleton<IDataRecordFactory, DataRecordFactory>();

            serviceCollection.AddTransient<MainWindow>();
            serviceCollection.AddTransient<VM_MainWindow>();
            serviceCollection.AddTransient<IVM_PlotFactory, VM_PlotFactory>();
            serviceCollection.AddTransient<IVM_DataTableFactory, VM_DataTableFactory>();

            ServiceProvider = serviceCollection.BuildServiceProvider();

            var mainWindow = ServiceProvider.GetService<MainWindow>();
            mainWindow?.Show();
        }

    }

}
