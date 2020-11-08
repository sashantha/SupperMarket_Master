using Wingcode.Base.DataModel;

namespace Wingcode.Data.Rest.Model
{
	public class CustomerCriteria : ModelBase<CustomerCriteria>
	{

		public long id { get; set; }
		public string code { get; set; }
		public string name { get; set; }
	}

}