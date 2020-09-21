using System;

namespace Wingcode.Data.Rest.Model
{

	public class CustomerPayment
	{

		public long id;

		public decimal amount;

		public DateTime createdAt;

		public DateTime payDate;

		public string payMethod;

		public DateTime updatedAt;

		public CustomerCreditInvoice customerCreditInvoice;

	}
}