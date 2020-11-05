using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wingcode.Controls;
using Wingcode.Data.Rest.Model;

namespace Wingcode.Items
{
    internal class UomSuggestionProvider : ISuggestionProvider
    {
        public ObservableCollection<UnitOfMeasurement> uomSource { get; set; }

        public IEnumerable<UnitOfMeasurement> GetSuggestions(string filter)
        {
            if (string.IsNullOrWhiteSpace(filter)) return null;
            return
                uomSource
                    .Where(s => s.unitDescription.IndexOf(filter, StringComparison.CurrentCultureIgnoreCase) > -1)
                    .ToList();
        }
        IEnumerable ISuggestionProvider.GetSuggestions(string filter)
        {
            return GetSuggestions(filter);
        }
    }
}
