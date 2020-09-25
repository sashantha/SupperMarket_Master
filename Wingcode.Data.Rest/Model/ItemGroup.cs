using System;

namespace Wingcode.Data.Rest.Model
{


	/// <summary>
	/// The persistent class for the item_group database table.
	/// 
	/// </summary>

	public class ItemGroup
	{

		public int id { get; set; }

		public DateTime createdAt { get; set; }

		public string groupName { get; set; }

		public DateTime updatedAt { get; set; }

	}
}