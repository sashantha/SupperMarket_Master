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

        public static UnitOfMeasurement CreateNewUnitOfMeasurement(this UnitOfMeasurement uom)
        {
            return uom = new UnitOfMeasurement()
            {
                id = 0,
                unitDescription = string.Empty,
                unitType = string.Empty,
                baseUnitName = string.Empty,
                basePrecision = 0.000m,
                baseRatio = 0.000m,
                purchaseUnitName = string.Empty,
                purchasePrecision = 0.000m,
                purchasePrecisionUnitName = string.Empty,
                baseRatioToPurchase = 0.000m,
                purchaseQuantifyValue = 0.000m,
                saleUnitName = string.Empty,
                salePrecision = 0.000m,
                baseRatioToSale = 0.000m,
                saleOtherUnitName = string.Empty
            };
        }

        public static bool IsValidUom(this UnitOfMeasurement uom) 
        {
            return (!string.IsNullOrEmpty(uom.unitDescription)
            && !string.IsNullOrEmpty(uom.unitType)
            && !string.IsNullOrEmpty(uom.baseUnitName)
            && (uom.basePrecision > 0)
            && (uom.baseRatio > 0)
            && !string.IsNullOrEmpty(uom.purchaseUnitName)
            && (uom.purchasePrecision > 0)
            && !string.IsNullOrEmpty(uom.purchasePrecisionUnitName)
            && (uom.baseRatioToPurchase > 0)
            && !string.IsNullOrEmpty(uom.saleUnitName)
            && (uom.salePrecision > 0)
            && (uom.baseRatioToSale > 0));
        }
    }
}
