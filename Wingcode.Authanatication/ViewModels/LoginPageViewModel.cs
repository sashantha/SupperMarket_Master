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

        private string userName;

        public string UserName { get => userName; set => SetProperty(ref userName, value); }

        private bool loginIsRunning;

        public bool LoginIsRunning { get => loginIsRunning; set => SetProperty(ref loginIsRunning, value); }

        private string warrning = "Loging Here";
        public string Warrning { get => warrning; set => SetProperty(ref warrning, value); }

        private ICommand logingCommand;
        public ICommand LogingCommand { get => logingCommand; set => SetProperty(ref logingCommand, value); }

        private ICommand warrnChangeCommand;
        public ICommand WarrnChangeCommand { get => warrnChangeCommand; set => SetProperty(ref warrnChangeCommand, value); }

        private List<string> branchNames;
        public List<string> BranchNames { get => branchNames; set => SetProperty(ref branchNames, value); }

        private string selectedBranch;
        public string SelectedBranch { get => selectedBranch; set => SetProperty(ref selectedBranch, value); }

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
