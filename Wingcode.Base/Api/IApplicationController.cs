using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Wingcode.Base.Api
{
    public interface IApplicationController
    {

        bool IsInitilized { get; set; }
                
        Task InitializeApplication();

        Task LogingApplication(object loggedUser);

        Task ShowMainView();

        Task ShowView(Type viewType);

        Task LogoutApllication();

        Task RestartApplication();

        Task ShutdownApplication();
    }
}
