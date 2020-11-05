using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace Wingcode.Base.Input
{
    public class KeyToInvokeActionCommand : TriggerAction<UIElement>
    {

        public Key Inputkey
        {
            get { return (Key)GetValue(InputkeyProperty); }
            set { SetValue(InputkeyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Inputkey.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty InputkeyProperty =
            DependencyProperty.Register(nameof(Inputkey), typeof(Key), typeof(KeyToInvokeActionCommand));



        public ModifierKeys Modifiers
        {
            get { return (ModifierKeys)GetValue(ModifiersProperty); }
            set { SetValue(ModifiersProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Modifiers.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ModifiersProperty =
            DependencyProperty.Register(nameof(Modifiers), typeof(ModifierKeys), typeof(KeyToInvokeActionCommand));



        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Command.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register(nameof(Command), typeof(ICommand), typeof(KeyToInvokeActionCommand), new UIPropertyMetadata(null));




        public object CommandParameter
        {
            get { return (object)GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CommandParameter.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CommandParameterProperty =
            DependencyProperty.Register(nameof(CommandParameter), typeof(object), typeof(KeyToInvokeActionCommand), new UIPropertyMetadata(null));


        private string commandName;

        public string CommandName
        {
            get 
            {
                ReadPreamble();
                return commandName;
            }
            set 
            {
                WritePreamble();
                commandName = value;
                WritePostscript();
            }
        }

        private ICommand ResolveCommand() 
        {
            ICommand result = null;
            if (Command != null)
            {
                return Command;
            }
            if (AssociatedObject != null)
            {
                PropertyInfo[] properties = AssociatedObject.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
                foreach (PropertyInfo propertyInfo in properties)
                {
                    if (typeof(ICommand).IsAssignableFrom(propertyInfo.PropertyType) && string.Equals(propertyInfo.Name, CommandName, StringComparison.Ordinal))
                    {
                        result = (ICommand)propertyInfo.GetValue(AssociatedObject, null);
                    }
                }
            }
            return result;
        }

        private void InvokeAction(object parameter)
        {
            if (AssociatedObject != null)
            {
                object commandParameter = CommandParameter != null ? CommandParameter : parameter;
                ICommand command = ResolveCommand();
                if (command?.CanExecute(commandParameter) ?? false)
                {
                    command.Execute(commandParameter);
                }
            }
        }

        protected override void Invoke(object parameter)
        {
            if (Keyboard.IsKeyDown(Inputkey) && Keyboard.Modifiers == Modifiers
                && !Validation.GetHasError(AssociatedObject)) 
            {
                InvokeAction(parameter);
            }
        }
    }
}
