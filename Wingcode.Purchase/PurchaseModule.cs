using Wingcode.Purchase.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using Wingcode.Base.Api;

namespace Wingcode.Purchase
{
    public class PurchaseModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            IMenuRegistryProvider menuRegistry = containerProvider.Resolve<IMenuRegistryProvider>();
            menuRegistry.Register(new PurchaseMenuRegistry());
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            
        }
    }
}