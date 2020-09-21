using System;
using System.Collections.ObjectModel;

namespace Wingcode.Data.Rest.Model
{

    public class SaleInvoice
	{

		public long id;

		public decimal balanceAmount;

		public DateTime createdAt;

		public decimal creditAmount;

		public DateTime invoiceDate;

		public decimal invoiceDiscount;

		public string invoiceNo;

		public string invoiceState;

		public string invoiceType;

		public decimal netAmount;

		public decimal paidAmount;

		public string payMethod;

		public string recordState;

		public string saleType;

		public decimal totalAmount;

		public decimal totalCost;

		public decimal totalDiscount;

		public decimal totalProfit;

		public DateTime updatedAt;

		public ObservableCollection<CashBook> cashBooks;

		public ObservableCollection<ChequeBook> chequeBooks;

		public Branch branch;

		public CustomerCriteria customer;

		public User user;

		public ObservableCollection<SaleItem> saleItems;

	}
}