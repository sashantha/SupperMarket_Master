using System;
using System.Diagnostics;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Windows;
using System.Windows.Markup;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: XmlnsPrefix("http://wingcodems.net/base", "wcb")]
[assembly: XmlnsDefinition("http://wingcodems.net/base", "Wingcode.Base.Api")]
[assembly: XmlnsDefinition("http://wingcodems.net/base", "Wingcode.Base.DataModel")]
[assembly: XmlnsDefinition("http://wingcodems.net/base", "Wingcode.Base.Expressions")]
[assembly: XmlnsDefinition("http://wingcodems.net/base", "Wingcode.Base.Native")]
[assembly: XmlnsDefinition("http://wingcodems.net/base", "Wingcode.Base.Extensions")]
[assembly: XmlnsDefinition("http://wingcodems.net/base", "Wingcode.Base.ViewModels")]
[assembly: XmlnsDefinition("http://wingcodems.net/base", "Wingcode.Base.Menus")]
[assembly: XmlnsDefinition("http://wingcodems.net/base", "Wingcode.Base.Animation")]
[assembly: XmlnsDefinition("http://wingcodems.net/base", "Wingcode.Base.AttachedProperties")]
[assembly: XmlnsDefinition("http://wingcodems.net/base", "Wingcode.Base.Event")]
[assembly: XmlnsDefinition("http://wingcodems.net/base", "Wingcode.Base.Input")]

[assembly: CompilationRelaxations(8)]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints)]
[assembly: AssemblyTitle("Wingcode.Base.dll")]
[assembly: AssemblyDescription("Base Functionality")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Wingcode Micro System")]
[assembly: AssemblyProduct("Wingcode Application Framework")]
[assembly: AssemblyCopyright("Copyright (C) 2017-2020 Wingcode ms")]
[assembly: AssemblyTrademark("Wingcode ms")]
[assembly: AllowPartiallyTrustedCallers]
[assembly: AssemblyDelaySign(false)]
[assembly: SecurityRules(SecurityRuleSet.Level1)]
[assembly: CLSCompliant(true)]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

//In order to begin building localizable applications, set 
//<UICulture>CultureYouAreCodingWith</UICulture> in your .csproj file
//inside a <PropertyGroup>.  For example, if you are using US english
//in your source files, set the <UICulture> to en-US.  Then uncomment
//the NeutralResourceLanguage attribute below.  Update the "en-US" in
//the line below to match the UICulture setting in the project file.

//[assembly: NeutralResourcesLanguage("en-US", UltimateResourceFallbackLocation.Satellite)]


[assembly: ThemeInfo(
    ResourceDictionaryLocation.None, //where theme specific resource dictionaries are located
                                     //(used if a resource is not found in the page, 
                                     // or application resource dictionaries)
    ResourceDictionaryLocation.SourceAssembly //where the generic resource dictionary is located
                                              //(used if a resource is not found in the page, 
                                              // app, or any theme specific resource dictionaries)
)]


// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers 
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("2020.1.0.0")]
[assembly: AssemblyFileVersion("2020.1.0.0")]

