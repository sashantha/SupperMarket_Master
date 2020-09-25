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
using Wingcode.Base.Event;
using Wingcode.Suppliers.ViewModels;

namespace Wingcode.Suppliers.Views
{
    /// <summary>
    /// Interaction logic for ViewA.xaml
    /// </summary>
    public partial class SupplierRegister : UserControl
    {
        private IEventAggregator aggregator;
        public SupplierRegister()
        {
            InitializeComponent();
            aggregator = ServiceLocator.Current.GetInstance<IEventAggregator>();
            aggregator.GetEvent<DataGridSelectionClearEvent>().Subscribe(ClearGridSelection, ThreadOption.UIThread);
            aggregator.GetEvent<UIElementFocusEvent>().Subscribe(FocusElement, ThreadOption.UIThread);
        }

        private void FocusElement(string elementName)
        {
            supName.Focus();
        }

        private void ClearGridSelection()
        {
            supGrid.SelectedIndex = -1;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            (DataContext as SupplierRegisterViewModel).Initialize();
        }
    }
}
