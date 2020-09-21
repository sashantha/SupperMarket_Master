using System;
using System.Collections.ObjectModel;

namespace Wingcode.Data.Rest.Model
{

    public class Item
    {
        public const long serialVersionUID = 1L;

        public long id { get; set; }

        public string category { get; set; }

        public string barcode { get; set; }

        public string code { get; set; }

        public DateTime createdAt { get; set; }

        public string name { get; set; }

        public string otherName { get; set; }

        public DateTime updatedAt { get; set; }

        public ItemGroup itemGroup { get; set; }

        public ItemSubGroup itemSubGroup { get; set; }

        public ObservableCollection<StoreInfor> storeInfors { get; set; }

    }
}