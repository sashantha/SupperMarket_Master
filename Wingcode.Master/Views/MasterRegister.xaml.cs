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
    /// Interaction logic for ViewA.xaml
    /// </summary>
    public partial class MasterRegister : UserControl
    {
        private IEventAggregator aggregator;

        public MasterRegister()
        {
            InitializeComponent();
            aggregator = ServiceLocator.Current.GetInstance<IEventAggregator>();
            aggregator.GetEvent<DataGridSelectionClearEvent>().Subscribe(ClearGridSelection, ThreadOption.UIThread);
        }

        private void ClearGridSelection()
        {
            brGrid.SelectedIndex = -1;
            bGrid.SelectedIndex = -1;
            brcGrid.SelectedIndex = -1;
            mGrid.SelectedIndex = -1;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            (DataContext as MasterRegisterViewModel).Initialize();
        }
    }
}
