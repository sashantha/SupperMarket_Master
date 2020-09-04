using Prism.Commands;
using Prism.Services.Dialogs;
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
using System.Windows.Shapes;
using Wingcode.Base.ViewModels;

namespace Wingcode.Base.Dialog
{
    /// <summary>
    /// Interaction logic for WingcodeDialogWindow.xaml
    /// </summary>
    public partial class WingcodeDialogWindow : Window, IDialogWindow
    {

        public ICommand CloseCommand => new DelegateCommand(Close); 
        public WingcodeDialogWindow()
        {
            //DataContext = new WingcodeDialogWindowViewModel(this);
            InitializeComponent();
        }

        public IDialogResult Result { get; set; }
    }
}
