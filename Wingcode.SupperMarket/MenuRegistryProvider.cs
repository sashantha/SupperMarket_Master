using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wingcode.Base;
using Wingcode.Base.Api;
using Wingcode.Base.ViewModels;
using Wingcode.SupperMarket.Views;

namespace Wingcode.SupperMarket
{
    internal class MenuRegistryProvider : IMenuRegistryProvider
    {

        private readonly Dictionary<string, MenuItemViewModel> Registry = new Dictionary<string, MenuItemViewModel>();
        private readonly List<IMenuRegistry> menuRegistries = new List<IMenuRegistry>();

        public MenuRegistryProvider()
        {
            menuRegistries.Clear();
        }

        public MenuItemViewModel GetMenu(string name)
        {
            MenuItemViewModel mv = new MenuItemViewModel()
            {
                AttachedControl = typeof(EmptyView)
            };
            if (Registry.ContainsKey(name))
                return Registry[name];
            return mv;
        }

        public List<MenuItemViewModel> GetMenuItemViews()
        {
            List<MenuItemViewModel> reg = Registry.Values.ToList();
            return reg;
        }

        public async Task InitializeMenu()
        {
            Registry.Clear();
            CreateDefualts();
            Register(new MainModuleMenusRegistry());
            MergeRegistry();
            await Task.Delay(0);
        }

        public void Register(IMenuRegistry registry)
        {
            if (!menuRegistries.Contains(registry))
                menuRegistries.Add(registry);
        }

        private void CreateDefualts()
        {
            int i = 0;
            foreach (string str in ConstValues.MENUITEMTEXTARRAY)
            {
                Registry.Add(str, new MenuItemViewModel()
                {
                    Index = i,
                    Text = str,
                    Name = str,
                    IconKind = ConstValues.PACKICONKINDS[i],
                    AttachedControl = typeof(EmptyView)
                });
                i++;
            }
        }

        private void MergeRegistry()
        {
            foreach (IMenuRegistry registry in menuRegistries)
            {
                if (registry.GetMenuRegistry() != null)
                {

                }
                if (registry.GetSubMenuRegistry() != null)
                {
                    Dictionary<string, List<SubMenuItemViewModel>> pairs = registry.GetSubMenuRegistry();
                    foreach (var item in pairs)
                    {
                        List<SubMenuItemViewModel> subs = item.Value;
                        MenuItemViewModel menu = Registry[item.Key];
                        if (menu.Childs == null)
                            menu.Childs = new List<SubMenuItemViewModel>();
                        foreach (SubMenuItemViewModel sm in subs)
                        {
                            sm.ParentName = menu.Name;
                            menu.Childs.Add(sm);
                        }
                    }
                }
            }
            foreach (var item in Registry.Keys)
            {
                List<SubMenuItemViewModel> slist = Registry[item].Childs?.OrderBy(i => i.Index).ToList();
                Registry[item].Childs = slist;
            }
        }
    }
}
