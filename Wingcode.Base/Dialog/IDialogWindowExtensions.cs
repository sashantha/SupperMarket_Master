using Prism.Services.Dialogs;

namespace Wingcode.Base.Dialog
{
    internal static class IDialogWindowExtensions
    {
        internal static IDialogAware GetDialogViewModel(this IDialogWindow dialogWindow)
        {
            return (IDialogAware)dialogWindow.DataContext;
        }
    }
}
