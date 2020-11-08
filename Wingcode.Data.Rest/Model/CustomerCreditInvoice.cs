using System;
using Wingcode.Base.DataModel;

namespace Wingcode.Data.Rest.Model
{


	/// <summary>
	/// The persistent class for the customer_credit_invoice database table.
	/// 
	/// </summary>

	public class CustomerCreditInvoice : ModelBase<CustomerCreditInvoice>
	{

		public long id { get; set; }

		public decimal balanceAmount { get; set; }

		public DateTime createdAt { get; set; }

		public decimal creditAmount { get; set; }

		public decimal paidAmount { get; set; }

		public DateTime updatedAt { get; set; }

		public int dueDays { get; set; }

		public CustomerCreditAccount customerCreditAccount { get; set; }

		public SaleInvoice saleInvoice { get; set; }

	}
}