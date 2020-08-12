using CommonServiceLocator;
using Prism.Ioc;
using System.Threading.Tasks;
using System.Windows.Input;
using Wingcode.Authanatication.Views;
using Wingcode.Base;
using Wingcode.Base.Api;
using Wingcode.Base.DataModel;
using Wingcode.Base.Input;

namespace Wingcode.Authanatication.ViewModels
{
    public class LoginPageViewModel : BaseViewModel
    {

        private IContainerExtension containerExtension;

        public string UserName { get; set; }

        public bool LoginIsRunning { get; set; }

        public string Warrning { get; set; } = "Loging Here";

        public ICommand LogingCommand { get; set; }

        public LoginPageViewModel(IContainerExtension container)
        {
            containerExtension = container;
            LogingCommand = new RelayParameterizedCommand(async (p) => await ProcessLoging(p));
        }

        private async Task ProcessLoging(object parameter) 
        {
            if (parameter is IHavePassword havePassword)
            {
                IApplicationController controller = containerExtension.Resolve<IApplicationController>();
                await controller.LogingApplication(new LoginInfor());
            }
            await Task.Delay(1000);
        }
    }
}
