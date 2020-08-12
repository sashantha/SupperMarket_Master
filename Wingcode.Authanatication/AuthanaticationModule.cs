using Wingcode.Authanatication.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using Prism.Mvvm;
using Prism.Events;
using CommonServiceLocator;

namespace Wingcode.Authanatication
{
    public class AuthanaticationModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion("RootContentRegion", typeof(LoginPage));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            
        }

        
    }
}