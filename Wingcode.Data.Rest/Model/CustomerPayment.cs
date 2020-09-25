using System;

namespace Wingcode.Data.Rest.Model
{


	/// <summary>
	/// The persistent class for the customer_payment database table.
	/// 
	/// </summary>

	public class CustomerPayment
	{

		public long id { get; set; }

		public decimal amount { get; set; }

		public DateTime createdAt { get; set; }

		public DateTime payDate { get; set; }

		public string payMethod { get; set; }

		public DateTime updatedAt { get; set; }

		// bi-directional many-to-one association to CustomerCreditInvoice

		public CustomerCreditInvoice customerCreditInvoice { get; set; }

	}
}