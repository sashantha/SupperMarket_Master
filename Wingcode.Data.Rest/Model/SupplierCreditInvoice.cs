using System;
using System.Collections.ObjectModel;

namespace Wingcode.Data.Rest.Model
{

    public class SupplierCreditInvoice
	{

		public long id;

		public decimal balanceAmount;

		public DateTime createdAt;

		public decimal creditAmount;

		public decimal paidAmount;

		public DateTime updatedAt;

		public SupplierCreditAccount supplierCreditAccount;

		public ObservableCollection<SupplierPayment> supplierPayments;

	}
}