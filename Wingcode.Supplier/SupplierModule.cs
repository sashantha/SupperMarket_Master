using Prism.Ioc;
using Prism.Modularity;
using Wingcode.Base.Api;

namespace Wingcode.Supplier
{
    public class SupplierModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            IMenuRegistryProvider menuRegistry = containerProvider.Resolve<IMenuRegistryProvider>();
            menuRegistry.Register(new SupplierMenuRegistry());
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            
        }
    }
}