using System;

namespace Wingcode.Data.Rest.Model
{

	public class CashBook
	{

		public long id;

		public decimal amount;

		public string bookType;

		public DateTime createdAt;

		public string description;

		public DateTime transactionDate;

		public DateTime updatedAt;

		public Branch branch;

		public Purchase purchase;

		public SaleInvoice saleInvoice;

	}
}