using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wingcode.Data.Rest.Model;

namespace Wingcode.Purchases.Extensions
{
    internal static class PurchaseHelperExtension
    {
        public static Purchase CreateNew(this Purchase pur)
        {
            return pur = new Purchase()
            {
                id = 0,
                costAmount = 0.00m,
                creditAmount = 0.00m,
                discountPercent = 0.00m,
                invoiceAmount = 0.00m,
                invoiceNo = string.Empty,
                invoiceType = string.Empty,
                netAmount = 0.00m,
                payAmount = 0.00m,
                payMethod = string.Empty,
                purchaseDate = DateTime.Now,
                purchaseDiscount = 0.00m,
                recordState = string.Empty,
                totalPurchaseItem = 0
            };
        }

        public static PurchaseItem CreateNew(this PurchaseItem pui, Purchase owner, Item item)
        {
            return pui = new PurchaseItem()
            {
                id = 0,
                amount = 0.00m,
                cost = 0.00m,
                discount = 0.00m,
                expireDate = DateTime.Now,
                freeQuantity = 0.000m,
                manufactureDate = DateTime.Now,
                purchaseDate = DateTime.Now,
                purchaseQuantity = 0.000m,
                purchaseType = string.Empty,
                quantity = 0.000m,
                realQuantity = 0.000m,
                availableQuantity = 0.000m,
                reorderLevel = 0.00m,
                retailPrice = 0.00m,
                wholesalePrice = 0.00m,
                item = item,
                purchase = owner
            };
        }
    }
}
