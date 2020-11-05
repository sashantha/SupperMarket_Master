using System;

namespace Wingcode.Data.Rest.Model
{


    /// <summary>
    /// The persistent class for the sale_item database table.
    /// 
    /// </summary>

    public class SaleItem
    {

        public long id { get; set; }

        public decimal amount { get; set; }

        public decimal cost { get; set; }

        public DateTime createdAt { get; set; }

        public decimal discount { get; set; }

        public int issueNo { get; set; }

        public decimal netAmount { get; set; }

        public decimal profit { get; set; }

        public decimal quantity { get; set; }

        public string saleUnit { get; set; }

        public decimal realAmount { get; set; }

        public DateTime saleDate { get; set; }

        public decimal salePrice { get; set; }

        public string recordState { get; set; }

        public decimal defectQuantity { get; set; }

        public string defectState { get; set; }

        public DateTime updatedAt { get; set; }

        public Item item { get; set; }

        public SaleInvoice saleInvoice { get; set; }

    }
}