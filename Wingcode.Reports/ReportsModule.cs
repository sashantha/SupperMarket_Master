using Prism.Ioc;
using Prism.Modularity;
using Wingcode.Base.Api;

namespace Wingcode.Reports
{
    public class ReportsModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            IMenuRegistryProvider menuRegistry = containerProvider.Resolve<IMenuRegistryProvider>();
            menuRegistry.Register(new ReportMenusRegistry());
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            
        }
    }
}