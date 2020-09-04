﻿using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wingcode.Base.DataModel;
using Wingcode.Base.Extensions;

namespace Wingcode.Customer.ViewModels
{
    public class CustomerRegisterViewModel : BaseViewModel
    {
        private IDialogService dialogService;
        private IContainerExtension containerExtension;

        public CustomerRegisterViewModel(IContainerExtension container, IDialogService dialog)
        {
            containerExtension = container;
            dialogService = dialog;
        }

    }
}
