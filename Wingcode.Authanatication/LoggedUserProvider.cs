using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wingcode.Data.Rest.Model;
using Wingcode.Data.Rest.Service;

namespace Wingcode.Authanatication
{
    internal class LoggedUserProvider : ILoggedUserProvider
    {
        public User LoggedUser { get; private set; }

        public LoggedUserProvider(User loggedUser)
        {
            LoggedUser = loggedUser;
        }
    }
}
