using MaterialDesignThemes.Wpf;
using Prism.Commands;
using Prism.Ioc;
using Prism.Services.Dialogs;
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

        private string _searchText;
        public string SearchText
        {
            get { return _searchText; }
            set { SetProperty(ref _searchText, value); }
        }

        private Item _selectedItem;
        public Item SelectedItem
        {
            get { return _selectedItem; }
            set { SetProperty(ref _selectedItem, value); }
        }

        private ObservableCollection<Item> _sourceItems;
        public ObservableCollection<Item> SourceItems
        {
            get { return _sourceItems; }
            set { SetProperty(ref _sourceItems, value); }
        }

        #endregion

        #region Commands

        private DelegateCommand _searchCommand;
        public DelegateCommand SearchCommand
        {
            get { return _searchCommand; }
            set { SetProperty(ref _searchCommand, value); }
        }

        private ICommand _itemEditorCommand;
        public ICommand ItemEditorCommand
        {
            get { return _itemEditorCommand; }
            set { SetProperty(ref _itemEditorCommand, value); }
        }

        private DelegateCommand _groupEditorCommand;
        public DelegateCommand GroupEditorCommand
        {
            get { return _groupEditorCommand; }
            set { SetProperty(ref _groupEditorCommand, value); }
        }

        private DelegateCommand _subGroupEditorCommand;
        public DelegateCommand SubGroupEditorCommand
        {
            get { return _subGroupEditorCommand; }
            set { SetProperty(ref _subGroupEditorCommand, value); }
        }

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
