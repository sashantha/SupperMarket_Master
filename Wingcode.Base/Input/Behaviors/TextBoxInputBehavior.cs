using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace Wingcode.Base.Input.Behaviors
{
    public class TextBoxInputBehavior : Behavior<TextBox>
    {
		public static readonly DependencyProperty RegularExpressionProperty = DependencyProperty.Register("RegularExpression", typeof(string), typeof(TextBoxInputBehavior), new FrameworkPropertyMetadata(".*"));

		public static readonly DependencyProperty MaxLengthProperty = DependencyProperty.Register("MaxLength", typeof(int), typeof(TextBoxInputBehavior), new FrameworkPropertyMetadata(int.MinValue));

		public string RegularExpression
		{
			get
			{
				return (string)base.GetValue(RegularExpressionProperty);
			}
			set
			{
				base.SetValue(RegularExpressionProperty, (object)value);
			}
		}

		public int MaxLength
		{
			get
			{
				return (int)base.GetValue(MaxLengthProperty);
			}
			set
			{
				base.SetValue(MaxLengthProperty, (object)value);
			}
		}

		protected override void OnAttached()
		{
			base.OnAttached();
			AssociatedObject.PreviewTextInput += OnPreviewTextInput;
			DataObject.AddPastingHandler(AssociatedObject, OnPaste);
		}

		private void OnPaste(object sender, DataObjectPastingEventArgs e)
		{
			if (e.DataObject.GetDataPresent(DataFormats.Text))
			{
				string newText = Convert.ToString(e.DataObject.GetData(DataFormats.Text));
				if (!IsValid(newText, paste: true))
				{
					e.CancelCommand();
				}
			}
			else
			{
				e.CancelCommand();
			}
		}

		private void OnPreviewTextInput(object sender, TextCompositionEventArgs e)
		{
			e.Handled = !IsValid(e.Text, paste: false);
		}

		protected override void OnDetaching()
		{
			base.OnDetaching();
			AssociatedObject.PreviewTextInput -= OnPreviewTextInput;
			DataObject.RemovePastingHandler(AssociatedObject, OnPaste);
		}

		private bool IsValid(string newText, bool paste)
		{
			return !ExceedsMaxLength(newText, paste) && Regex.IsMatch(newText, RegularExpression);
		}

		private bool ExceedsMaxLength(string newText, bool paste)
		{
			if (MaxLength == 0)
			{
				return false;
			}
			return LengthOfModifiedText(newText, paste) > MaxLength;
		}

		private int LengthOfModifiedText(string newText, bool paste)
		{
			int length = AssociatedObject.SelectedText.Length;
			int caretIndex = AssociatedObject.CaretIndex;
			string text = AssociatedObject.Text;
			if (length > 0 || paste)
			{
				text = text.Remove(caretIndex, length);
				return text.Length + newText.Length;
			}
			return (Keyboard.IsKeyToggled(Key.Insert) && caretIndex < text.Length) ? text.Length : (text.Length + newText.Length);
		}
	}
}
