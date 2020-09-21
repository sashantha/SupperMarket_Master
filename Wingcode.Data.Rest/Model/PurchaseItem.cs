using System;

namespace Wingcode.Data.Rest.Model
{

	public class PurchaseItem
	{

		public long id;

		public decimal amount;

		public decimal availableQuantity;

		public decimal cost;

		public DateTime createdAt;

		public decimal discount;

		public DateTime expireDate;

		public decimal freeQuantity;

		public DateTime manufactureDate;

		public DateTime purchaseDate;

		public string purchaseType;

		public decimal quantity;

		public decimal reorderLevel;

		public decimal retailPrice;

		public DateTime updatedAt;

		public decimal wholesalePrice;

		public Purchase purchase;

	}
}