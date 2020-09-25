using System;

namespace Wingcode.Data.Rest.Model
{


	/// <summary>
	/// The persistent class for the store_infor database table.
	/// 
	/// </summary>

	public class StoreInfor
	{

		public long id { get; set; }

		public decimal availableQuantity { get; set; }

		public decimal cost { get; set; }

		public DateTime createdAt { get; set; }

		public decimal discount { get; set; }

		public decimal reorderLevel { get; set; }

		public decimal retailPrice { get; set; }

		public DateTime updatedAt { get; set; }

		public decimal wholesalePrice { get; set; }

		// bi-directional many-to-one association to Branch

		public Branch branch { get; set; }

		// bi-directional many-to-one association to Item

		public Item item { get; set; }

	}
}