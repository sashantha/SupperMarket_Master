using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Wingcode.Base.Input
{
    public class InputLanguageController
    {
        private static readonly string LangEnglish = "en-US";

        private static readonly string LangSinhala = "si-LK";

        public static void SwapInputLanguage() 
        {
            string name = InputLanguageManager.Current.CurrentInputLanguage.Name;
            if (name.Equals(LangEnglish))
                InputLanguageManager.Current.CurrentInputLanguage = new CultureInfo(LangSinhala);
            else
                InputLanguageManager.Current.CurrentInputLanguage = new CultureInfo(LangEnglish);
        }
    }
}
