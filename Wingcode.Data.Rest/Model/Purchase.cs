using System;
using System.Collections.ObjectModel;

namespace Wingcode.Data.Rest.Model
{

    public class Purchase
	{

		public long id;

		public DateTime createdAt;

		public decimal creditAmount;

		public double discountPercent;

		public decimal invoiceAmount;

		public string invoiceNo;

		public string invoiceType;

		public decimal netAmount;

		public decimal payAmount;

		public string payMethod;

		public DateTime purchaseDate;

		public decimal purchaseDiscount;

		public string recordState;

		public int totalPurchaseItem;

		public DateTime updatedAt;

		public ObservableCollection<CashBook> cashBooks;

		public ObservableCollection<ChequeBook> chequeBooks;

		public Branch branch;

		public Supplier supplier;

		public User user;

		public ObservableCollection<PurchaseItem> purchaseItems;

	}
}