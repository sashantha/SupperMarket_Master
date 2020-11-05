using MaterialDesignThemes.Wpf;

namespace Wingcode.Base
{
    public static class ConstValues
    {

        #region Arrays

        public static readonly string[] MENUITEMTEXTARRAY = { "Home", "Registers", "Stork", "Sales", "Expenses", "Reports", "Users", "Settings" };
        public static readonly PackIconKind[] PACKICONKINDS = {PackIconKind.Home,
            PackIconKind.RegisteredTrademark, PackIconKind.Store,
            PackIconKind.PointOfSale, PackIconKind.CashRemove, PackIconKind.FileChart, PackIconKind.AccountGroup, PackIconKind.Cog };
        public static readonly string[] UNIT_TYPES = { "Default", "Mass", "Length", "Volume", "Quantify" };

        #endregion

        #region ConstProperty

        public static readonly string PT_PURCHASE = "purchase";
        public static readonly string PT_FREE = "free";

        public static readonly string RCS_CANCLE = "cancel";
        public static readonly string RCS_FINE = "fine";
        public static readonly string RCS_DELETED = "deleted";
        public static readonly string RCS_EXPIRE = "expired";
        public static readonly string RCS_CREDIT = "credit";
        public static readonly string RCS_CASH = "cash";
        #endregion
    }
}
