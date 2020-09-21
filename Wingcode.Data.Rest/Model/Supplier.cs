using System;
using System.Collections.ObjectModel;

namespace Wingcode.Data.Rest.Model
{

    public class Supplier
	{
		public long id { get; set; }

		public string address { get; set; }

		public string code { get; set; }

		public string contact { get; set; }

		public DateTime createdAt { get; set; }

		public string description { get; set; }

		public string name { get; set; }

		public DateTime updatedAt { get; set; }

		public Branch branch { get; set; }

		public SupplierCreditAccount supplierCreditAccount { get; set; }

	}
}