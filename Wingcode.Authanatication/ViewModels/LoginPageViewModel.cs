using CommonServiceLocator;
using Prism.Commands;
using Prism.Ioc;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security;
using System.Threading.Tasks;
using System.Windows.Input;
using Wingcode.Authanatication.Views;
using Wingcode.Base;
using Wingcode.Base.Api;
using Wingcode.Base.DataModel;
using Wingcode.Base.Dialog;
using Wingcode.Base.Extensions;
using Wingcode.Base.FileSystem;
using Wingcode.Base.Input;
using Wingcode.Base.Security;
using Wingcode.Data.Rest.Model;
using Wingcode.Data.Rest.Service;

namespace Wingcode.Authanatication.ViewModels
{
    public class LoginPageViewModel : BaseViewModel
    {

        private IContainerExtension containerExtension;
        private ObservableCollection<Branch> branches;


        public string UserName { get; set; }

        public bool LoginIsRunning { get; set; }

        public string Warrning { get; set; }

        public ICommand LogingCommand { get; set; }

        public ICommand WarrnChangeCommand { get; set; }

        public List<string> BranchNames { get; set; }

        public string SelectedBranch { get; set; }

        public LoginPageViewModel(IContainerExtension container)
        {
            containerExtension = container;
            WarrnChangeCommand = new DelegateCommand(() => Warrning = "Loging Here");
            LogingCommand = new RelayParameterizedCommand(async (p) => await ProcessLoging(p));
            LoadBranchNames();
        }

        private async void LoadBranchNames()
        {
            IRestDataMapper mapper = containerExtension.Resolve<IRestDataMapper>();
            branches = await BranchRestService.GetAllBranchAsync(mapper);
            BranchNames = branches.Select(b => b.name).ToList();
        }

        private async Task ProcessLoging(object parameter)
        {
            IRestDataMapper mapper = containerExtension.Resolve<IRestDataMapper>();
            if (SelectedBranch == null || UserName == null)
            {
                Warrning = "Invalid Branch Or User Name";
                return;
            }
            if (parameter is IHavePassword havePassword)
            {
                string userPassword = havePassword.Password;
                if (string.IsNullOrEmpty(userPassword))
                {
                    Warrning = "Invalid Password Or User Name";
                    return;
                }

                Branch b = branches.Where(br => br.name.Equals(SelectedBranch)).FirstOrDefault();
                User user = await UserRestService.GetUserByParamAndBranchIdAsync(mapper, UserName, b.id);

                if (user != null)
                {
                    if (user.id > 0)
                    {
                        if (userPassword.ComparePassword(user.password))
                        {
                            IApplicationController controller = containerExtension.Resolve<IApplicationController>();
                            await controller.LogingApplication(new LoggedUserProvider(user));
                        }
                        else
                        {
                            Warrning = "Invalid Password Or User Name";
                            UserName = string.Empty;
                            havePassword.ClearPassword();
                            return;
                        }
                    }
                    else
                    {
                        Warrning = "Invalid Password Or User Name";
                        UserName = string.Empty;
                        havePassword.ClearPassword();
                        return;
                    }
                }
                else
                {
                    Warrning = "Invalid Password Or User Name";
                    UserName = string.Empty;
                    havePassword.ClearPassword();
                    return;
                }
            }
            else 
            {
                Warrning = "Internal Error Please Contact Developer.";
                return;
            }
            await Task.Delay(1000);
        }
    }
}
