using System;

namespace Wingcode.Data.Rest.Model
{


	/// <summary>
	/// The persistent class for the purchase database table.
	/// 
	/// </summary>

	public class Purchase
	{

		public long id { get; set; }

		public decimal costAmount { get; set; }

		public DateTime createdAt { get; set; }

		public decimal creditAmount { get; set; }

		public decimal discountPercent { get; set; }

		public decimal invoiceAmount { get; set; }

		public string invoiceNo { get; set; }

		public string invoiceType { get; set; }

		public decimal netAmount { get; set; }

		public decimal payAmount { get; set; }

		public string payMethod { get; set; }

		public DateTime purchaseDate { get; set; }

		public decimal purchaseDiscount { get; set; }

		public string recordState { get; set; }

		public int totalPurchaseItem { get; set; }

		public DateTime updatedAt { get; set; }

		public Branch branch { get; set; }

		public Supplier supplier { get; set; }

		public User user { get; set; }

	}
}