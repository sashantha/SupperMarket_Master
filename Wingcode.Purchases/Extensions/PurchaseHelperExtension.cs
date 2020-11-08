using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wingcode.Base;
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
                recordState = ConstValues.RCS_NEW,
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
                defectQuantity = 0.000m,
                defectState = ConstValues.DFS_NON,
                item = item,
                purchase = owner
            };
        }

        public static SupplierCriteria ToSupplierCriterita(this Supplier sup)
        {
            if (sup.id > 0)
                return new SupplierCriteria()
                {
                    id = sup.id,
                    code = sup.code,
                    name = sup.name
                };
            else
                return new SupplierCriteria() 
                {
                    id = 0,
                    code = string.Empty,
                    name = string.Empty
                };
        }

        public static ItemCriteria ToItemCriteria(this Item i)
        {
            if (i.id > 0)
                return new ItemCriteria()
                {
                    id = i.id,
                    code = i.code,
                    name = i.name,
                    barcode = i.barcode,
                    otherName = i.otherName
                };
            else
                return new ItemCriteria()
                {
                    id = 0,
                    code = string.Empty,
                    name = string.Empty,
                    barcode = string.Empty,
                    otherName = string.Empty
                };
        }

        public static void CalculateCostWithDiscount(this PurchaseItem puri) 
        {
            if (puri.cost > 0.00m && puri.discount > 0.00m) 
            {
                decimal c = puri.cost;
                puri.cost = c - ((puri.discount * c) / 100);
            }
        }
    }
}
