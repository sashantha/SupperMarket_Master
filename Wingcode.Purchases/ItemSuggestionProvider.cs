using CommonServiceLocator;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Wingcode.Controls;
using Wingcode.Data.Rest.Model;
using Wingcode.Data.Rest.Service;

namespace Wingcode.Purchases
{
    internal class ItemSuggestionProvider : ISuggestionProvider
    {

        public ObservableCollection<ItemCriteria> itemCriterias { get; set; }

        public string DisplayMember { get; set; } = "code";

        public IEnumerable<ItemCriteria> GetSuggestions(string filter) 
        {
            switch (DisplayMember)
            {
                case "code":
                    return
                itemCriterias
                    .Where(s => s.code.IndexOf(filter, StringComparison.CurrentCultureIgnoreCase) > -1)
                    .ToList();
                case "barcode":
                    return
                itemCriterias
                    .Where(s => s.barcode.IndexOf(filter, StringComparison.CurrentCultureIgnoreCase) > -1)
                    .ToList();
                case "name":
                    return
                itemCriterias
                    .Where(s => s.name.IndexOf(filter, StringComparison.CurrentCultureIgnoreCase) > -1)
                    .ToList();
                default:
                    return null;
            }           
        }

        IEnumerable ISuggestionProvider.GetSuggestions(string filter)
        {
            return GetSuggestions(filter);
        }

    }
}
