using System;
using System.Collections.ObjectModel;

namespace Wingcode.Data.Rest.Model
{

    public class CustomerCreditInvoice
	{

		public long id;

		public decimal balanceAmount;

		public DateTime createdAt;

		public decimal creditAmount;

		public decimal paidAmount;

		public DateTime updatedAt;

		public CustomerCreditAccount customerCreditAccount;

		public ObservableCollection<CustomerPayment> customerPayments;

	}
}