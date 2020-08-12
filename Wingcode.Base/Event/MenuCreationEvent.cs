using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wingcode.Base.ViewModels;

namespace Wingcode.Base.Event
{
    public class MenuCreationEvent : PubSubEvent<List<MenuItemViewModel>>
    {
    }
}
