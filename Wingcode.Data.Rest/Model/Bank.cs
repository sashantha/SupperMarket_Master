using System;

namespace Wingcode.Data.Rest.Model
{


	/// <summary>
	/// The persistent class for the bank database table.
	/// 
	/// </summary>
	public class Bank
	{

		public int id { get; set; }

		public DateTime createdAt { get; set; }

		public string name { get; set; }

		public DateTime updatedAt { get; set; }

	}
}