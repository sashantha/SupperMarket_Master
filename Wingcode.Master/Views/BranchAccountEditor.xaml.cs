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
using Wingcode.Data.Rest.Model;
using Wingcode.Master.Event;
using Wingcode.Master.ViewModels;

namespace Wingcode.Master.Views
{
    /// <summary>
    /// Interaction logic for BranchAccountEditor.xaml
    /// </summary>
    public partial class BranchAccountEditor : UserControl
    {

        private IEventAggregator aggregator;

        public BranchAccountEditor()
        {
            InitializeComponent();
            aggregator = ServiceLocator.Current.GetInstance<IEventAggregator>();
            aggregator.GetEvent<UIElementFocusEvent>().Subscribe(FocusElement, ThreadOption.UIThread);
            aggregator.GetEvent<BankSelectionEvent>().Subscribe(SelectBank,ThreadOption.UIThread);
        }

        private void SelectBank(Bank obj)
        {
            IEnumerable<Bank> enumerable = baBank.Items.Cast<Bank>();
            Bank isg = enumerable.Where(r => r.name.Equals(obj.name)).FirstOrDefault();
            int i = baBank.Items.IndexOf(isg);
            baBank.SelectedIndex = i;
        }

        private void FocusElement(string obj)
        {
           baBank.Focus();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            (DataContext as BranchAccountEditorViewModel).Initialize();
        }
    }
}
