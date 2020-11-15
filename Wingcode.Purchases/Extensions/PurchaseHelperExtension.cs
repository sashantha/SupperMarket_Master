using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
                chqAmount = 0.00m,
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
                purchaseDate = owner.purchaseDate,
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

        public static ChequeBook CreateNewChequeBook(this ChequeBook book, Branch br, User us, Purchase pu) 
        {
            return book = new ChequeBook()
            {
                id = 0,
                bookDescription = $"Paid to Purchase Invoice {pu.invoiceNo} of {pu.supplier.name}",
                bookType = ConstValues.BT_DEBITED,
                chequeNo = string.Empty,
                transactionDate = pu.purchaseDate,
                description = string.Empty,
                chequeAmount = pu.chqAmount,
                chequeStatus = ConstValues.CHQ_STAT[0],
                branch = br,
                user = us,
                purchase = pu,
                branchAccount = null,
                releseDate = DateTime.Now,
                saleInvoice = null
            };
        }

        public static CashBook CreateNewCashBook(this CashBook book, Branch br, User us, Purchase pu) 
        {
            return book = new CashBook()
            {
                id = 0,
                transactionDate = pu.purchaseDate,
                bookType = ConstValues.BT_DEBITED,
                amount = pu.payAmount,
                description = $"Paid to Purchase Invoice {pu.invoiceNo} of {pu.supplier.name}",
                branchAccount = null,
                purchase = pu,
                branch = br,
                user = us,
                saleInvoice = null
            };
        }

        public static SupplierCreditInvoice CreateNewCreditInvoice(this SupplierCreditInvoice scinv, Branch br, User us, Purchase pu) 
        {
            return scinv = new SupplierCreditInvoice() 
            {
                id = 0,
                dueDays = 0,
                creditAmount = pu.creditAmount,
                paidAmount = 0.00m,
                balanceAmount = pu.creditAmount,
                purchase = pu,
                supplierCreditAccount = pu.supplier.supplierCreditAccount
            };
        }

        public static PurchaseItem Merge(this PurchaseItem pui, PurchaseItem puiExist)
        {
            puiExist.amount += pui.amount;
            puiExist.cost = pui.cost;
            puiExist.createdAt = pui.createdAt;
            puiExist.discount = pui.discount;
            puiExist.expireDate = pui.expireDate;
            puiExist.freeQuantity += pui.freeQuantity;
            puiExist.manufactureDate = pui.manufactureDate;
            puiExist.purchaseDate = pui.purchaseDate;
            puiExist.purchaseType = pui.purchaseType;
            puiExist.quantity += pui.quantity;
            puiExist.realQuantity += pui.realQuantity;
            puiExist.availableQuantity += pui.availableQuantity;
            puiExist.reorderLevel = pui.reorderLevel;
            puiExist.retailPrice = pui.retailPrice;
            puiExist.updatedAt = pui.updatedAt;
            puiExist.wholesalePrice = pui.wholesalePrice;
            puiExist.recordState = pui.recordState;
            puiExist.item = pui.item;
            puiExist.purchase = pui.purchase;
            return puiExist;
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

        public static void CalaculatePurchase(this Purchase cp, ObservableCollection<PurchaseItem> items)
        {
            decimal amt = 0.00m;
            foreach (var i in items)
            {
                amt += i.amount;
            }
            cp.costAmount = amt;
            cp.invoiceAmount = amt;
            cp.totalPurchaseItem = items.Count;
        }
    }
}
