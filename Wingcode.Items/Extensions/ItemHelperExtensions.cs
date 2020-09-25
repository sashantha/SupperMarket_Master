using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wingcode.Data.Rest.Model;

namespace Wingcode.Items.Extensions
{
    internal static class ItemHelperExtensions
    {
        public static StoreInfor CreateNewStore(this StoreInfor s) 
        {
            s = new StoreInfor() 
            {
                id = 0,
                cost = 0.00m,
                wholesalePrice = 0.00m,
                retailPrice = 0.00m,
                availableQuantity = 0.00m,
                reorderLevel = 0.00m,
                discount = 0.00m,
            };
            return s;
        }

        public static Item CreateNewItem(this Item i) 
        {
            i = new Item()
            {
                id = 0,
                category = string.Empty,
                code = string.Empty,
                barcode = string.Empty,
                name = string.Empty,
                otherName = string.Empty
            };            
            return i;
        }

        public static bool IsValiedItem(this Item i) 
        {
            return (!string.IsNullOrEmpty(i.category) || 
                !string.IsNullOrEmpty(i.code) || 
                !string.IsNullOrEmpty(i.name) || 
                i.itemGroup != null || i.itemSubGroup != null);
        }
    }
}
