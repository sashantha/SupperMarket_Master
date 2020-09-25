using System;

namespace Wingcode.Data.Rest.Model
{


	/// <summary>
	/// The persistent class for the cheque_book database table.
	/// 
	/// </summary>
	public class ChequeBook
	{

		public long id { get; set; }

		public string bookDescription { get; set; }

		public string bookType { get; set; }

		public decimal chequeAmount { get; set; }

		public string chequeNo { get; set; }

		public string chequeStatus { get; set; }

		public DateTime createdAt { get; set; }

		public string description { get; set; }

		public DateTime releseDate { get; set; }

		public DateTime transactionDate { get; set; }

		public DateTime updatedAt { get; set; }

		public Branch branch { get; set; }

		public BranchAccount branchAccount { get; set; }

		public Purchase purchase { get; set; }

		public SaleInvoice saleInvoice { get; set; }

		public User user { get; set; }

	}
}