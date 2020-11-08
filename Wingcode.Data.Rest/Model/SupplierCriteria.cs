using Wingcode.Base.DataModel;

namespace Wingcode.Data.Rest.Model
{
	public class SupplierCriteria : ModelBase<SupplierCriteria>
	{

		public long id { get; set; }
		public string code { get; set; }
		public string name { get; set; }

	}

}