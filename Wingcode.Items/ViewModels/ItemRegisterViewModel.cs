using MaterialDesignThemes.Wpf;
using Prism.Commands;
using Prism.Ioc;
using Prism.Services.Dialogs;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Wingcode.Base.DataModel;
using Wingcode.Base.Extensions;
using Wingcode.Base.Input;
using Wingcode.Data.Rest.Model;
using Wingcode.Data.Rest.Service;
using Wingcode.Items.Views;

namespace Wingcode.Items.ViewModels
{
    public class ItemRegisterViewModel : BaseViewModel
    {

        #region Property

        private IContainerExtension containerExtension;
        private ILoggedUserProvider loggedUser;
        private IDialogService dialogService;

        public string SearchText { get; set; }

        public Item SelectedItem { get; set; }

        public ObservableCollection<Item> SourceItems { get; set; }

        #endregion

        #region Commands

        public DelegateCommand SearchCommand { get; set; }

        public ICommand ItemEditorCommand { get; set; }

        public DelegateCommand GroupEditorCommand { get; set; }

        public DelegateCommand SubGroupEditorCommand { get; set; }

        public DelegateCommand UomEditorCommand { get; set; }

        #endregion

        #region Constructor

        public ItemRegisterViewModel(IContainerExtension container)
        {
            containerExtension = container;
            SearchText = string.Empty;
            SearchCommand = new DelegateCommand(SearchItem);
            ItemEditorCommand = new RelayParameterizedCommand((p) => ShowItemEditor(bool.Parse(p.ToString())));
            GroupEditorCommand = new DelegateCommand(ShowGroupEditor);
            SubGroupEditorCommand = new DelegateCommand(ShowSubGroupEditor);
            UomEditorCommand = new DelegateCommand(ShowUomEditor);
            loggedUser = containerExtension.Resolve<ILoggedUserProvider>();
            dialogService = containerExtension.Resolve<IDialogService>();
        }

        #endregion

        #region Function And Event

        internal void Initialize() 
        {
            LoadAllItem();
        }

        private async void LoadAllItem() 
        {
            IRestDataMapper mapper = containerExtension.Resolve<IRestDataMapper>();
            SourceItems = await ItemRestService.GetAllItemAsync(mapper);
        }

        private async void ShowSubGroupEditor()
        {
            _= await DialogHost.Show(new ItemSubGroupEditor() 
            { 
                DataContext = new ItemSubGroupEditorViewModel(containerExtension, loggedUser)
            }, "ItemRoot", EditorClosing);
        }

        private async void ShowGroupEditor()
        {
            _= await DialogHost.Show(new ItemGroupEditor() 
            {
                DataContext = new ItemGroupEditorViewModel(containerExtension, loggedUser)
            }, "ItemRoot", EditorClosing);
        }

        private async void ShowUomEditor()
        {
            _ = await DialogHost.Show(new UnitOfMeasureEditor()
            {
                DataContext = new UnitOfMeasureEditorViewModel(containerExtension, loggedUser)
            }, "ItemRoot", EditorClosing);
        }

        private async void ShowItemEditor(bool isNew)
        {
            ItemEditorViewModel itemEditor = new ItemEditorViewModel(containerExtension, loggedUser);
            itemEditor.IsNew = isNew;
            if (!isNew)
            {
                if (SelectedItem != null)
                {
                    itemEditor.SelectedItem = SelectedItem;
                }
                else
                {
                    _ = ShowMessageDialg("Item Update", "Please Select Item Before Update", MsgDialogType.warrning);
                    return;
                }
            }
            itemEditor.SyncEditro += ItemEditor_SyncEditro;
            _ = await DialogHost.Show(new ItemEditor()
            {
                DataContext = itemEditor
            }, "ItemRoot", EditorClosing);
        }

        private void ItemEditor_SyncEditro()
        {
            LoadAllItem();
        }

        private void EditorClosing(object sender, DialogClosingEventArgs eventArgs)
        {
            LoadAllItem();
        }

        private void SearchItem()
        {

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
