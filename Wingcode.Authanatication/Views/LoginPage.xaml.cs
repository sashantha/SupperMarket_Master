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

        public SecureString SecurePassword => password.SecurePassword;
    }
}
