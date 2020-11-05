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
    internal class BranchAccountSuggestionProvider : ISuggestionProvider
    {

        public ObservableCollection<BranchAccount> brancgAcCriterias { get; set; }

        public IEnumerable<BranchAccount> GetSuggestions(string filter) 
        {
            if (string.IsNullOrWhiteSpace(filter)) return null;
            return
                brancgAcCriterias
                    .Where(s => s.bank.name.IndexOf(filter, StringComparison.CurrentCultureIgnoreCase) > -1)
                    .ToList();
        }

        IEnumerable ISuggestionProvider.GetSuggestions(string filter)
        {
            return GetSuggestions(filter);
        }

        public BranchAccountSuggestionProvider()
        {
            
        }        
    }
}
