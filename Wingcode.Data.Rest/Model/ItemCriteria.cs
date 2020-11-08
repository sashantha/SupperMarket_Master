using Wingcode.Base.DataModel;

namespace Wingcode.Data.Rest.Model
{
	public class ItemCriteria : ModelBase<ItemCriteria>
	{

		public long id { get; set; }
		public string code { get; set; }
		public string barcode { get; set; }
		public string name { get; set; }
		public string otherName { get; set; }

	}

}