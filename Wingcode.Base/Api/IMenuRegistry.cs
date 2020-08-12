using System.Collections.Generic;
using Wingcode.Base.ViewModels;

namespace Wingcode.Base.Api
{
    public interface IMenuRegistry
    {

        List<MenuItemViewModel> GetMenuRegistry();

        Dictionary<string, List<SubMenuItemViewModel>> GetSubMenuRegistry();
    }
}
