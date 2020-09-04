using MaterialDesignColors;
using MaterialDesignThemes.Wpf;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using Wingcode.Base.DataModel;
using Wingcode.Base.Input;

namespace Wingcode.SupperMarket.ViewModels
{
    public class ThemeSelectorViewModel : BaseViewModel
    {
        public IEnumerable<Swatch> Swatches { get; }

        public ICommand ToggleBaseCommand { get; } = new RelayParameterizedCommand(p => ApplyBase((bool)p));

        public ICommand ApplyPrimaryCommand { get; } = new RelayParameterizedCommand(s => ApplyPrimary((Swatch)s));

        public ICommand ApplyAccentCommand { get; } = new RelayParameterizedCommand(s => ApplyAccent((Swatch)s));

        public ThemeSelectorViewModel()
        {
            Swatches = new SwatchesProvider().Swatches;
        }

        private static void ApplyBase(bool isDark)
        {
            ModifyTheme(theme => theme.SetBaseTheme(isDark ? Theme.Dark : Theme.Light));
        }

        private static void ApplyPrimary(Swatch swatch)
        {
            ModifyTheme(theme => theme.SetPrimaryColor(swatch.ExemplarHue.Color));
        }

        private static void ApplyAccent(Swatch swatch)
        {
            ModifyTheme(theme => theme.SetSecondaryColor(swatch.AccentExemplarHue.Color));
        }

        private static void ModifyTheme(Action<ITheme> modificationAction)
        {
            PaletteHelper paletteHelper = new PaletteHelper();
            ITheme theme = paletteHelper.GetTheme();

            modificationAction?.Invoke(theme);

            paletteHelper.SetTheme(theme);
        }

    }
}
