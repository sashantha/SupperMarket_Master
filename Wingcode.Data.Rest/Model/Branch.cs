using System;

namespace Wingcode.Data.Rest.Model
{


	/// <summary>
	/// The persistent class for the branch database table.
	/// 
	/// </summary>
	public class Branch
	{

		public int id { get; set; }

		public string address { get; set; }

		public string code { get; set; }

		public string companyName { get; set; }

		public string contact { get; set; }

		public DateTime createdAt { get; set; }

		public string name { get; set; }

		public DateTime updatedAt { get; set; }

	}
}