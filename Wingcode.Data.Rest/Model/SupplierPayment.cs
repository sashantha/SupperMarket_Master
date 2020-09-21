using System;

namespace Wingcode.Data.Rest.Model
{

	public class SupplierPayment
	{

		public long id;

		public decimal amonut;

		public DateTime createdAt;

		public DateTime payDate;

		public string payMethod;

		public DateTime updatedAt;

		public SupplierCreditInvoice supplierCreditInvoice;

	}
}