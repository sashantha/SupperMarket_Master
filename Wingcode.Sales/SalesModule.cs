using Wingcode.Sales.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using Wingcode.Base.Api;

namespace Wingcode.Sales
{
    public class SalesModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            IMenuRegistryProvider menuRegistry = containerProvider.Resolve<IMenuRegistryProvider>();
            menuRegistry.Register(new SalesMenuRegistry());
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            
        }
    }
}