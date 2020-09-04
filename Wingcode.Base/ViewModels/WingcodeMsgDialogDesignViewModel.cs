using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wingcode.Base.ViewModels
{
    public class WingcodeMsgDialogDesignViewModel : WingcodeMsgDialogViewModel
    {

        public static WingcodeMsgDialogDesignViewModel Instance => new WingcodeMsgDialogDesignViewModel();

        public WingcodeMsgDialogDesignViewModel()
        {
            Message = "Test Message sdfsdfsdfsdfgsdfsd fsdfsdfsdfsdfsdf sdfgdfhfgjfghdfg hdjhdfgdfgdfjhfghdfhdfh";
            YesNoEnable = true;
        }
    }
}
