using Wingcode.Customers.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using System.Collections.Generic;
using Wingcode.Base.ViewModels;
using Wingcode.Base.Api;

namespace Wingcode.Customers
{
    public class CustomersModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            IMenuRegistryProvider menuRegistry = containerProvider.Resolve<IMenuRegistryProvider>();
            menuRegistry.Register(new CustomersMenusRegistry());
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
        }
    }
}