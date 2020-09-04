using Wingcode.Master.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using Wingcode.Base.Api;

namespace Wingcode.Master
{
    public class MasterModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            IMenuRegistryProvider menuRegistry = containerProvider.Resolve<IMenuRegistryProvider>();
            menuRegistry.Register(new MasterMenuRegistry());
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            
        }
    }
}