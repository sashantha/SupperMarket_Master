using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using Wingcode.Base.DataModel;
using Wingcode.Base.Input;
using Wingcode.Base.Native;

namespace Wingcode.Base.ViewModels
{
    public class WingcodeDialogWindowViewModel : BaseViewModel
    {
        #region Private Member


        //private IContainerProvider container;

        /// <summary>
        /// The window this view model controls
        /// </summary>
        private Window dWindow;

        /// <summary>
        /// The margin around the window to allow for a drop shadow
        /// </summary>
        private int mOuterMarginSize = 5;

        /// <summary>
        /// The radius of the edges of the window
        /// </summary>
        private int mWindowRadius = 10;

        /// <summary>
        /// The last known dock position
        /// </summary>
        private WindowDockPosition mDockPosition = WindowDockPosition.Undocked;

        private Thickness innerContentPadding = new Thickness(0);

        #endregion

        #region Public Properties
        /// <summary>
        /// The Window Title
        /// </summary>
        public string Title { get; set; } = "Supper Market Master";

        /// <summary>
        /// The smallest width the window can go to
        /// </summary>
        public double WindowMinWidth { get; set; } = 350;

        /// <summary>
        /// The smallest height the window can go to
        /// </summary>
        public double WindowMinHeight { get; set; } = 150;

        /// <summary>
        /// True if the window should be borderless because it is docked or maximized
        /// </summary>
        public bool Borderless => (dWindow.WindowState == WindowState.Maximized || mDockPosition != WindowDockPosition.Undocked);
        /// <summary>
        /// The size of the resize border around the window
        /// </summary>
        public int ResizeBorder => Borderless ? 0 : 4;
        /// <summary>
        /// The size of the resize border around the window, taking into account the outer margin
        /// </summary>
        public Thickness ResizeBorderThickness => new Thickness(ResizeBorder + OuterMarginSize);


        public Thickness GetInnerContentPadding()
        {
            return innerContentPadding;
        }

        public void SetInnerContentPadding(Thickness value)
        {
            innerContentPadding = value;
        }

        /// <summary>
        /// The margin around the window to allow for a drop shadow
        /// </summary>
        public int OuterMarginSize
        {
            // If it is maximized or docked, no border
            get => Borderless ? 0 : mOuterMarginSize;
            set => mOuterMarginSize = value;
        }

        /// <summary>
        /// The margin around the window to allow for a drop shadow
        /// </summary>
        public Thickness OuterMarginSizeThickness { get { return new Thickness(OuterMarginSize); } }

        /// <summary>
        /// The radius of the edges of the window
        /// </summary>
        public int WindowRadius
        {
            // If it is maximized or docked, no border
            get => Borderless ? 0 : mWindowRadius;
            set => mWindowRadius = value;
        }

        /// <summary>
        /// The radius of the edges of the window
        /// </summary>
        public CornerRadius WindowCornerRadius => new CornerRadius(WindowRadius);
        /// <summary>
        /// The height of the title bar / caption of the window
        /// </summary>
        public int TitleHeight { get; set; } = 30;
        /// <summary>
        /// The height of the title bar / caption of the window
        /// </summary>
        public GridLength TitleHeightGridLength => new GridLength(TitleHeight + ResizeBorder);





        #endregion

        #region Commands

        /// <summary>
        /// The command to close the window
        /// </summary>
        public ICommand CloseCommand { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public WingcodeDialogWindowViewModel(Window window)
        {
            //this.container = containerProvider;
            dWindow = window;

            // Listen out for the window resizing
            dWindow.StateChanged += (sender, e) =>
            {
                // Fire off events for all properties that are affected by a resize
                WindowResized();
            };

            // Create commands
            CloseCommand = new RelayCommand(() => dWindow.Close());

            // Fix window resize issue
            var resizer = new WindowResizer(dWindow);

            // Listen out for dock changes
            resizer.WindowDockChanged += (dock) =>
            {
                // Store last position
                mDockPosition = dock;

                // Fire off resize events
                WindowResized();
            };
            
        }

        #endregion


        #region Private Helpers

        /// <summary>
        /// Gets the current mouse position on the screen
        /// </summary>
        /// <returns></returns>
        private Point GetMousePosition()
        {

            // Position of the mouse relative to the window
            var position = Mouse.GetPosition(dWindow);
            // Add the window position so its a "ToScreen"
            if (dWindow.WindowState == WindowState.Maximized)
                return new Point(position.X, position.Y);
            else
                return new Point(position.X + dWindow.Left, position.Y + dWindow.Top);
        }

        /// <summary>
        /// If the window resizes to a special position (docked or maximized)
        /// this will update all required property change events to set the borders and radius values
        /// </summary>
        private void WindowResized()
        {
            // Fire off events for all properties that are affected by a resize
            OnPropertyChanged(new PropertyChangedEventArgs(nameof(Borderless)));
            OnPropertyChanged(new PropertyChangedEventArgs(nameof(ResizeBorderThickness)));
            OnPropertyChanged(new PropertyChangedEventArgs(nameof(OuterMarginSize)));
            OnPropertyChanged(new PropertyChangedEventArgs(nameof(OuterMarginSizeThickness)));
            OnPropertyChanged(new PropertyChangedEventArgs(nameof(WindowRadius)));
            OnPropertyChanged(new PropertyChangedEventArgs(nameof(WindowCornerRadius)));
        }


        #endregion
    }
}
