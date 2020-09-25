using System;

namespace Wingcode.Data.Rest.Model
{


	/// <summary>
	/// The persistent class for the supplier_payment database table.
	/// 
	/// </summary>

	public class SupplierPayment
	{

		public long id { get; set; }

		public decimal amonut { get; set; }

		public DateTime createdAt { get; set; }

		public DateTime payDate { get; set; }

		public string payMethod { get; set; }

		public DateTime updatedAt { get; set; }

		// bi-directional many-to-one association to SupplierCreditInvoice

		public SupplierCreditInvoice supplierCreditInvoice { get; set; }
	}
}