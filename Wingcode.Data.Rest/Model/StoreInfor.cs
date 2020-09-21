using System;

namespace Wingcode.Data.Rest.Model
{

	public class StoreInfor
	{
		public long id;

		public decimal availableQuantity;

		public decimal cost;

		public DateTime createdAt;

		public decimal discount;

		public decimal reorderLevel;

		public decimal retailPrice;

		public DateTime updatedAt;

		public decimal wholesalePrice;

		public Branch branch;

		public Item item;

	}
}