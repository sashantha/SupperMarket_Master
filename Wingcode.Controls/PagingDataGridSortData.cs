using System.ComponentModel;

namespace Wingcode.Controls
{
    public class PagingDataGridSortData
    {

        public string ColumnName { get; set; }
        public ListSortDirection? ListSortDirection { get; set; }

        public PagingDataGridSortData(string columnName, ListSortDirection? listSortDirection)
        {
            ColumnName = columnName;
            ListSortDirection = listSortDirection;
        }
    }
}
