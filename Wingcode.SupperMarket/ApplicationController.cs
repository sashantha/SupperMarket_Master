using CommonServiceLocator;
using Prism.Events;
using Prism.Ioc;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Wingcode.Authanatication.Views;
using Wingcode.Base;
using Wingcode.Base.Api;
using Wingcode.Base.Event;
using Wingcode.SupperMarket.Views;
using MenuItem = Wingcode.SupperMarket.Controls.MenuItem;

namespace Wingcode.SupperMarket
{
    internal class ApplicationController : IApplicationController
    {
        public bool IsInitilized { get; set; } = false;

        private IContainerExtension containerExtension;
        private IRegionManager regionManager;
        private IEventAggregator eventAggregator;
        private IRegion tcRegion;
        private IRegion rtRegion;
        private IRegion cpRegion;
        private TopContent topContent;
        private MainPage mainPage;
        private LoginPage loginPage;

        public ApplicationController(IContainerExtension container, IRegionManager rManager, IEventAggregator aggregator)
        {
            containerExtension = container;
            regionManager = rManager;
            eventAggregator = aggregator;
        }
        public async Task InitializeApplication()
        {
            if (IsInitilized)
                return;
            tcRegion = regionManager.Regions["TopContentRegion"];
            rtRegion = regionManager.Regions["RootContentRegion"];
            
            topContent = containerExtension.Resolve<TopContent>();
            mainPage = containerExtension.Resolve<MainPage>();
            loginPage = containerExtension.Resolve<LoginPage>();
            tcRegion.Add(topContent);
            rtRegion.Add(loginPage);
            rtRegion.Add(mainPage);

            tcRegion.Deactivate(topContent);
            //rtRegion.Deactivate(loginPage);
            //rtRegion.Activate(mainPage);
            IMenuRegistryProvider menu = containerExtension.Resolve<IMenuRegistryProvider>();
            if (menu != null)
                eventAggregator.GetEvent<MenuCreationEvent>().Publish(menu.GetMenuItemViews());
            //eventAggregator.GetEvent<MenuExpndControlEvent>().Publish(false);
            cpRegion = regionManager.Regions["ContentPaneRegion"];
            IsInitilized = true;
            await Task.Delay(1000);
        }

        public async Task LogingApplication(LoginInfor loginInfor)
        {
            rtRegion.Deactivate(loginPage);
            rtRegion.Activate(mainPage);
            await Task.Delay(1000);
        }

        public async Task LogoutApllication()
        {

            await Task.Delay(1000);
        }

        public Task RestartApplication()
        {
            throw new NotImplementedException();
        }

        public Task ShowMainView()
        {
            throw new NotImplementedException();
        }

        public Task ShutdownApplication()
        {
            throw new NotImplementedException();
        }

        public async Task ShowView(Type viewType)
        {
            if(cpRegion != null) 
            {
                cpRegion.RemoveAll();
                UserControl uc = containerExtension.Resolve(viewType) as UserControl;
                if (uc != null)
                {
                    cpRegion.Add(uc);
                    cpRegion.Activate(uc);
                    eventAggregator.GetEvent<MenuExpndControlEvent>().Publish(true);
                }
            }
            await Task.Delay(0);
        }
    }
}
