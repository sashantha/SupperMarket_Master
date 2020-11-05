using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wingcode.Data.Rest.Model;

namespace Wingcode.Data.Rest.Service
{
    public class ItemRestService
    {
        #region Constant Value

        public static readonly string ITEM_CODE_FLAG = "cd";
        public static readonly string ITEM_BARCODE_FLAG = "bcd";
        public static readonly string ITEM_NAME_FLAG = "nm";

        public static readonly string ITEM_GROUP_FLAG = "ig";
        public static readonly string ITEM_SUBGROUP_FLAG = "isg";

        #endregion

        #region Item Group Rest Service

        public static async Task<ItemGroup> CreateItemGroupAsync(IRestDataMapper mapper, ItemGroup data)
        {
            ItemGroup reds = new ItemGroup();
            if (mapper == null)
                return reds;
            string url = "item/api/v1/itemgroups";
            reds = await mapper.PostDataAsync(url, data);
            return reds;
        }

        public static async Task<ItemGroup> UpdateItemGroupAsync(IRestDataMapper mapper, ItemGroup data)
        {
            ItemGroup reds = new ItemGroup();
            if (mapper == null)
                return reds;
            string url = $"item/api/v1/itemgroups/{data.id}";
            reds = await mapper.PutDataAsync(url, data);
            return reds;
        }

        public static async Task<int> DeleteItemGroupAsync(IRestDataMapper mapper, int itemGroupId)
        {
            int reds = 0;
            if (mapper == null)
                return reds;
            string url = $"item/api/v1/itemgroups/{itemGroupId}";
            reds = await mapper.DeleteDataAsync<int>(url);
            return reds;
        }

        public static async Task<ObservableCollection<ItemGroup>> GetAllItemGroupAsync(IRestDataMapper mapper)
        {
            ObservableCollection<ItemGroup> data = new ObservableCollection<ItemGroup>();
            if (mapper == null)
                return data;
            string url = "item/api/v1/itemgroups";
            data = await mapper.GetDataAsync<ObservableCollection<ItemGroup>>(url);
            return data;
        }

        public static async Task<ItemGroup> GetItemGroupByNameAsync(IRestDataMapper mapper, string groupName)
        {
            ItemGroup reds = new ItemGroup();
            if (mapper == null)
                return reds;
            string url = $"item/api/v1/itemgroups/{groupName}";
            reds = await mapper.GetDataAsync<ItemGroup>(url);
            return reds;
        }

        #endregion

        #region Item Sub Group Rest Service

        public static async Task<ItemSubGroup> CreateItemSubGroupAsync(IRestDataMapper mapper, ItemSubGroup data)
        {
            ItemSubGroup reds = new ItemSubGroup();
            if (mapper == null)
                return reds;
            string url = "item/api/v1/itemsubgroup";
            reds = await mapper.PostDataAsync(url, data);
            return reds;
        }

        public static async Task<ItemSubGroup> UpdateItemSubGroupAsync(IRestDataMapper mapper, ItemSubGroup data)
        {
            ItemSubGroup reds = new ItemSubGroup();
            if (mapper == null)
                return reds;
            string url = $"item/api/v1/itemsubgroup/{data.id}";
            reds = await mapper.PutDataAsync(url, data);
            return reds;
        }

        public static async Task<int> DeleteItemSubGroupAsync(IRestDataMapper mapper, long itemSubGroupId)
        {
            int reds = 0;
            if (mapper == null)
                return reds;
            string url = $"item/api/v1/itemsubgroup/{itemSubGroupId}";
            reds = await mapper.DeleteDataAsync<int>(url);
            return reds;
        }

        public static async Task<ObservableCollection<ItemSubGroup>> GetAllItemSubGroupAsync(IRestDataMapper mapper)
        {
            ObservableCollection<ItemSubGroup> data = new ObservableCollection<ItemSubGroup>();
            if (mapper == null)
                return data;
            string url = "item/api/v1/itemsubgroup";
            data = await mapper.GetDataAsync<ObservableCollection<ItemSubGroup>>(url);
            return data;
        }

        public static async Task<ItemSubGroup> GetItemSubGroupByNameAsync(IRestDataMapper mapper, string subGroupName)
        {
            ItemSubGroup reds = new ItemSubGroup();
            if (mapper == null)
                return reds;
            string url = $"item/api/v1/itemsubgroup/{subGroupName}";
            reds = await mapper.GetDataAsync<ItemSubGroup>(url);
            return reds;
        }

        #endregion

        #region Item rest Service

        public static async Task<Item> CreateItemAsync(IRestDataMapper mapper, Item data)
        {
            Item reds = new Item();
            if (mapper == null)
                return reds;
            string url = "item/api/v1/items";
            reds = await mapper.PostDataAsync(url, data);
            return reds;
        }

        public static async Task<Item> UpdateItemAsync(IRestDataMapper mapper, Item data)
        {
            Item reds = new Item();
            if (mapper == null)
                return reds;
            string url = $"item/api/v1/items/{data.id}";
            reds = await mapper.PutDataAsync(url, data);
            return reds;
        }

        public static async Task<int> DeleteItemAsync(IRestDataMapper mapper, long itemId)
        {
            int reds = 0;
            if (mapper == null)
                return reds;
            string url = $"item/api/v1/items/{itemId}";
            reds = await mapper.DeleteDataAsync<int>(url);
            return reds;
        }

        public static async Task<ObservableCollection<Item>> GetAllItemAsync(IRestDataMapper mapper)
        {
            ObservableCollection<Item> data = new ObservableCollection<Item>();
            if (mapper == null)
                return data;
            string url = "item/api/v1/items";
            data = await mapper.GetDataAsync<ObservableCollection<Item>>(url);
            return data;
        }

        public static async Task<ObservableCollection<Item>> GetItemByGroupOrSubGroupAsync(IRestDataMapper mapper, string flag, string paramVal)
        {
            ObservableCollection<Item> data = new ObservableCollection<Item>();
            if (mapper == null)
                return data;
            string url = $"item/api/v1/items/lists/{flag}/{paramVal}";
            data = await mapper.GetDataAsync<ObservableCollection<Item>>(url);
            return data;
        }

        public static async Task<Item> GetItemByParamAsync(IRestDataMapper mapper, string flag, string paramVal)
        {
            Item reds = new Item();
            if (mapper == null)
                return reds;
            string url = $"item/api/v1/items/{flag}/{paramVal}";
            reds = await mapper.GetDataAsync<Item>(url);
            return reds;
        }

        public static async Task<ObservableCollection<ItemCriteria>> GetItemAttribsByBranchIdAsync(IRestDataMapper mapper)
        {
            ObservableCollection<ItemCriteria> data = new ObservableCollection<ItemCriteria>();
            if (mapper == null)
                return data;
            string url = $"item/api/v1/items/attribs";
            data = await mapper.GetDataAsync<ObservableCollection<ItemCriteria>>(url);
            return data;
        }

        #endregion

        #region Item Store Infor Rest Service

        public static async Task<StoreInfor> CreateStoreInforAsync(IRestDataMapper mapper, StoreInfor data)
        {
            StoreInfor reds = new StoreInfor();
            if (mapper == null)
                return reds;
            string url = "item/api/v1/itemstores";
            reds = await mapper.PostDataAsync(url, data);
            return reds;
        }

        public static async Task<StoreInfor> UpdateStoreInforAsync(IRestDataMapper mapper, StoreInfor data)
        {
            StoreInfor reds = new StoreInfor();
            if (mapper == null)
                return reds;
            string url = $"item/api/v1/itemstores/{data.id}";
            reds = await mapper.PutDataAsync(url, data);
            return reds;
        }

        public static async Task<int> DeleteStoreInforAsync(IRestDataMapper mapper, long storeInforId)
        {
            int reds = 0;
            if (mapper == null)
                return reds;
            string url = $"item/api/v1/itemstores/{storeInforId}";
            reds = await mapper.DeleteDataAsync<int>(url);
            return reds;
        }

        public static async Task<ObservableCollection<StoreInfor>> GetAllStoreInforByItemIdAsync(IRestDataMapper mapper, long itemId)
        {
            ObservableCollection<StoreInfor> data = new ObservableCollection<StoreInfor>();
            if (mapper == null)
                return data;
            string url = $"item/api/v1/itemstores/{itemId}";
            data = await mapper.GetDataAsync<ObservableCollection<StoreInfor>>(url);
            return data;
        }

        public static async Task<StoreInfor> GetStoreInforByItemIdAndBranchAsync(IRestDataMapper mapper, long itemId, int branchId)
        {
            StoreInfor reds = new StoreInfor();
            if (mapper == null)
                return reds;
            string url = $"item/api/v1/itemstores/{itemId}/{branchId}";
            reds = await mapper.GetDataAsync<StoreInfor>(url);
            return reds;
        }

        #endregion

        #region Unit Rest Service

        public static async Task<Unit> CreateUnitAsync(IRestDataMapper mapper, Unit data)
        {
            Unit reds = new Unit();
            if (mapper == null)
                return reds;
            string url = "item/api/v1/units";
            reds = await mapper.PostDataAsync(url, data);
            return reds;
        }

        public static async Task<Unit> UpdateUnitAsync(IRestDataMapper mapper, Unit data)
        {
            Unit reds = new Unit();
            if (mapper == null)
                return reds;
            string url = $"item/api/v1/units/{data.id}";
            reds = await mapper.PutDataAsync(url, data);
            return reds;
        }
                
        public static async Task<ObservableCollection<Unit>> GetAllUnitAsync(IRestDataMapper mapper)
        {
            ObservableCollection<Unit> data = new ObservableCollection<Unit>();
            if (mapper == null)
                return data;
            string url = "item/api/v1/units";
            data = await mapper.GetDataAsync<ObservableCollection<Unit>>(url);
            return data;
        }

        public static async Task<ObservableCollection<Unit>> GetAllUnitByTypeAsync(IRestDataMapper mapper, string unitType)
        {
            ObservableCollection<Unit> data = new ObservableCollection<Unit>();
            if (mapper == null)
                return data;
            string url = $"item/api/v1/units/type/{unitType}";
            data = await mapper.GetDataAsync<ObservableCollection<Unit>>(url);
            return data;
        }

        public static async Task<Unit> GetAllUnitByNameAsync(IRestDataMapper mapper, string unitName)
        {
            Unit data = new Unit();
            if (mapper == null)
                return data;
            string url = $"item/api/v1/units/name/{unitName}";
            data = await mapper.GetDataAsync<Unit>(url);
            return data;
        }

        #endregion

        #region Unit of Measurement Rest Service

        public static async Task<UnitOfMeasurement> CreateUnitOfMeasurementAsync(IRestDataMapper mapper, UnitOfMeasurement data)
        {
            UnitOfMeasurement reds = new UnitOfMeasurement();
            if (mapper == null)
                return reds;
            string url = "item/api/v1/uoms";
            reds = await mapper.PostDataAsync(url, data);
            return reds;
        }

        public static async Task<UnitOfMeasurement> UpdateUnitOfMeasurementAsync(IRestDataMapper mapper, UnitOfMeasurement data)
        {
            UnitOfMeasurement reds = new UnitOfMeasurement();
            if (mapper == null)
                return reds;
            string url = $"item/api/v1/uoms/{data.id}";
            reds = await mapper.PutDataAsync(url, data);
            return reds;
        }

        public static async Task<ObservableCollection<UnitOfMeasurement>> GetAllUnitOfMeasurementAsync(IRestDataMapper mapper)
        {
            ObservableCollection<UnitOfMeasurement> data = new ObservableCollection<UnitOfMeasurement>();
            if (mapper == null)
                return data;
            string url = "item/api/v1/uoms";
            data = await mapper.GetDataAsync<ObservableCollection<UnitOfMeasurement>>(url);
            return data;
        }

        public static async Task<ObservableCollection<UnitOfMeasurement>> GetAllUnitOfMeasurementByTypeAsync(IRestDataMapper mapper, string unitType)
        {
            ObservableCollection<UnitOfMeasurement> data = new ObservableCollection<UnitOfMeasurement>();
            if (mapper == null)
                return data;
            string url = $"item/api/v1/uoms/type/{unitType}";
            data = await mapper.GetDataAsync<ObservableCollection<UnitOfMeasurement>>(url);
            return data;
        }

        public static async Task<UnitOfMeasurement> GetAllUnitOfMeasurementByDescriptionAsync(IRestDataMapper mapper, string desc)
        {
            UnitOfMeasurement data = new UnitOfMeasurement();
            if (mapper == null)
                return data;
            string url = $"item/api/v1/uoms/description/{desc}";
            data = await mapper.GetDataAsync<UnitOfMeasurement>(url);
            return data;
        }

        #endregion
    }
}
