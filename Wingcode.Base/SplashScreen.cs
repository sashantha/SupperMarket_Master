using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wingcode.Base.Api;

namespace Wingcode.Base
{
    public class SplashScreen
    {
		private ISplashManager splashManager;
		

		private static readonly SplashScreen splash = new SplashScreen();

		private SplashScreen()
		{			
		}


		public static SplashScreen GetSplash() 
		{			
			return splash;
		}

		public void SetSplashManager(ISplashManager manager) 
		{
			splashManager = manager;
		}

		public void Show()
		{
			if (splashManager != null)
			{
				splashManager.ShowSplashScreen();
			}

		}

		public void Close()
		{
            if (splashManager != null)
            {
                splashManager.CloseSplashScreen();
            }
            //splashManager.CloseSplashScreen();
            splashManager = null;
		}

		public void UpdateState(string text)
		{
			if (splashManager != null)
			{
				splashManager.UpdateState(text);
			}
		}
	}
}
