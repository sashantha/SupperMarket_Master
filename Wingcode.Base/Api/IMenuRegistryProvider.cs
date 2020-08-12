using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wingcode.Base.ViewModels;

namespace Wingcode.Base.Api
{
    public interface IMenuRegistryProvider
    {

        Task InitializeMenu();

        void Register(IMenuRegistry registry);

        List<MenuItemViewModel> GetMenuItemViews();

        MenuItemViewModel GetMenu(string name);
    }
}
