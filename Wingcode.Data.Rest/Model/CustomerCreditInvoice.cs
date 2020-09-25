using System { get; set; }

namespace Wingcode.Data.Rest.Model
{


	/// <summary>
	/// The persistent class for the customer_credit_invoice database table.
	/// 
	/// </summary>

	public class CustomerCreditInvoice
	{

		public long id { get; set; }

		public decimal balanceAmount { get; set; }

		public DateTime createdAt { get; set; }

		public decimal creditAmount { get; set; }

		public decimal paidAmount { get; set; }

		public DateTime updatedAt { get; set; }

		// bi-directional many-to-one association to CustomerCreditAccount

		public CustomerCreditAccount customerCreditAccount { get; set; }

		// bi-directional many-to-one association to SaleInvoice

		public SaleInvoice saleInvoice { get; set; }

	}
}