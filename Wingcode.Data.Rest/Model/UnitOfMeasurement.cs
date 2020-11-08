using System;
using Wingcode.Base.DataModel;

namespace Wingcode.Data.Rest.Model
{


    /// <summary>
    /// The persistent class for the unit_of_measure database table.
    /// 
    /// </summary>

    public class UnitOfMeasurement : ModelBase<UnitOfMeasurement>
    {

        public int id { get; set; }

        public decimal basePrecision { get; set; }

        public decimal baseRatio { get; set; }

        public decimal baseRatioToPurchase { get; set; }

        public decimal baseRatioToSale { get; set; }

        public string baseUnitName { get; set; }

        public decimal purchasePrecision { get; set; }

        public string purchasePrecisionUnitName { get; set; }

        public decimal purchaseQuantifyValue { get; set; }

        public string purchaseUnitName { get; set; }

        public string saleOtherUnitName { get; set; }

        public decimal salePrecision { get; set; }

        public string saleUnitName { get; set; }

        public string unitDescription { get; set; }

        public string unitType { get; set; }

        public DateTime createdAt { get; set; }

        public DateTime updatedAt { get; set; }

    }
}