using System;

namespace Wingcode.Data.Rest.Model
{


	/// <summary>
	/// The persistent class for the measurement database table.
	/// 
	/// </summary>

	public class Unit
	{

		public int id { get; set; }

		public DateTime createdAt { get; set; }

		public string unitName { get; set; }

		public string unitType { get; set; }

		public string abbreviation { get; set; }

		public DateTime updatedAt { get; set; }

	}
}