using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Wingcode.Base.DataModel;

namespace Wingcode.SupperMarket.ViewModels
{
    public class SplashWindowViewModel : BaseViewModel
    {

        public double WindowMinimumWidth { get; set; }
        public double WindowMinimumHeight { get; set; }
        public string Title { get; set; }
        public string Version { get; set; }
        public string Copyright { get; set; }
        public string Description { get; set; }
        public string State { get; set; }

        public SplashWindowViewModel()
        {
            Assembly executingAssembly = Assembly.GetExecutingAssembly();
            FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(executingAssembly.Location);
            this.Version = "Version : " + versionInfo.ProductVersion;
            this.Copyright = versionInfo.LegalCopyright;
            Type attributeType = typeof(AssemblyDescriptionAttribute);
            if (!Attribute.IsDefined(executingAssembly, attributeType))
                return;
            this.Description = ((AssemblyDescriptionAttribute)Attribute.GetCustomAttribute(executingAssembly, attributeType)).Description;
        }
    }
}
