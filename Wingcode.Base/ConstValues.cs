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
        public static readonly string[] CHQ_STAT = { "waiting", "relesed", "returned", "stoped" };

        #endregion

        #region ConstProperty

        public static readonly string PT_PURCHASE = "purchase";
        public static readonly string PT_FREE = "free";

        public static readonly string RCS_CANCLE = "cancel";
        public static readonly string RCS_FINE = "fine";
        public static readonly string RCS_NEW = "creating";
        public static readonly string RCS_DELETED = "deleted";
        public static readonly string RCS_EXPIRE = "expired";
        public static readonly string RCS_CREDIT = "credit";
        public static readonly string RCS_CASH = "cash";

        public static readonly string DFS_RTN = "rtn";
        public static readonly string DFS_DMG = "dmg";
        public static readonly string DFS_NON = "none";

        public static readonly string INVT_CASH = "cach";
        public static readonly string INVT_CHEQUE = "cheque";
        public static readonly string INVT_CASH_CHEQUE = "cach_cheque";
        public static readonly string INVT_CREDIT = "credit";
        public static readonly string INVT_PAID = "paid";

        public static readonly string BT_CREDITED = "credited";
        public static readonly string BT_DEBITED = "debited";

        #endregion
    }
}
