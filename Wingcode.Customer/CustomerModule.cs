using Wingcode.Customer.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using System.Collections.Generic;
using Wingcode.Base.ViewModels;
using Wingcode.Base.Api;

namespace Wingcode.Customer
{
    public class CustomerModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            IMenuRegistryProvider menuRegistry = containerProvider.Resolve<IMenuRegistryProvider>();
            menuRegistry.Register(new CustomerMenusRegistry());
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
        }
    }
}