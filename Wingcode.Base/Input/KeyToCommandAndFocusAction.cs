using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace Wingcode.Base.Input
{
    public class KeyToCommandAndFocusAction : TriggerAction<UIElement>
    {
		public static readonly DependencyProperty KeyProperty = DependencyProperty.Register("Key", typeof(Key), typeof(KeyToCommandAndFocusAction));

		public static readonly DependencyProperty ModifiersProperty = DependencyProperty.Register("Modifiers", typeof(ModifierKeys), typeof(KeyToCommandAndFocusAction));

		public static readonly DependencyProperty TargetProperty = DependencyProperty.Register("Target", typeof(UIElement), typeof(KeyToCommandAndFocusAction), new UIPropertyMetadata(null));

		public static readonly DependencyProperty TargetNProperty = DependencyProperty.Register("TargetN", typeof(UIElement), typeof(KeyToCommandAndFocusAction), new UIPropertyMetadata(null));

		public static readonly DependencyProperty CommandProperty = DependencyProperty.Register("Command", typeof(ICommand), typeof(KeyToCommandAndFocusAction), new UIPropertyMetadata(null));

		private string commandName;

		public Key Key
		{
			get
			{
				return (Key)base.GetValue(KeyProperty);
			}
			set
			{
				base.SetValue(KeyProperty, (object)value);
			}
		}

		public ModifierKeys Modifiers
		{
			get
			{
				return (ModifierKeys)base.GetValue(ModifiersProperty);
			}
			set
			{
				base.SetValue(ModifiersProperty, (object)value);
			}
		}

		public UIElement Target
		{
			get
			{
				return (UIElement)base.GetValue(TargetProperty);
			}
			set
			{
				base.SetValue(TargetProperty, (object)value);
			}
		}

		public UIElement TargetN
		{
			get
			{
				return (UIElement)base.GetValue(TargetNProperty);
			}
			set
			{
				base.SetValue(TargetNProperty, (object)value);
			}
		}

		public ICommand Command
		{
			get
			{
				return (ICommand)base.GetValue(CommandProperty);
			}
			set
			{
				base.SetValue(CommandProperty, (object)value);
			}
		}

		public string CommandName
		{
			get
			{
				base.ReadPreamble();
				return commandName;
			}
			set
			{
				if (CommandName != value)
				{
					base.WritePreamble();
					commandName = value;
					base.WritePostscript();
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

		private void focusAction()
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

		private void executeAction(object parameter)
		{
			if (AssociatedObject != null)
			{
				ICommand command = ResolveCommand();
				if (command?.CanExecute(parameter) ?? false)
				{
					command.Execute(parameter);
				}
			}
		}

		protected override void Invoke(object parameter)
		{
			if (Keyboard.IsKeyDown(Key) && Keyboard.Modifiers == Modifiers 
				&& !Validation.GetHasError(AssociatedObject) 
				&& (!(AssociatedObject is TextBox) || !string.IsNullOrEmpty((AssociatedObject as TextBox).Text)))
			{
				focusAction();
				executeAction(parameter);
			}
		}
	}
}
