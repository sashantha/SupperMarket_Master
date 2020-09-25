using System;

namespace Wingcode.Data.Rest.Model
{


	/// <summary>
	/// The persistent class for the item_sub_group database table.
	/// 
	/// </summary>

	public class ItemSubGroup
	{

		public int id { get; set; }

		public DateTime createdAt { get; set; }

		public string subGroupName { get; set; }

		public DateTime updatedAt { get; set; }

	}
}