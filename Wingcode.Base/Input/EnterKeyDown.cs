using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace Wingcode.Base.Input
{
    public sealed class EnterKeyDown
    {

        #region Properties

        #region Command

        public static ICommand GetCommand(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(CommandProperty);
        }

        public static void SetCommand(DependencyObject obj, ICommand value)
        {
            obj.SetValue(CommandProperty, value);
        }

        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.RegisterAttached("Command", typeof(ICommand), typeof(EnterKeyDown),
                new PropertyMetadata(null, OnCommandChanged));

        #endregion Command

        #region CommandParameter

        public static object GetCommandParameter(DependencyObject obj)
        {
            return (object)obj.GetValue(CommandParameterProperty);
        }

        public static void SetCommandParameter(DependencyObject obj, object value)
        {
            obj.SetValue(CommandParameterProperty, value);
        }

        public static readonly DependencyProperty CommandParameterProperty =
            DependencyProperty.RegisterAttached("CommandParameter", typeof(object), typeof(EnterKeyDown),
                new PropertyMetadata(null, OnCommandParameterChanged));

        #endregion CommandArgument

        #region HasCommandParameter


        private static bool GetHasCommandParameter(DependencyObject obj)
        {
            return (bool)obj.GetValue(HasCommandParameterProperty);
        }

        private static void SetHasCommandParameter(DependencyObject obj, bool value)
        {
            obj.SetValue(HasCommandParameterProperty, value);
        }

        private static readonly DependencyProperty HasCommandParameterProperty =
            DependencyProperty.RegisterAttached("HasCommandParameter", typeof(bool), typeof(EnterKeyDown),
                new PropertyMetadata(false));


        #endregion HasCommandArgument

        #endregion Propreties

        #region Event Handling

        private static void OnCommandParameterChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            SetHasCommandParameter(o, true);
        }

        private static void OnCommandChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            FrameworkElement element = o as FrameworkElement;
            if (element != null)
            {
                if (e.NewValue == null)
                {
                    element.KeyDown -= new KeyEventHandler(FrameworkElement_KeyDown);
                }
                else if (e.OldValue == null)
                {
                    element.KeyDown += new KeyEventHandler(FrameworkElement_KeyDown);
                }
            }
        }

        private static void FrameworkElement_KeyDown(object sender, KeyEventArgs e)
        {
            if (sender is TextBox text)
            {
                if (string.IsNullOrEmpty(text.Text))
                    return;
            }
            if (sender is PasswordBox pass)
            {
                if (string.IsNullOrEmpty(pass.Password))
                    return;
            }
            if (e.Key == Key.Enter)
            {
                DependencyObject o = sender as DependencyObject;
                ICommand command = GetCommand(sender as DependencyObject);

                FrameworkElement element = e.OriginalSource as FrameworkElement;
                if (element != null)
                {
                    // If the command argument has been explicitly set (even to NULL)
                    if (GetHasCommandParameter(o))
                    {
                        object commandParameter = GetCommandParameter(o);

                        // Execute the command
                        if (command.CanExecute(commandParameter))
                        {
                            command.Execute(commandParameter);
                        }
                    }
                    else if (command.CanExecute(element.DataContext))
                    {
                        command.Execute(element.DataContext);
                    }
                }
            }
        }

        #endregion
    }
}
