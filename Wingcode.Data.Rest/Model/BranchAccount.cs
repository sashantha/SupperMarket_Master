using System;

namespace Wingcode.Data.Rest.Model
{


	/// <summary>
	/// The persistent class for the branch_account database table.
	/// 
	/// </summary>
	public class BranchAccount
	{

		public int id { get; set; }

		public string accountNo { get; set; }

		public DateTime createdAt { get; set; }

		public DateTime updatedAt { get; set; }

		public Bank bank { get; set; }

		public Branch branch { get; set; }

	}
}