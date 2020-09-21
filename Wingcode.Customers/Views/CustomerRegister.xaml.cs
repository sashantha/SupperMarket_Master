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
using Wingcode.Customers.ViewModels;

namespace Wingcode.Customers.Views
{
    /// <summary>
    /// Interaction logic for ViewA.xaml
    /// </summary>
    public partial class CustomerRegister : UserControl
    {
        private IEventAggregator aggregator;
        public CustomerRegister()
        {
            InitializeComponent();
            aggregator = ServiceLocator.Current.GetInstance<IEventAggregator>();
            aggregator.GetEvent<DataGridSelectionClearEvent>().Subscribe(ClearGridSelection, ThreadOption.UIThread);
            aggregator.GetEvent<UIElementFocusEvent>().Subscribe(FocusElement, ThreadOption.UIThread);
        }

        private void FocusElement(string elementName)
        {
            cusName.Focus();
        }

        private void ClearGridSelection() 
        {
            cusGrid.SelectedIndex = -1;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            (DataContext as CustomerRegisterViewModel).Initialize();
        }
    }
}
