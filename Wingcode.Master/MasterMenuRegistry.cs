using System.Collections.Generic;
using Wingcode.Base.Api;
using Wingcode.Base.ViewModels;
using Wingcode.Master.Views;

namespace Wingcode.Master
{
    internal class MasterMenuRegistry : IMenuRegistry
    {
        public List<MenuItemViewModel> GetMenuRegistry()
        {
            return null;
        }

        public Dictionary<string, List<SubMenuItemViewModel>> GetSubMenuRegistry()
        {
            List<SubMenuItemViewModel> subs = new List<SubMenuItemViewModel>
            {
                new SubMenuItemViewModel()
                {
                    Index = 0,
                    Name = "Master Value Register",
                    AttachedControl = typeof(MasterRegister)
                }
            };
            Dictionary<string, List<SubMenuItemViewModel>> dic = new Dictionary<string, List<SubMenuItemViewModel>>
            {
                { "Registers", subs }
            };
            return dic;
        }
    }
}
