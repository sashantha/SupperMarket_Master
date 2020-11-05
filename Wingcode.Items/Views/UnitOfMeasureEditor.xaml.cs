using CommonServiceLocator;
using Prism.Events;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Wingcode.Base.Event;
using Wingcode.Items.ViewModels;

namespace Wingcode.Items.Views
{
    /// <summary>
    /// Interaction logic for UnitOfMeasureEditor.xaml
    /// </summary>
    public partial class UnitOfMeasureEditor : UserControl
    {

        private IEventAggregator aggregator;

        public UnitOfMeasureEditor()
        {
            InitializeComponent();
            aggregator = ServiceLocator.Current.GetInstance<IEventAggregator>();
            aggregator.GetEvent<UIElementFocusEvent>().Subscribe(FocusElement, ThreadOption.UIThread);
            aggregator.GetEvent<DataGridSelectionClearEvent>().Subscribe(ClearGridSelection, ThreadOption.UIThread);
        }

        private void ClearGridSelection()
        {
            uomGrid.SelectedIndex = -1;
        }

        private void FocusElement(string obj)
        {
            uDes.Focus();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            (DataContext as UnitOfMeasureEditorViewModel).Initialize();
        }

    }
}
