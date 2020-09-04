using Prism.Services.Dialogs;
using System;
using Wingcode.Base.DataModel;
using Wingcode.Base.Dialog;
using Wingcode.Base.ViewModels;

namespace Wingcode.Base.Extensions
{
    public static class WingcodeDialogServiceExtensions
    {

        public static void ShowMsgDialog(this IDialogService dialogService, string message, Action<IDialogResult> callBack)
        {
            DialogParameters param = new DialogParameters()
            {
                {DialogParameterKeys.Title, "Message Dialog"},
                {DialogParameterKeys.Message, message},
                {DialogParameterKeys.ButtonType, MsgDialogButtonType.Ok},
                {DialogParameterKeys.DialogType, MsgDialogType.none}
            };
            dialogService.ShowDialog("WingcodeMsgDialog", param, callBack);
        }

        public static void ShowMsgDialog(this IDialogService dialogService, string title, string message, Action<IDialogResult> callBack)
        {
            DialogParameters param = new DialogParameters()
            {
                {DialogParameterKeys.Title, title},
                {DialogParameterKeys.Message, message},
                {DialogParameterKeys.ButtonType, MsgDialogButtonType.Ok },
                {DialogParameterKeys.DialogType, MsgDialogType.none}
            };
            dialogService.ShowDialog("WingcodeMsgDialog", param, callBack);
        }

        public static void ShowMsgDialog(this IDialogService dialogService, string title, string message, MsgDialogButtonType dialogButtonType,
            Action<IDialogResult> callBack)
        {
            DialogParameters param = new DialogParameters()
            {
                {DialogParameterKeys.Title, title},
                {DialogParameterKeys.Message, message},
                {DialogParameterKeys.ButtonType, dialogButtonType},
                {DialogParameterKeys.DialogType, MsgDialogType.none}
            };
            dialogService.ShowDialog("WingcodeMsgDialog", param, callBack);
        }

        public static void ShowMsgDialog(this IDialogService dialogService, string title, string message, MsgDialogType dialogType,
            Action<IDialogResult> callBack)
        {
            DialogParameters param = new DialogParameters()
            {
                {DialogParameterKeys.Title, title},
                {DialogParameterKeys.Message, message},
                {DialogParameterKeys.ButtonType, MsgDialogButtonType.Ok},
                {DialogParameterKeys.DialogType, dialogType}
            };
            dialogService.ShowDialog("WingcodeMsgDialog", param, callBack);
        }

        public static void ShowMsgDialog(this IDialogService dialogService, string title, string message, MsgDialogButtonType dialogButtonType,
            MsgDialogType dialogType, Action<IDialogResult> callBack)
        {
            DialogParameters param = new DialogParameters()
            {
                {DialogParameterKeys.Title, title},
                {DialogParameterKeys.Message, message},
                {DialogParameterKeys.ButtonType, dialogButtonType},
                {DialogParameterKeys.DialogType, dialogType}
            };
            dialogService.ShowDialog("WingcodeMsgDialog", param, callBack);
        }

        public static void ShowDialog(this IDialogService dialogService, BaseDialogHostViewModel dialogHostViewModel, Action<IDialogResult> callBack)
        {
            DialogParameters param = new DialogParameters()
            {
                {DialogParameterKeys.DialogHost, dialogHostViewModel},
            };
            dialogService.ShowDialog("WingcodeDialogBox", param, callBack);
        }

        public static void Show(this IDialogService dialogService, BaseDialogHostViewModel dialogHostViewModel, Action<IDialogResult> callBack)
        {
            DialogParameters param = new DialogParameters()
            {
                {DialogParameterKeys.DialogHost, dialogHostViewModel},
            };
            dialogService.Show("WingcodeDialogBox", param, callBack);
        }
    }
}
