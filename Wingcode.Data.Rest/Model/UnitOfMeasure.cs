using System;

namespace Wingcode.Data.Rest.Model
{


	/// <summary>
	/// The persistent class for the unit_of_measure database table.
	/// 
	/// </summary>

	public class UnitOfMeasure
	{

		public int id { get; set; }

		public DateTime createdAt { get; set; }

		public decimal measureQuantity { get; set; }

		public string measureType { get; set; }

		public string measureUnit { get; set; }

		public decimal perOneUnit { get; set; }

		public string purchaseUnit { get; set; }

		public string saleUnit { get; set; }

		public DateTime updatedAt { get; set; }

		// bi-directional one-to-one association to Item

		public Item item { get; set; }

	}
}