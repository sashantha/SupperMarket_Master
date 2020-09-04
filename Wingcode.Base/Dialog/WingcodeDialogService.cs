﻿using Prism.Common;
using Prism.Ioc;
using Prism.Services.Dialogs;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Wingcode.Base.Dialog
{
    public class WingcodeDialogService : IDialogService
    {
        private readonly IContainerExtension _containerExtension;

        //TODO: delete
        protected IDialogWindow DialogWindow { get; private set; }

        public WingcodeDialogService(IContainerExtension containerExtension)
        {
            _containerExtension = containerExtension;
        }

        public void Show(string name, IDialogParameters parameters, Action<IDialogResult> callback)
        {
            ShowDialogInternal(name, parameters, callback, false);
        }

        public void ShowDialog(string name, IDialogParameters parameters, Action<IDialogResult> callback)
        {
            ShowDialogInternal(name, parameters, callback, true);
        }

        protected void ShowDialogInternal(string name, IDialogParameters parameters, Action<IDialogResult> callback, bool isModal)
        {
            DialogWindow = CreateDialogWindow();

            ConfigureDialogWindowEvents(DialogWindow, callback);
            ConfigureDialogWindowContent(name, DialogWindow, parameters);

            //TODO: remove this
            InitializeDialogWindow(name, parameters);

            //TODO: do this
            //DialogWindow.Initialize(name, parameters);

            if (isModal)
                DialogWindow.ShowDialog();
            else
                DialogWindow.Show();
        }

        //TODO: delete this
        protected void InitializeDialogWindow(string name, IDialogParameters parameters)
        {

        }

        protected IDialogWindow CreateDialogWindow()
        {
            return new WingcodeDialogWindow();
            //return _containerExtension.Resolve<IDialogWindow>();
        }

        protected void ConfigureDialogWindowContent(string dialogName, IDialogWindow window, IDialogParameters parameters)
        {
            var content = _containerExtension.Resolve<object>(dialogName);
            var dialogContent = content as FrameworkElement;
            if (dialogContent == null)
                throw new NullReferenceException("A dialog's content must be a FrameworkElement");

            var viewModel = dialogContent.DataContext as IDialogAware;
            if (viewModel == null)
                throw new NullReferenceException("A dialog's ViewModel must implement the IDialogAware interface");

            ConfigureDialogWindowProperties(window, dialogContent, viewModel);

            MvvmHelpers.ViewAndViewModelAction<IDialogAware>(viewModel, d => d.OnDialogOpened(parameters));
        }

        protected void ConfigureDialogWindowEvents(IDialogWindow dialogWindow, Action<IDialogResult> callback)
        {
            Action<IDialogResult> requestCloseHandler = null;
            requestCloseHandler = (o) =>
            {
                dialogWindow.Result = o;
                dialogWindow.Close();
            };

            RoutedEventHandler loadedHandler = null;
            loadedHandler = (o, e) =>
            {
                dialogWindow.Loaded -= loadedHandler;
                dialogWindow.GetDialogViewModel().RequestClose += requestCloseHandler;
            };
            dialogWindow.Loaded += loadedHandler;

            CancelEventHandler closingHandler = null;
            closingHandler = (o, e) =>
            {
                if (!dialogWindow.GetDialogViewModel().CanCloseDialog())
                    e.Cancel = true;
            };
            dialogWindow.Closing += closingHandler;

            EventHandler closedHandler = null;
            closedHandler = (o, e) =>
            {
                dialogWindow.Closed -= closedHandler;
                dialogWindow.Closing -= closingHandler;
                dialogWindow.GetDialogViewModel().RequestClose -= requestCloseHandler;

                dialogWindow.GetDialogViewModel().OnDialogClosed();

                if (dialogWindow.Result == null)
                    dialogWindow.Result = new DialogResult();

                callback?.Invoke(dialogWindow.Result);

                dialogWindow.DataContext = null;
                dialogWindow.Content = null;
            };
            dialogWindow.Closed += closedHandler;
        }

        protected void ConfigureDialogWindowProperties(IDialogWindow window, FrameworkElement dialogContent, IDialogAware viewModel)
        {
            var windowStyle = Prism.Services.Dialogs.Dialog.GetWindowStyle(dialogContent);
            if (windowStyle != null)
                window.Style = windowStyle;

            //TODO: add to prism
            if (window.Content == null)
            {
                window.Content = dialogContent;
            }
            else
            {
                Window w = window as Window;
                ContentControl _content = w.FindName("DialogPane") as ContentControl;
                _content.Content = dialogContent;
                //_content.Focus();
            }

            if (viewModel != null)
                window.DataContext = viewModel; //we want the host window and the dialog to share the same data contex

            if (window.Owner == null)
                window.Owner = Application.Current.Windows.OfType<Window>().FirstOrDefault(x => x.IsActive);
        }
    }
}
