using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;
using Wingcode.Base.Api;
using Wingcode.SupperMarket.ViewModels;
using Wingcode.SupperMarket.Views;

namespace Wingcode.SupperMarket
{
    public class SplashManager : ISplashManager
    {

        private SplashWindow mSplashWindow;
        private SplashWindowViewModel viewModel;
        private AutoResetEvent autoResetEvent;
        public SplashManager()
        {
            mSplashWindow = new SplashWindow();
            viewModel = new SplashWindowViewModel();
            viewModel.WindowMinimumWidth = 473.0;
            viewModel.WindowMinimumHeight = 300.0;
            mSplashWindow.ViewModel = viewModel;
            autoResetEvent = new AutoResetEvent(false);

        }
        public void CloseSplashScreen()
        {
            if (mSplashWindow == null)
                return;
            Application.Current.Dispatcher.Invoke(() => mSplashWindow.Close());
        }

        public async Task ShowSplashScreen()
        {
            if (mSplashWindow == null)
                return;
            await Task.Run(() => showSplash());
        }

        private void showSplash()
        {
            ThreadStart threadStart = () => 
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    mSplashWindow.Show();
                    autoResetEvent.Set();

                });
            };

            var thread = new Thread(threadStart) { Name = "Splash Thread", IsBackground = true };
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start(); 
            autoResetEvent.WaitOne();
            //    await Application.Current.Dispatcher.BeginInvoke((Action)async delegate
            //     {
            //         mSplashWindow.Show();
            //         Thread.Sleep(4000);
            //         await Task.Delay(4000);
            //         autoResetEvent.Set();
            //     });
            //autoResetEvent.WaitOne();
            //await Task.Delay(0);
        }

        public async void UpdateState(string text)
        {
            viewModel.State = text;
            await Task.Delay(1000);
        }
    }
}
