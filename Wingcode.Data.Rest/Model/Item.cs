using System;

namespace Wingcode.Data.Rest.Model
{


	/// <summary>
	/// The persistent class for the item database table.
	/// 
	/// </summary>

	public class Item
	{

		public long id { get; set; }

		public string barcode { get; set; }

		public string category { get; set; }

		public string code { get; set; }

		public DateTime createdAt { get; set; }

		public string name { get; set; }

		public string otherName { get; set; }

		public DateTime updatedAt { get; set; }

		// bi-directional many-to-one association to ItemGroup

		public ItemGroup itemGroup { get; set; }

		// bi-directional many-to-one association to ItemSubGroup

		public ItemSubGroup itemSubGroup { get; set; }

		// bi-directional one-to-one association to UnitOfMeasure

		public UnitOfMeasurement unitOfMeasurement { get; set; }

	}
}