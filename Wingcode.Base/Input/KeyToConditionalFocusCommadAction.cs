using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace Wingcode.Base.Input
{
    public class KeyToConditionalFocusCommadAction : TriggerAction<UIElement>
    {

        public ModifierKeys Modifiers
        {
            get { return (ModifierKeys)GetValue(ModifiersProperty); }
            set { SetValue(ModifiersProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Modifiers.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ModifiersProperty =
            DependencyProperty.Register(nameof(Modifiers), typeof(ModifierKeys), typeof(KeyToConditionalFocusCommadAction));

        public Key ShortCutKey
        {
            get { return (Key)GetValue(ShortCutKeyProperty); }
            set { SetValue(ShortCutKeyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ShortCutKey.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ShortCutKeyProperty =
            DependencyProperty.Register(nameof(ShortCutKey), typeof(Key), typeof(KeyToConditionalFocusCommadAction));

        public bool Condition
        {
            get { return (bool)GetValue(ConditionProperty); }
            set { SetValue(ConditionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Condition.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ConditionProperty =
            DependencyProperty.Register(nameof(Condition), typeof(bool), typeof(KeyToConditionalFocusCommadAction), new UIPropertyMetadata(false));

        public UIElement TargetTrue
        {
            get { return (UIElement)GetValue(TargetTrueProperty); }
            set { SetValue(TargetTrueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TargetTrue.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TargetTrueProperty =
            DependencyProperty.Register(nameof(TargetTrue), typeof(UIElement), typeof(KeyToConditionalFocusCommadAction), new UIPropertyMetadata(null));

        public UIElement TargetFalse
        {
            get { return (UIElement)GetValue(TargetFalseProperty); }
            set { SetValue(TargetFalseProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TargetFalse.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TargetFalseProperty =
            DependencyProperty.Register(nameof(TargetFalse), typeof(UIElement), typeof(KeyToConditionalFocusCommadAction), new UIPropertyMetadata(null));

        public ICommand CommandTrue
        {
            get { return (ICommand)GetValue(CommandTrueProperty); }
            set { SetValue(CommandTrueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CommandTrue.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CommandTrueProperty =
            DependencyProperty.Register(nameof(CommandTrue), typeof(ICommand), typeof(KeyToConditionalFocusCommadAction), new UIPropertyMetadata(null));

        public object CommandTrueParameter
        {
            get { return (object)GetValue(CommandTrueParameterProperty); }
            set { SetValue(CommandTrueParameterProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CommandTrueParameter.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CommandTrueParameterProperty =
            DependencyProperty.Register(nameof(CommandTrueParameter), typeof(object), typeof(KeyToConditionalFocusCommadAction), new UIPropertyMetadata(null));



        public ICommand CommandFalse
        {
            get { return (ICommand)GetValue(CommandFalseProperty); }
            set { SetValue(CommandFalseProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CommandFalse.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CommandFalseProperty =
            DependencyProperty.Register(nameof(CommandFalse), typeof(ICommand), typeof(KeyToConditionalFocusCommadAction), new UIPropertyMetadata(null));



        public object CommandFalseParameter
        {
            get { return (object)GetValue(CommandFalseParameterProperty); }
            set { SetValue(CommandFalseParameterProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CommandFalseParameter.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CommandFalseParameterProperty =
            DependencyProperty.Register(nameof(CommandFalseParameter), typeof(object), typeof(KeyToConditionalFocusCommadAction), new UIPropertyMetadata(null));

        private ICommand ResolveCommand(bool condition)
        {
            if (condition)
            {
                ICommand result = null;
                if (CommandTrue != null)
                {
                    return CommandTrue;
                }
                if (AssociatedObject != null)
                {
                    PropertyInfo[] properties = AssociatedObject.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
                    foreach (PropertyInfo propertyInfo in properties)
                    {
                        if (typeof(ICommand).IsAssignableFrom(propertyInfo.PropertyType) && string.Equals(propertyInfo.Name, nameof(CommandTrue), StringComparison.Ordinal))
                        {
                            result = (ICommand)propertyInfo.GetValue(AssociatedObject, null);
                        }
                    }
                }
                return result;
            }
            else
            {
                ICommand result = null;
                if (CommandFalse != null)
                {
                    return CommandFalse;
                }
                if (AssociatedObject != null)
                {
                    PropertyInfo[] properties = AssociatedObject.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
                    foreach (PropertyInfo propertyInfo in properties)
                    {
                        if (typeof(ICommand).IsAssignableFrom(propertyInfo.PropertyType) && string.Equals(propertyInfo.Name, nameof(CommandFalse), StringComparison.Ordinal))
                        {
                            result = (ICommand)propertyInfo.GetValue(AssociatedObject, null);
                        }
                    }
                }
                return result;
            }
        }

        private void InvokeFocusAction()
        {
            if (Condition)
            {
                if (TargetTrue != null)
                {
                    if (TargetTrue.IsVisible)
                    {
                        TargetTrue.Focus();
                        if (TargetTrue is TextBoxBase tb)
                            tb.SelectAll();
                    }
                }
            }
            else
            {
                if (TargetFalse != null)
                {
                    if (TargetFalse.IsVisible)
                    {
                        TargetFalse.Focus();
                        if (TargetFalse is TextBoxBase tb)
                            tb.SelectAll();
                    }
                }
            }
        }

        private void InvokeAction(object parameter)
        {
            if (AssociatedObject != null)
            {
                object commandParameter = Condition ? CommandTrueParameter : CommandFalseParameter;
                if (commandParameter == null)
                    commandParameter = parameter;
                ICommand command = ResolveCommand(Condition);
                if (command?.CanExecute(commandParameter) ?? false)
                {
                    command.Execute(commandParameter);
                }
            }
        }

        protected override void Invoke(object parameter)
        {
            if (Keyboard.IsKeyDown(ShortCutKey) && Keyboard.Modifiers == Modifiers
                && !Validation.GetHasError(AssociatedObject))
            {
                InvokeFocusAction();
                InvokeAction(parameter);
            }
        }
    }
}
