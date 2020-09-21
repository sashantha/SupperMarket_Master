using CommonServiceLocator;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using System;
using System.Windows;
using System.Windows.Controls;
using Wingcode.Authanatication;
using Wingcode.Base.Api;
using Wingcode.Base.Dialog;
using Wingcode.Base.FileSystem;
using Wingcode.Base.Tasks;
using Wingcode.Base.ViewModels;
using Wingcode.Customers;
using Wingcode.Data.Rest;
using Wingcode.Data.Rest.Service;
using Wingcode.Expenses;
using Wingcode.Items;
using Wingcode.Master;
using Wingcode.Purchases;
using Wingcode.Sales;
using Wingcode.SupperMarket.CustomRegion;
using Wingcode.SupperMarket.ViewModels;
using Wingcode.SupperMarket.Views;
using Wingcode.Suppliers;

namespace Wingcode.SupperMarket
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {


        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<ITaskManager, TaskManager>();
            containerRegistry.RegisterSingleton<IFileManager, FileManager>();
            containerRegistry.RegisterSingleton<IRestDataMapper, RestDataMapper>();

            containerRegistry.RegisterSingleton<IDialogService, WingcodeDialogService>();
            containerRegistry.RegisterDialogWindow<WingcodeDialogWindow>();
            containerRegistry.RegisterDialog<WingcodeMsgDialog, WingcodeMsgDialogViewModel>();
            containerRegistry.RegisterDialog<WingcodeDialogBox, WingcodeDialogBoxViewModel>();
            containerRegistry.RegisterSingleton<IApplicationController, ApplicationController>();
            containerRegistry.RegisterSingleton<IMenuRegistryProvider, MenuRegistryProvider>();
        }

        protected override void ConfigureViewModelLocator()
        {
            base.ConfigureViewModelLocator();
            ViewModelLocationProvider.Register<MainWindow>(() => new MainWindowViewModel(this.MainWindow));
            ViewModelLocationProvider.Register<WingcodeMsgDialog, WingcodeMsgDialogViewModel>();
            ViewModelLocationProvider.Register<WingcodeDialogBox, WingcodeDialogBoxViewModel>();
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            base.ConfigureModuleCatalog(moduleCatalog);
            moduleCatalog.AddModule<AuthanaticationModule>();
            moduleCatalog.AddModule<MasterModule>();
            moduleCatalog.AddModule<ItemModule>();
            moduleCatalog.AddModule<CustomersModule>();
            moduleCatalog.AddModule<SupplierModule>();
            moduleCatalog.AddModule<PurchaseModule>();
            moduleCatalog.AddModule<SalesModule>();
            moduleCatalog.AddModule<ExpensModule>();
        }

        protected override void OnActivated(EventArgs e)
        {

            bool b = RestConnection.GetRestConnection().TestConnectionAsync();
            if (!b)
            {
                if (MessageBox.Show("Application Server not found", "Internal Server Error", MessageBoxButton.OK, MessageBoxImage.Error) == MessageBoxResult.OK)
                    Current.Shutdown();
            }

            base.OnActivated(e);            
            IMenuRegistryProvider registryProvider = ServiceLocator.Current.GetInstance<IMenuRegistryProvider>();
            registryProvider.InitializeMenu();
            IApplicationController controller = ServiceLocator.Current.GetInstance<IApplicationController>();
            controller.InitializeApplication();
        }
        protected override void ConfigureRegionAdapterMappings(RegionAdapterMappings regionAdapterMappings)
        {
            base.ConfigureRegionAdapterMappings(regionAdapterMappings);
            regionAdapterMappings.RegisterMapping(typeof(StackPanel), Container.Resolve<StackPanelRegionAdapter>());
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
        }
    }
}
