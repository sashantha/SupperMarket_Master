using System;
using System.Collections.ObjectModel;

namespace Wingcode.Data.Rest.Model
{

    public class ItemGroup
	{

		public int id;

		public DateTime createdAt;

		public string groupName;

		public DateTime updatedAt;

		public ObservableCollection<Item> items;

	}
}