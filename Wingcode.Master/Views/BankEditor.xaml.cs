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
using Wingcode.Master.ViewModels;

namespace Wingcode.Master.Views
{
    /// <summary>
    /// Interaction logic for BankEditor.xaml
    /// </summary>
    public partial class BankEditor : UserControl
    {
        private IEventAggregator aggregator;

        public BankEditor()
        {
            InitializeComponent();
            aggregator = ServiceLocator.Current.GetInstance<IEventAggregator>();
            aggregator.GetEvent<UIElementFocusEvent>().Subscribe(FocusElement, ThreadOption.UIThread);
        }

        private void FocusElement(string obj)
        {
            bool b = bName.Focus();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            (DataContext as BankEditorViewModel).Initialize();
        }

    }
}
