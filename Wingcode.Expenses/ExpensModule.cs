using Prism.Ioc;
using Prism.Modularity;
using Wingcode.Base.Api;

namespace Wingcode.Expenses
{
    public class ExpensModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            IMenuRegistryProvider menuRegistry = containerProvider.Resolve<IMenuRegistryProvider>();
            menuRegistry.Register(new ExpensesMenuRegistry());
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            
        }
    }
}