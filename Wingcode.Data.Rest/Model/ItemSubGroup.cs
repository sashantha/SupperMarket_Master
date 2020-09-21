using System;
using System.Collections.ObjectModel;

namespace Wingcode.Data.Rest.Model
{

    public class ItemSubGroup
	{

		public int id;

		public DateTime createdAt;

		public string subGroupName;

		public DateTime updatedAt;

		public ObservableCollection<Item> items;

	}
}