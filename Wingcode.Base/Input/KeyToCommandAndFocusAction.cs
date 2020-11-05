using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Interactivity;
using Wingcode.Controls;

namespace Wingcode.Base.Input
{
    public class KeyToCommandAndFocusAction : TriggerAction<UIElement>
    {
		public static readonly DependencyProperty KeyProperty = DependencyProperty.Register("Key", typeof(Key), typeof(KeyToCommandAndFocusAction));

		public static readonly DependencyProperty ModifiersProperty = DependencyProperty.Register("Modifiers", typeof(ModifierKeys), typeof(KeyToCommandAndFocusAction));

		public static readonly DependencyProperty TargetProperty = DependencyProperty.Register("Target", typeof(UIElement), typeof(KeyToCommandAndFocusAction), new UIPropertyMetadata(null));

		public static readonly DependencyProperty TargetNProperty = DependencyProperty.Register("TargetN", typeof(UIElement), typeof(KeyToCommandAndFocusAction), new UIPropertyMetadata(null));

		public static readonly DependencyProperty CommandProperty = DependencyProperty.Register("Command", typeof(ICommand), typeof(KeyToCommandAndFocusAction), new UIPropertyMetadata(null));
		
		public static readonly DependencyProperty CommandParameterProperty = DependencyProperty.Register("CommandParameter", typeof(object), typeof(KeyToCommandAndFocusAction), new UIPropertyMetadata(null));

		private string commandName;

		public Key Key
		{
			get
			{
				return (Key)GetValue(KeyProperty);
			}
			set
			{
                SetValue(KeyProperty, value);
			}
		}

		public ModifierKeys Modifiers
		{
			get
			{
				return (ModifierKeys)GetValue(ModifiersProperty);
			}
			set
			{
                SetValue(ModifiersProperty, value);
			}
		}

		public UIElement Target
		{
			get
			{
				return (UIElement)GetValue(TargetProperty);
			}
			set
			{
                SetValue(TargetProperty, value);
			}
		}

		public UIElement TargetN
		{
			get
			{
				return (UIElement)GetValue(TargetNProperty);
			}
			set
			{
                SetValue(TargetNProperty, value);
			}
		}

		public ICommand Command
		{
			get
			{
				return (ICommand)GetValue(CommandProperty);
			}
			set
			{
                SetValue(CommandProperty, value);
			}
		}

		public object CommandParameter
		{
			get
			{
				return GetValue(CommandParameterProperty);
			}
			set
			{
				SetValue(CommandParameterProperty, value);
			}
		}

		public string CommandName
		{
			get
			{
                ReadPreamble();
				return commandName;
			}
			set
			{
				if (CommandName != value)
				{
                    WritePreamble();
					commandName = value;
                    WritePostscript();
				}
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

		private void FocusAction()
		{
			if (Target != null && TargetN != null)
			{
				if (Target.IsVisible)
				{
					Target.Focus();
					if (Target is TextBoxBase)
					{
						(Target as TextBoxBase).SelectAll();
					}
				}
				else
				{
					TargetN.Focus();
					if (TargetN is TextBoxBase)
					{
						(TargetN as TextBoxBase).SelectAll();
					}
				}
				return;
			}
			if (Target != null)
			{
				Target.Focus();
				if (Target is TextBoxBase)
				{
					(Target as TextBoxBase).SelectAll();
				}
			}
			if (TargetN != null)
			{
				TargetN.Focus();
				if (TargetN is TextBoxBase)
				{
					(TargetN as TextBoxBase).SelectAll();
				}
			}
		}

		private void ExecuteAction(object parameter)
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
			if (Keyboard.IsKeyDown(Key) && Keyboard.Modifiers == Modifiers 
				&& !Validation.GetHasError(AssociatedObject) 
				&& (!(AssociatedObject is TextBox) || !string.IsNullOrEmpty((AssociatedObject as TextBox).Text)))
			{
                if (AssociatedObject is AutoCompleteTextBox box)
                {
					if (box.SelectedItem == null && Key == Key.Enter)
						return;
                }
				FocusAction();
				ExecuteAction(parameter);
			}
		}
	}
}
