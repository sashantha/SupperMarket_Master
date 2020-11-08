using System;
using Wingcode.Base.DataModel;

namespace Wingcode.Data.Rest.Model
{


	/// <summary>
	/// The persistent class for the supplier_credit_account database table.
	/// 
	/// </summary>

	public class SupplierCreditAccount : ModelBase<SupplierCreditAccount>
	{

		public long id { get; set; }

		public DateTime createdAt { get; set; }

		public decimal totalCredit { get; set; }

		public DateTime updatedAt { get; set; }				

	}
}