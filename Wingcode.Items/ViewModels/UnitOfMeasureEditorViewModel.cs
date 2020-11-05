﻿using Prism.Commands;
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
using Wingcode.Items.Extensions;

namespace Wingcode.Items.ViewModels
{
    public class UnitOfMeasureEditorViewModel : BaseViewModel
    {
        #region Property

        private IDialogService dialogService;
        private IContainerExtension containerExtension;
        private IEventAggregator aggregator;
        private ILoggedUserProvider loggedUser;


        private UnitOfMeasurement _selectedUom;
        public UnitOfMeasurement SelectedUom
        {
            get { return _selectedUom; }
            set { SetProperty(ref _selectedUom, value); }
        }

        private ObservableCollection<UnitOfMeasurement> uoms;
        public ObservableCollection<UnitOfMeasurement> Uoms
        {
            get { return uoms; }
            set { SetProperty(ref uoms, value); }
        }
        private bool _isNew;
        public bool IsNew
        {
            get { return _isNew; }
            set { SetProperty(ref _isNew, value); }
        }

        private ObservableCollection<string> unitNames;
        public ObservableCollection<string> UnitNames
        {
            get { return unitNames; }
            set { SetProperty(ref unitNames, value); }
        }

        private ObservableCollection<string> unitTypes;
        public ObservableCollection<string> UnitTypes
        {
            get { return unitTypes; }
            set { SetProperty(ref unitTypes, value); }
        }
        #endregion

        #region Commands

        private DelegateCommand _saveCommand;
        public DelegateCommand SaveCommand
        {
            get { return _saveCommand; }
            set { SetProperty(ref _saveCommand, value); }
        }

        private DelegateCommand _newCommand;
        public DelegateCommand NewCommand
        {
            get { return _newCommand; }
            set { SetProperty(ref _newCommand, value); }
        }

        private DelegateCommand _updateCommand;
        public DelegateCommand UpdateCommand
        {
            get { return _updateCommand; }
            set { SetProperty(ref _updateCommand, value); }
        }

        private DelegateCommand _gridSelectionChangedCommand;
        public DelegateCommand GridSelectionChangedCommand
        {
            get { return _gridSelectionChangedCommand; }
            set { SetProperty(ref _gridSelectionChangedCommand, value); }
        }

        private DelegateCommand _typeChangedCommand;
        public DelegateCommand TypeChangedCommand
        {
            get { return _typeChangedCommand; }
            set { SetProperty(ref _typeChangedCommand, value); }
        }
        #endregion

        #region Constructor

        public UnitOfMeasureEditorViewModel(IContainerExtension extension, ILoggedUserProvider provider)
        {
            containerExtension = extension;
            loggedUser = provider;
            SaveCommand = new DelegateCommand(SaveUom);
            NewCommand = new DelegateCommand(NewUom);
            UpdateCommand = new DelegateCommand(UpdateUom);
            GridSelectionChangedCommand = new DelegateCommand(GridSelectionChanged);
            TypeChangedCommand = new DelegateCommand(TypeChanged);
            dialogService = containerExtension.Resolve<IDialogService>();
            aggregator = containerExtension.Resolve<IEventAggregator>();
        }

        #endregion

        #region Functions
        internal async void Initialize()
        {
            LoadAllItem();
            await Task.Delay(500);
            NewUom();
        }

        private async void LoadAllItem()
        {
            UnitTypes = new ObservableCollection<string>();
            UnitTypes.AddRange(ConstValues.UNIT_TYPES);
            IRestDataMapper mapper = containerExtension.Resolve<IRestDataMapper>();
            Uoms = await ItemRestService.GetAllUnitOfMeasurementAsync(mapper);           
            LoadUnitNames();
        }

        private async void LoadUnitNames() 
        {

            IRestDataMapper mapper = containerExtension.Resolve<IRestDataMapper>();
            ObservableCollection<Unit> ms = await ItemRestService.GetAllUnitAsync(mapper);
            if (SelectedUom == null)
            {
                UnitNames = new ObservableCollection<string>();
                foreach (var item in ms)
                {
                    UnitNames.Add(item.unitName);
                }
            }
            else if (string.IsNullOrEmpty(SelectedUom.unitType))
            {
                UnitNames = new ObservableCollection<string>();
                foreach (var item in ms)
                {
                    UnitNames.Add(item.unitName);
                }
            }
            else
            {
                UnitNames = new ObservableCollection<string>();
                List<Unit> msc = ms.Where(r => r.unitType.Equals(SelectedUom.unitType)).ToList();
                foreach (var item in msc)
                {
                    UnitNames.Add(item.unitName);
                }

            }
        }

        private void TypeChanged()
        {
            if(IsNew)
                LoadUnitNames();
        }

        private void GridSelectionChanged()
        {
            IsNew = false;
        }

        private async void SaveUom()
        {
            IRestDataMapper mapper = containerExtension.Resolve<IRestDataMapper>();
            if (SelectedUom.IsValidUom())
            {
                SelectedUom.saleOtherUnitName = $"{SelectedUom.baseUnitName}, {SelectedUom.purchaseUnitName}";

                UnitOfMeasurement b = await ItemRestService.CreateUnitOfMeasurementAsync(mapper, SelectedUom);
                if (b.id > 0)
                {
                    _ = ShowMessageDialg("New UnitOfMeasure Creation", "UnitOfMeasure Created Successfully", MsgDialogType.infor);
                    Initialize();
                }
                else
                {
                    _ = ShowMessageDialg("New UnitOfMeasure Creation", "Can't Save UnitOfMeasure", MsgDialogType.error);
                    return;
                }
            }
            else
            {
                _ = ShowMessageDialg("New UnitOfMeasure Creation", "Invalid UnitOfMeasure Details or Already Exist UnitOfMeasure", MsgDialogType.warrning);
                return;
            }
        }

        private async void UpdateUom()
        {
            IRestDataMapper mapper = containerExtension.Resolve<IRestDataMapper>();
            if (SelectedUom != null)
            {
                if (SelectedUom.id > 0)
                {
                    SelectedUom.saleOtherUnitName = $"{SelectedUom.baseUnitName}, {SelectedUom.purchaseUnitName}";

                    UnitOfMeasurement i = await ItemRestService.UpdateUnitOfMeasurementAsync(mapper, SelectedUom);
                    if (i.id > 0)
                    {
                        _ = ShowMessageDialg("UnitOfMeasure Update", "UnitOfMeasure Updated Successfully", MsgDialogType.infor);
                    }
                    else
                    {
                        _ = ShowMessageDialg("UnitOfMeasure Update", "Can't Save UnitOfMeasure", MsgDialogType.error);
                        return;
                    }
                }
                else
                {
                    _ = ShowMessageDialg("UnitOfMeasure Update", "Please Select UnitOfMeasure Before Update", MsgDialogType.warrning);
                    return;
                }
            }
            else
            {
                _ = ShowMessageDialg("UnitOfMeasure Update", "Please Select UnitOfMeasure Before Update", MsgDialogType.warrning);
                return;
            }
        }

        private async void NewUom()
        {
            aggregator.GetEvent<DataGridSelectionClearEvent>().Publish();
            aggregator.GetEvent<UIElementFocusEvent>().Publish("");
            await Task.Delay(500);
            SelectedUom = SelectedUom.CreateNewUnitOfMeasurement();
            IsNew = true;
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
