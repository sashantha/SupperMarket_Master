using Prism.Commands;
using Prism.Events;
using Prism.Ioc;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wingcode.Base;
using Wingcode.Base.DataModel;
using Wingcode.Base.Event;
using Wingcode.Base.Extensions;
using Wingcode.Data.Rest.Model;
using Wingcode.Data.Rest.Service;
using Wingcode.Master.Extension;

namespace Wingcode.Master.ViewModels
{
    public class MeasureEditorViewModel : BaseViewModel
    {

        #region Property

        private IDialogService dialogService;
        private IContainerExtension containerExtension;
        private IEventAggregator aggregator;
        private ILoggedUserProvider loggedUser;


        public Unit SelectedMeasure { get; set; }

        public bool IsNew { get; set; }

        //TypeSource
        public string[] TypeSource { get; set; }

        #endregion

        #region Commands

        public DelegateCommand SaveCommand { get; set; }

        public DelegateCommand NewCommand { get; set; }

        public DelegateCommand UpdateCommand { get; set; }

        #endregion

        #region Cunstructor

        public MeasureEditorViewModel(IContainerExtension extension, ILoggedUserProvider provider)
        {
            containerExtension = extension;
            loggedUser = provider;
            SaveCommand = new DelegateCommand(SaveMeasurement);
            NewCommand = new DelegateCommand(NewMeasurement);
            UpdateCommand = new DelegateCommand(UpdateMeasurement);
            dialogService = containerExtension.Resolve<IDialogService>();
            aggregator = containerExtension.Resolve<IEventAggregator>();
        }

        #endregion

        #region Functions
        internal void Initialize()
        {
            TypeSource = ConstValues.UNIT_TYPES;
            if (IsNew)
                NewMeasurement();
        }

        private async void SaveMeasurement()
        {
            IRestDataMapper mapper = containerExtension.Resolve<IRestDataMapper>();

            if (SelectedMeasure.IsValidUnit())
            {
                Unit b = await ItemRestService.CreateUnitAsync(mapper, SelectedMeasure);
                if (b.id > 0)
                {
                    _ = ShowMessageDialg("New Unit Creation", "Measurement Created Successfully", MsgDialogType.infor);
                    RizeSyncEvent();
                    Initialize();
                }
                else
                {
                    _ = ShowMessageDialg("New Unit Creation", "Can't Save Measurement", MsgDialogType.error);
                    return;
                }
            }
            else
            {
                _ = ShowMessageDialg("New Unit Creation", "Invalid Unit Details or Already Exist Unit", MsgDialogType.warrning);
                return;
            }
        }

        private async void UpdateMeasurement()
        {
            IRestDataMapper mapper = containerExtension.Resolve<IRestDataMapper>();
            if (SelectedMeasure != null)
            {
                if (SelectedMeasure.id > 0)
                {
                    Unit i = await ItemRestService.UpdateUnitAsync(mapper, SelectedMeasure);
                    if (i.id > 0)
                    {
                        _ = ShowMessageDialg("Unit Update", "Unit Updated Successfully", MsgDialogType.infor);
                        RizeSyncEvent();
                    }
                    else
                    {
                        _ = ShowMessageDialg("Unit Update", "Can't Save Unit", MsgDialogType.error);
                        return;
                    }
                }
                else
                {
                    _ = ShowMessageDialg("Unit Update", "Please Select Unit Before Update", MsgDialogType.warrning);
                    return;
                }
            }
            else
            {
                _ = ShowMessageDialg("Unit Update", "Please Select Unit Before Update", MsgDialogType.warrning);
                return;
            }
        }

        private async void NewMeasurement()
        {
            aggregator.GetEvent<UIElementFocusEvent>().Publish("");
            await Task.Delay(500);
            SelectedMeasure = SelectedMeasure.CreateNewUnit();
        }

        private bool ShowMessageDialg(string title, string message, MsgDialogType dialogType)
        {
            bool result = false;
            dialogService.ShowMsgDialog(title, message, dialogType, (r) =>
            {
                if (r.Result == ButtonResult.OK)
                {
                    result = true;
                }
            });
            return result;
        }

        #endregion
    }
}
