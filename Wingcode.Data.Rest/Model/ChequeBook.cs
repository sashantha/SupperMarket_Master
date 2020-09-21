using System;

namespace Wingcode.Data.Rest.Model
{

	public class ChequeBook
	{

		public long id;

		public string bookDescription;

		public string bookType;

		public decimal chequeAmount;

		public string chequeNo;

		public string chequeStatus;

		public DateTime createdAt;

		public string description;

		public DateTime transactionDate;

		public DateTime updatedAt;

		public Branch branch;

		public Purchase purchase;

		public SaleInvoice saleInvoice;

	}
}