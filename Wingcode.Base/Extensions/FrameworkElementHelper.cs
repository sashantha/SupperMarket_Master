using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Wingcode.Base.Extensions
{
    public static class FrameworkElementHelper
    {

        public static void RemoveFromParent(this FrameworkElement item)
        {
            if (item != null)
            {
                var parentItemsControl = (ItemsControl)item.Parent;
                if (parentItemsControl != null)
                {
                    parentItemsControl.Items.Remove(item as UIElement);
                }
            }
        }
    }
}
