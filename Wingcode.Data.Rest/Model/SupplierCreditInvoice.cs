using System;

namespace Wingcode.Data.Rest.Model
{


	/// <summary>
	/// The persistent class for the supplier_credit_invoice database table.
	/// 
	/// </summary>

	public class SupplierCreditInvoice
	{

		public long id { get; set; }

		public decimal balanceAmount { get; set; }

		public DateTime createdAt { get; set; }

		public decimal creditAmount { get; set; }

		public decimal paidAmount { get; set; }

		public DateTime updatedAt { get; set; }

		public int dueDays { get; set; }

		public Purchase purchase { get; set; }

		public SupplierCreditAccount supplierCreditAccount { get; set; }

	}
}