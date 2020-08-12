using Wingcode.Item.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using Wingcode.Base.Api;

namespace Wingcode.Item
{
    public class ItemModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            IMenuRegistryProvider menuRegistry = containerProvider.Resolve<IMenuRegistryProvider>();
            menuRegistry.Register(new ItemMenusRegistry());
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            
        }
    }
}