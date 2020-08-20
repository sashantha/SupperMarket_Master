using CommonServiceLocator;
using Prism.Events;
using Prism.Regions;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using Wingcode.Base.Event;
using Wingcode.Base.ViewModels;
using MenuItem = Wingcode.Base.Menus.MenuItem;

namespace Wingcode.SupperMarket.Views
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : UserControl
    {

        private bool MenuClosed = false;
        private IEventAggregator aggregator;
        private IRegionManager regionManager;

        public MainPage()
        {
            InitializeComponent();
            aggregator = ServiceLocator.Current.GetInstance<IEventAggregator>();
            regionManager = ServiceLocator.Current.GetInstance<IRegionManager>();
            if (aggregator != null)
            {
                aggregator.GetEvent<MenuCreationEvent>().Subscribe(UpdateMenuHolder, ThreadOption.UIThread, true);
                aggregator.GetEvent<MenuExpndControlEvent>().Subscribe(MenuExpandControl, ThreadOption.UIThread, true);
            }
            RegionManager.SetRegionName(ControlHolder, "ContentPaneRegion");
            RegionManager.SetRegionManager(ControlHolder, regionManager);
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (MenuClosed)
            {
                Open.Visibility = Visibility.Collapsed;
                Close.Visibility = Visibility.Visible;
                Storyboard openMenu = (Storyboard)Open.FindResource("OpenMenu");
                openMenu.Begin();
            }
            else
            {
                Open.Visibility = Visibility.Visible;
                Close.Visibility = Visibility.Collapsed;
                Storyboard closeMenu = (Storyboard)Close.FindResource("CloseMenu");
                closeMenu.Begin();
            }
            MenuClosed = !MenuClosed;
        }

        private void MenuExpandControl(bool parameter) 
        {
            if (MenuClosed == !parameter)
                return;
            MenuClosed = parameter;
            button_Click(this, new RoutedEventArgs());
        }

        private void UpdateMenuHolder(List<MenuItemViewModel> menuItemViews) 
        {
            if (menuItemViews == null)
                return;

            MenuHolder.Children.Clear();
            foreach (MenuItemViewModel item in menuItemViews)
            {
                MenuItem menu = new MenuItem() { ViewModelObject = item};
                MenuHolder.Children.Add(menu);
            }
        }
    }
}
