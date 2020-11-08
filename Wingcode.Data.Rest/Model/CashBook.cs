using System;
using Wingcode.Base.DataModel;

namespace Wingcode.Data.Rest.Model
{


	/// <summary>
	/// The persistent class for the cash_book database table.
	/// 
	/// </summary>
	public class CashBook : ModelBase<CashBook>
	{

		public long id { get; set; }

		public decimal amount { get; set; }

		public string bookType { get; set; }

		public DateTime createdAt { get; set; }

		public string description { get; set; }

		public DateTime transactionDate { get; set; }

		public DateTime updatedAt { get; set; }

		public Branch branch { get; set; }

		public BranchAccount branchAccount { get; set; }

		public Purchase purchase { get; set; }

		public SaleInvoice saleInvoice { get; set; }

		public User user { get; set; }

	}
}