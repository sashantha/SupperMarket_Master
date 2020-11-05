using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Wingcode.Base.Extensions
{
    public static class CollectionExtension
    {

        public static DataSet ConvertToDataSet<T>(this IDictionary<string, IEnumerable<T>> source, string name) 
        {
            if (source == null)
                throw new ArgumentNullException("source ");
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException("name");
            var converted = new DataSet(name);
            foreach (var item in source)
            {
                converted.Tables.Add(item.Value.ConvertToDataTable(item.Key));
            }            
            return converted;
        }

        public static DataTable ConvertToDataTable<T>(this IEnumerable<T> data, string tableName) {
            PropertyInfo[] propInfo = typeof(T).GetProperties();
            DataTable table = Table<T>(tableName, data, propInfo);
            IEnumerator<T> enumerator = data.GetEnumerator();
            while (enumerator.MoveNext())
                table.Rows.Add(CreateRow<T>(table.NewRow(), enumerator.Current, propInfo));
            return table;
        }

        private static DataTable Table<T>(string tableName, IEnumerable<T> list, PropertyInfo[] pi) 
        {
            DataTable table = new DataTable(tableName);
            foreach (PropertyInfo p in pi)
                table.Columns.Add(p.Name, p.PropertyType);
            return table;
        }

        private static DataRow CreateRow<T>(DataRow row, T listItem, PropertyInfo[] pi)
        {
            foreach (PropertyInfo p in pi)
                row[p.Name.ToString()] = p.GetValue(listItem, null);
            return row;
        }
    }
}
