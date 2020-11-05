using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace Wingcode.Base.Input
{
    public class KeyDownFocusAction : TriggerAction<UIElement>
    {
		public static readonly DependencyProperty KeyProperty = DependencyProperty.Register("Key", typeof(Key), typeof(KeyDownFocusAction));

		public static readonly DependencyProperty TargetProperty = DependencyProperty.Register("Target", typeof(UIElement), typeof(KeyDownFocusAction), new UIPropertyMetadata(null));

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

		protected override void Invoke(object parameter)
		{
			if (Keyboard.IsKeyDown(Key))
			{
				Target.Focus();
				
				if (Target is TextBoxBase)
				{
					(Target as TextBoxBase).SelectAll();
				}
			}
		}
	}

}
