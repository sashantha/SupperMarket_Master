using Prism.Ioc;
using Prism.Modularity;
using System.Windows;
using System;
using Prism.Mvvm;
using Prism.Unity;
using Prism;
using Wingcode.SupperMarket.Views;
using Wingcode.SupperMarket.ViewModels;
using Wingcode.Authanatication;
using Wingcode.Base.Api;
using CommonServiceLocator;
using Wingcode.Item;
using Prism.Regions;
using System.Windows.Controls;
using Wingcode.SupperMarket.CustomRegion;
using System.Windows.Navigation;
using Wingcode.Customer;
using Prism.Services.Dialogs;
using Wingcode.Base.Dialog;
using Wingcode.Base.ViewModels;
using Wingcode.Base.FileSystem;
using Wingcode.Base.Tasks;

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
            moduleCatalog.AddModule<ItemModule>();
            moduleCatalog.AddModule<CustomerModule>();
        }

        protected override void OnActivated(EventArgs e)
        {
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
