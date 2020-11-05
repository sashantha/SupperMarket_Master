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
    internal class SupplierSuggestionProvider : ISuggestionProvider
    {

        public ObservableCollection<SupplierCriteria> supplierCriterias { get; set; }

        public IEnumerable<SupplierCriteria> GetSuggestions(string filter) 
        {
            if (string.IsNullOrWhiteSpace(filter)) return null;
            return
                supplierCriterias
                    .Where(s => s.name.IndexOf(filter, StringComparison.CurrentCultureIgnoreCase) > -1)
                    .ToList();
        }

        IEnumerable ISuggestionProvider.GetSuggestions(string filter)
        {
            return GetSuggestions(filter);
        }

        public SupplierSuggestionProvider()
        {
            
        }        
    }
}
