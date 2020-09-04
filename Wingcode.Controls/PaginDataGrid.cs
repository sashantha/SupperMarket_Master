using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Wingcode.Controls.Events;
using Wingcode.Controls.Interfaces;

namespace Wingcode.Controls
{
    /// <summary>
    /// Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
    ///
    /// Step 1a) Using this custom control in a XAML file that exists in the current project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:Wingcode.Controls"
    ///
    ///
    /// Step 1b) Using this custom control in a XAML file that exists in a different project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:Wingcode.Controls;assembly=Wingcode.Controls"
    ///
    /// You will also need to add a project reference from the project where the XAML file lives
    /// to this project and Rebuild to avoid compilation errors:
    ///
    ///     Right click on the target project in the Solution Explorer and
    ///     "Add Reference"->"Projects"->[Browse to and select this project]
    ///
    ///
    /// Step 2)
    /// Go ahead and use your control in the XAML file.
    ///
    ///     <MyNamespace:PaginDataGrid/>
    ///
    /// </summary>

    [TemplatePart(Name = "PART_PagingControls", Type = typeof(StackPanel)),
     TemplatePart(Name = "PART_FirstPageButton", Type = typeof(Button)),
     TemplatePart(Name = "PART_PreviousPageButton", Type = typeof(Button)),
     TemplatePart(Name = "PART_PageTextBox", Type = typeof(TextBox)),
     TemplatePart(Name = "PART_PageTotal", Type = typeof(TextBox)),
     TemplatePart(Name = "PART_NextPageButton", Type = typeof(Button)),
     TemplatePart(Name = "PART_LastPageButton", Type = typeof(Button)),
     TemplatePart(Name = "PART_PageSizesCombobox", Type = typeof(ComboBox))]

    public class PaginDataGrid : DataGrid
    {

        #region PrivatePoperty

        protected Button btnFirstPage, btnPreviousPage, btnNextPage, btnLastPage;
        protected TextBox txtPage;
        protected TextBox txtTotalPage;
        protected ComboBox cmbPageSizes;
        protected StackPanel pagingControls;

        #endregion

        #region DependencyProperties

        public ObservableCollection<object> ItemsData
        {
            get { return (ObservableCollection<object>)GetValue(ItemsDataProperty); }
            set { SetValue(ItemsDataProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ItemsData.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemsDataProperty =
            DependencyProperty.Register(nameof(ItemsData), typeof(ObservableCollection<object>), typeof(PaginDataGrid), new PropertyMetadata(new ObservableCollection<object>()));

        public int Page
        {
            get { return (int)GetValue(PageProperty); }
            set { SetValue(PageProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Page.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PageProperty =
            DependencyProperty.Register(nameof(Page), typeof(int), typeof(PaginDataGrid));

        public int TotalPages
        {
            get { return (int)GetValue(TotalPagesProperty); }
            set { SetValue(TotalPagesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TotalPages.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TotalPagesProperty =
            DependencyProperty.Register(nameof(TotalPages), typeof(int), typeof(PaginDataGrid));

        public ObservableCollection<int> PageSizes
        {
            get { return (ObservableCollection<int>)GetValue(PageSizesProperty); }            
        }

        // Using a DependencyProperty as the backing store for PageSizes.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PageSizesProperty =
            DependencyProperty.Register(nameof(PageSizes), typeof(ObservableCollection<int>), typeof(PaginDataGrid), new PropertyMetadata(new ObservableCollection<int>()));

        public IPageControlContract PageContract
        {
            get { return (IPageControlContract)GetValue(PageContractProperty); }
            set { SetValue(PageContractProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PageContractProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PageContractProperty =
            DependencyProperty.Register(nameof(PageContract), typeof(IPageControlContract), typeof(PaginDataGrid));

        public object Filter
        {
            get { return (object)GetValue(FilterProperty); }
            set { SetValue(FilterProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Filter.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FilterProperty =
            DependencyProperty.Register(nameof(Filter), typeof(object), typeof(PaginDataGrid), new FrameworkPropertyMetadata(Target));

        public bool IsPagingEnable
        {
            get { return (bool)GetValue(IsPagingEnableProperty); }
            set { SetValue(IsPagingEnableProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsPagingEnable.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsPagingEnableProperty =
            DependencyProperty.Register(nameof(IsPagingEnable), typeof(bool), typeof(PaginDataGrid), new PropertyMetadata(true));

        public ICommand SortingCommand
        {
            get { return (ICommand)GetValue(SortingCommandProperty); }
            set { SetValue(SortingCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SortCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SortingCommandProperty =
            DependencyProperty.Register(nameof(SortingCommand), typeof(ICommand), typeof(PaginDataGrid), new PropertyMetadata(default(ICommand)));

        #endregion

        #region Events

        public delegate void PageChangedEventHandler(object sender, PageChangedEventArgs args);

        public static readonly RoutedEvent PreviewPageChangeEvent;
        public static readonly RoutedEvent PageChangedEvent;

        public event PageChangedEventHandler PreviewPageChange
        {
            add
            {
                AddHandler(PreviewPageChangeEvent, value);
            }
            remove
            {
                RemoveHandler(PreviewPageChangeEvent, value);
            }
        }

        public event PageChangedEventHandler PageChanged
        {
            add
            {
                AddHandler(PageChangedEvent, value);
            }
            remove
            {
                RemoveHandler(PageChangedEvent, value);
            }
        }
       
        #endregion

        #region Constructor

        static PaginDataGrid()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PaginDataGrid), new FrameworkPropertyMetadata(typeof(PaginDataGrid)));
            PreviewPageChangeEvent = EventManager.RegisterRoutedEvent(nameof(PreviewPageChange), RoutingStrategy.Bubble, typeof(PageChangedEventHandler), typeof(PaginDataGrid));
            PageChangedEvent = EventManager.RegisterRoutedEvent(nameof(PageChanged), RoutingStrategy.Bubble, typeof(PageChangedEventHandler), typeof(PaginDataGrid));
        }

        public PaginDataGrid()
        {
            Loaded += PaginDataGrid_Loaded;
            //Sorting += OnSorting;
        }

        ~ PaginDataGrid()
        {
            UnregisterEvents();            
        }
        #endregion

        #region Functions

        private static void Target(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var target = (PaginDataGrid)d;
            target.Navigate(PageChanges.Current);
        }

        public override void OnApplyTemplate()
        {
            btnFirstPage = GetTemplateChild<Button>("PART_FirstPageButton");
            btnPreviousPage = GetTemplateChild<Button>("PART_PreviousPageButton");
            txtPage =  GetTemplateChild<TextBox>("PART_PageTextBox");
            txtTotalPage = GetTemplateChild<TextBox>("PART_PageTotal");
            btnNextPage = GetTemplateChild<Button>("PART_NextPageButton");
            btnLastPage = GetTemplateChild<Button>("PART_LastPageButton");
            cmbPageSizes = GetTemplateChild<ComboBox>("PART_PageSizesCombobox");
            pagingControls = GetTemplateChild<StackPanel>("PART_PagingControls");

            if (!IsPagingEnable)
            {
                pagingControls.Visibility = Visibility.Collapsed;
            }
            base.OnApplyTemplate();
        }

        protected T GetTemplateChild<T>(string name) where T : DependencyObject
        {
            var child = GetTemplateChild(name) as T;
            if (child == null)
                throw new NullReferenceException(name);
            return child;
        }

        private void RegisterEvents()
        {
            btnFirstPage.Click += new RoutedEventHandler(btnFirstPage_Click);
            btnPreviousPage.Click += new RoutedEventHandler(btnPreviousPage_Click);
            btnNextPage.Click += new RoutedEventHandler(btnNextPage_Click);
            btnLastPage.Click += new RoutedEventHandler(btnLastPage_Click);

            txtPage.LostFocus += new RoutedEventHandler(txtPage_LostFocus);

            cmbPageSizes.SelectionChanged += new SelectionChangedEventHandler(cmbPageSizes_SelectionChanged);
        }

        private void UnregisterEvents()
        {
            btnFirstPage.Click -= btnFirstPage_Click;
            btnPreviousPage.Click -= btnPreviousPage_Click;
            btnNextPage.Click -= btnNextPage_Click;
            btnLastPage.Click -= btnLastPage_Click;

            txtPage.LostFocus -= txtPage_LostFocus;

            cmbPageSizes.SelectionChanged -= cmbPageSizes_SelectionChanged;
        }

        private void SetDefaultValues()
        {
            ItemsData = new ObservableCollection<object>();

            cmbPageSizes.IsEditable = false;
            cmbPageSizes.SelectedIndex = 0;
        }

        private void BindProperties()
        {
            if (!IsPagingEnable)
                return;
            Binding propBinding;

            propBinding = new Binding(nameof(Page));
            propBinding.RelativeSource = new RelativeSource(RelativeSourceMode.TemplatedParent);
            propBinding.Mode = BindingMode.TwoWay;
            propBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            txtPage.SetBinding(TextBox.TextProperty, propBinding);

            //cmbPageSizes.ItemsSource = null;
            List<int> ps = PageSizes.Distinct().ToList();
            PageSizes.Clear();
            ps.ForEach((i) => { PageSizes.Add(i); });
            propBinding = new Binding(nameof(PageSizes));
            propBinding.RelativeSource = new RelativeSource(RelativeSourceMode.TemplatedParent);
            propBinding.Mode = BindingMode.OneTime;
            propBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            cmbPageSizes.SetBinding(ComboBox.ItemsSourceProperty, propBinding);
            //cmbPageSizes.ItemsSource = new ObservableCollection<int>() {10,50,100,150,200,500,1000};

            propBinding = new Binding(nameof(TotalPages));
            propBinding.RelativeSource = new RelativeSource(RelativeSourceMode.TemplatedParent);
            propBinding.Mode = BindingMode.TwoWay;
            propBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            txtTotalPage.SetBinding(TextBox.TextProperty, propBinding);

            propBinding = new Binding(nameof(ItemsData));
            propBinding.RelativeSource = new RelativeSource(RelativeSourceMode.Self);
            propBinding.Mode = BindingMode.TwoWay;
            propBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            SetBinding(ItemsSourceProperty, propBinding);            
        }

        private void OnSorting(object sender, DataGridSortingEventArgs eventArgs)
        {
            eventArgs.Handled = true;
            if (SortingCommand != null)
            {
                var column = eventArgs.Column;
                var sortDirection = GetNextSortDirection(column.SortDirection);
                column.SortDirection = sortDirection;
                SortingCommand.Execute(new PagingDataGridSortData(column.Header.ToString(), sortDirection));
            }
        }

        private ListSortDirection? GetNextSortDirection(ListSortDirection? sortDirection)
        {
            if (!sortDirection.HasValue)
            {
                return ListSortDirection.Ascending;
            }
            else
            {
                if (sortDirection == ListSortDirection.Ascending)
                {
                    return ListSortDirection.Descending;
                }
                else
                {
                    return null;
                }
            }
        }

        private void RaisePageChanged(int OldPage, int NewPage)
        {
            PageChangedEventArgs args = new PageChangedEventArgs(PageChangedEvent, OldPage, NewPage, TotalPages);
            RaiseEvent(args);
        }

        private void RaisePreviewPageChange(int OldPage, int NewPage)
        {
            PageChangedEventArgs args = new PageChangedEventArgs(PreviewPageChangeEvent, OldPage, NewPage, TotalPages);
            RaiseEvent(args);
        }

        private void Navigate(PageChanges change)
        {
            int totalRecords;
            int newPageSize;

            if (PageContract == null)
            {
                return;
            }

            totalRecords = PageContract.GetTotalCount();
            newPageSize = (int)cmbPageSizes.SelectedItem;

            if (totalRecords == 0)
            {
                ItemsData.Clear();
                TotalPages = 1;
                Page = 1;
            }
            else
            {
                TotalPages = (totalRecords / newPageSize) + (int)((totalRecords % newPageSize == 0) ? 0 : 1);
            }

            int newPage = 1;

            switch (change)
            {
                case PageChanges.First:
                    if (Page == 1)
                    {
                        return;
                    }
                    break;
                case PageChanges.Previous:
                    newPage = (Page - 1 > TotalPages) ? TotalPages : (Page - 1 < 1) ? 1 : Page - 1;
                    break;
                case PageChanges.Current:
                    newPage = (Page > TotalPages) ? TotalPages : (Page < 1) ? 1 : Page;
                    break;
                case PageChanges.Next:
                    newPage = (Page + 1 > TotalPages) ? TotalPages : Page + 1;
                    //(Page + 1) < 1 ? 1 :
                    break;
                case PageChanges.Last:
                    if (Page == TotalPages)
                    {
                        return;
                    }
                    newPage = TotalPages;
                    break;
                default:
                    break;
            }

            var startingIndex = (newPage - 1) * newPageSize;

            var oldPage = Page;
            RaisePreviewPageChange(Page, newPage);

            Page = newPage;
            ItemsData.Clear();

            ICollection<object> fetchData = PageContract.GetRecordsBy(startingIndex, newPageSize, Filter);
            foreach (object row in fetchData)
            {
                ItemsData.Add(row);
            }

            RaisePageChanged(oldPage, Page);
        }

        private void PaginDataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            RegisterEvents();
            SetDefaultValues();
            BindProperties();
        }

        private void btnFirstPage_Click(object sender, RoutedEventArgs e)
        {
            Navigate(PageChanges.First);
        }

        private void btnPreviousPage_Click(object sender, RoutedEventArgs e)
        {
            Navigate(PageChanges.Previous);
        }

        private void btnNextPage_Click(object sender, RoutedEventArgs e)
        {
            Navigate(PageChanges.Next);
        }

        private void btnLastPage_Click(object sender, RoutedEventArgs e)
        {
            Navigate(PageChanges.Last);
        }

        private void txtPage_LostFocus(object sender, RoutedEventArgs e)
        {
            Navigate(PageChanges.Current);
        }

        private void cmbPageSizes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Navigate(PageChanges.Current);
        }

        #endregion
    }
}
