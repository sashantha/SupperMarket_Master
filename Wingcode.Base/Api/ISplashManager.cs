using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wingcode.Base.Api
{
    public interface ISplashManager
    {
        Task ShowSplashScreen();

        void CloseSplashScreen();

        void UpdateState(string text);
    }
}
