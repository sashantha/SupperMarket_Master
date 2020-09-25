using System;

namespace Wingcode.Data.Rest.Model
{


	/// <summary>
	/// The persistent class for the purchase_item database table.
	/// 
	/// </summary>

	public class PurchaseItem
	{

		public long id { get; set; }

		public decimal amount { get; set; }

		public decimal cost { get; set; }

		public DateTime createdAt { get; set; }

		public decimal discount { get; set; }

		public DateTime expireDate { get; set; }

		public decimal freeQuantity { get; set; }

		public DateTime manufactureDate { get; set; }

		public DateTime purchaseDate { get; set; }

		public decimal purchaseQuantity { get; set; }

		public string purchaseType { get; set; }

		public decimal quantity { get; set; }

		public decimal realQuantity { get; set; }

		public decimal reorderLevel { get; set; }

		public decimal retailPrice { get; set; }

		public DateTime updatedAt { get; set; }

		public decimal wholesalePrice { get; set; }

		// bi-directional many-to-one association to Item

		public Item item { get; set; }

		// bi-directional many-to-one association to Purchase

		public Purchase purchase { get; set; }

	}
}