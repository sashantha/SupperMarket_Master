using CommonServiceLocator;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Wingcode.Base.Event;
using Wingcode.Data.Rest.Model;
using Wingcode.Items.Event;
using Wingcode.Items.ViewModels;

namespace Wingcode.Items.Views
{
    /// <summary>
    /// Interaction logic for ItemEditor.xaml
    /// </summary>
    public partial class ItemEditor : UserControl
    {
        private IEventAggregator aggregator;

        public ItemEditor()
        {
            InitializeComponent();
            aggregator = ServiceLocator.Current.GetInstance<IEventAggregator>();
            aggregator.GetEvent<UIElementFocusEvent>().Subscribe(FocusElement, ThreadOption.UIThread);
            aggregator.GetEvent<ItemGroupSelectionEvent>().Subscribe(ItemGroupSelection, ThreadOption.UIThread);
            aggregator.GetEvent<ItemSubGroupSelectionEvent>().Subscribe(ItemSubGroupSelection, ThreadOption.UIThread);
        }

        private void ItemSubGroupSelection(ItemSubGroup obj)
        {
            IEnumerable<ItemSubGroup> enumerable = iSubGroup.Items.Cast<ItemSubGroup>();
            ItemSubGroup isg = enumerable.Where(r => r.subGroupName.Equals(obj.subGroupName)).FirstOrDefault();
            int i = iSubGroup.Items.IndexOf(isg);
            iSubGroup.SelectedIndex = i;
        }

        private void ItemGroupSelection(ItemGroup obj)
        {
            IEnumerable<ItemGroup> enumerable = iGroup.Items.Cast<ItemGroup>();
            ItemGroup ig = enumerable.Where(r => r.groupName.Equals(obj.groupName)).FirstOrDefault();
            int i = iGroup.Items.IndexOf(ig);
            iGroup.SelectedIndex = i;
        }

        private void FocusElement(string elementName)
        {
            icat.Focus();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            (DataContext as ItemEditorViewModel).Initialize();
        }

    }
}
