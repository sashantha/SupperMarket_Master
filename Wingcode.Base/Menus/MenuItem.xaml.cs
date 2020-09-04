using CommonServiceLocator;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Wingcode.Base.Api;
using Wingcode.Base.Event;
using Wingcode.Base.ViewModels;

namespace Wingcode.Base.Menus
{
    /// <summary>
    /// Interaction logic for MenuItem.xaml
    /// </summary>
    public partial class MenuItem : UserControl
    {

        private MenuItemViewModel mViewModel;
        private IApplicationController controller;
        private IEventAggregator eventAggregator;

        public MenuItemViewModel ViewModelObject
        {
            get => mViewModel;
            set
            {
                if (mViewModel == value)
                    return;
                //update vm
                mViewModel = value;
                //reset datacontext
                DataContext = mViewModel;
            }
        }

        public MenuItem()
        {
            InitializeComponent();
            controller = ServiceLocator.Current.GetInstance<IApplicationController>();
            eventAggregator = ServiceLocator.Current.GetInstance<IEventAggregator>();
        }

        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (controller == null)
                return;
            SubMenuItemViewModel sub = ((ListView)sender).SelectedItem as SubMenuItemViewModel;
            controller.ShowView(sub.AttachedControl);
        }


        private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (controller == null)
                return;

            if (sender is TextBlock tb)
            {
                IMenuRegistryProvider provider = ServiceLocator.Current.GetInstance<IMenuRegistryProvider>();
                if (provider != null)
                {
                    MenuItemViewModel mv = provider.GetMenu(tb.Text);
                    controller.ShowView(mv.AttachedControl);                    
                }
            }
        }

        private void TextBlock_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            if (controller == null)
                return;
            if (sender is TextBlock tb)
            {
                SubMenuItemViewModel sub = ListViewMenu.Items.Cast<SubMenuItemViewModel>().Where(i => i.Name.Equals(tb.Text)).FirstOrDefault();
                controller.ShowView(sub.AttachedControl);
            }
        }

        private void ExpanderMenu_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            eventAggregator.GetEvent<MenuItemControlExpandEvent>().Publish(ViewModelObject.Name);
            eventAggregator.GetEvent<MenuExpndControlEvent>().Publish(true);
        }

    }
}
