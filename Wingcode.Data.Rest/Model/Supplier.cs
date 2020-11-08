using System;
using Wingcode.Base.DataModel;

namespace Wingcode.Data.Rest.Model
{


	/// <summary>
	/// The persistent class for the supplier database table.
	/// 
	/// </summary>

	public class Supplier : ModelBase<Supplier>
	{

		public long id { get; set; }

		public string address { get; set; }

		public string code { get; set; }

		public string contact { get; set; }

		public DateTime createdAt { get; set; }

		public string description { get; set; }

		public string name { get; set; }

		public DateTime updatedAt { get; set; }

		// bi-directional many-to-one association to Branch

		public Branch branch { get; set; }

		// bi-directional one-to-one association to SupplierCreditAccount

		public SupplierCreditAccount supplierCreditAccount { get; set; }

	}
}