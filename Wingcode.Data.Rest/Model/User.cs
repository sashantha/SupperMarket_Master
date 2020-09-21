using System;
using System.Collections.ObjectModel;

namespace Wingcode.Data.Rest.Model
{

    public class User
	{

		public int id { get; set; }

		public DateTime createdAt { get; set; }

		public string email { get; set; }

		public string name { get; set; }

		public string password { get; set; }

		public DateTime updatedAt { get; set; }

		public string userRole { get; set; }

		public Branch branch { get; set; }

	}
}