using CommonServiceLocator;
using Prism.Events;
using System.Windows.Controls;
using Wingcode.Base.Event;
using Wingcode.Items.ViewModels;

namespace Wingcode.Items.Views
{
    /// <summary>
    /// Interaction logic for ItemGroupEditor.xaml
    /// </summary>
    public partial class ItemGroupEditor : UserControl
    {
        private IEventAggregator aggregator;

        public ItemGroupEditor()
        {
            InitializeComponent();
            aggregator = ServiceLocator.Current.GetInstance<IEventAggregator>();
            aggregator.GetEvent<DataGridSelectionClearEvent>().Subscribe(ClearGridSelection, ThreadOption.UIThread);
            aggregator.GetEvent<UIElementFocusEvent>().Subscribe(FocusElement, ThreadOption.UIThread);
        }

        private void FocusElement(string elementName)
        {
            iGroup.Focus();
        }

        private void ClearGridSelection()
        {
            igGrid.SelectedIndex = -1;
        }

        private void UserControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            (DataContext as ItemGroupEditorViewModel).Initialize();
        }
    }
}
