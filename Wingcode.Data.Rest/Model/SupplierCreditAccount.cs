using System;

namespace Wingcode.Data.Rest.Model
{


	/// <summary>
	/// The persistent class for the supplier_credit_account database table.
	/// 
	/// </summary>

	public class SupplierCreditAccount
	{

		public long id { get; set; }

		public DateTime createdAt { get; set; }

		public decimal totalCredit { get; set; }

		public DateTime updatedAt { get; set; }				

	}
}