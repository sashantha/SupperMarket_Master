using System.Security;
using System.Windows.Controls;
using Wingcode.Base.Api;

namespace Wingcode.Authanatication.Views
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginPage : UserControl, IHavePassword
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        public string Password => password.Password;

        public void ClearPassword()
        {            
            password.Password = string.Empty;
            userName.Focus();
        }

        private void branches_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {            
            if (sender is ComboBox combo)
                combo.IsDropDownOpen = true;
        }

    }
}
