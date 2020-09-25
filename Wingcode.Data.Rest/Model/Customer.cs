using System;

namespace Wingcode.Data.Rest.Model
{


	/// <summary>
	/// The persistent class for the customer database table.
	/// 
	/// </summary>
	public class Customer
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

		public CustomerCreditAccount customerCreditAccount { get; set; }


	}
}