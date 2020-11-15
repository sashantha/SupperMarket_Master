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
using Wingcode.Purchases.ViewModels;

namespace Wingcode.Purchases.Views
{
    /// <summary>
    /// Interaction logic for ViewA.xaml
    /// </summary>
    public partial class GRNSheet : UserControl
    {

        private IEventAggregator aggregator;

        public GRNSheet()
        {
            InitializeComponent();
            aggregator = ServiceLocator.Current.GetInstance<IEventAggregator>();
            aggregator.GetEvent<UIElementFocusEvent>().Subscribe(FocusElement, ThreadOption.UIThread);

        }

        private void FocusElement(string obj)
        {
            switch (obj)
            {
                case "inv":
                    invNo.Clear();
                    invNo.Focus();
                    break;
                case "sup":
                    supName.Focus();
                    break;
                case "pdt":
                    pdtp.Focus();
                    break;
                case "itm":
                    itmCode.Focus();
                    break;
                case "cah":
                    pCasha.Focus();
                    pCasha.SelectAll();
                    break;
                case "chqa":
                    pChqamt.Focus();
                    pChqamt.SelectAll();
                    break;
                case "chq":
                    pChqno.Focus();
                    break;
                case "sav":
                    pSave.Focus();
                    break;
                default:
                    invNo.Focus();
                    break;
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            (DataContext as GRNSheetViewModel).Initialize();
        }

    }
}
