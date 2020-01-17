﻿using NHMCore.Configs;
using System.Collections.Generic;
using System.Windows;

namespace NHM.Wpf
{
    internal static class ThemeSetterManager
    {
        private static List<IThemeSetter> _themeSetters = new List<IThemeSetter>();
        private static bool IsLight = true;

        static ThemeSetterManager()
        {
            GUISettings.Instance.PropertyChanged += Instance_PropertyChanged;
        }

        private static void Instance_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(GUISettings.DisplayTheme))
            {
                SetTheme(GUISettings.Instance.DisplayTheme);
            }
        }

        internal static void AddThemeSetter(IThemeSetter setter)
        {
            if (!_themeSetters.Contains(setter)) {
                _themeSetters.Add(setter);
                setter.SetTheme(IsLight);
            }
        }

        internal static void RemoveThemeSetter(IThemeSetter setter)
        {
            _themeSetters.Remove(setter);
        }

        internal static void SetThemeSelectedThemes()
        {
            SetTheme(IsLight);
        }

        internal static void SetTheme(string displayTheme)
        {
            IsLight = displayTheme == "Light";
            SetTheme(IsLight);
        }
        
        internal static void SetTheme(bool isLight)
        {
            if (isLight)
            {
                // LIGHT
                Application.Current.Resources["NHMLogo"] = Application.Current.FindResource("NHMLogoLightBrush");
                
                Application.Current.Resources["BackgroundColor"] = Application.Current.FindResource("Brushes.Light.Grey.Grey4Background");
                Application.Current.Resources["BorderColor"] = Application.Current.FindResource("Brushes.Light.Border");
                Application.Current.Resources["LoginCircle"] = Application.Current.FindResource("LoginCircleLogoLightBrush");
                Application.Current.Resources["TextColorBrush"] = Application.Current.FindResource("Brushes.Light.TextColor");
            }
            else
            {
                // DARK
                Application.Current.Resources["NHMLogo"] = Application.Current.FindResource("NHMLogoDarkBrush");
                
                Application.Current.Resources["BackgroundColor"] = Application.Current.FindResource("Brushes.Dark.Grey.Grey1Background");
                Application.Current.Resources["BorderColor"] = Application.Current.FindResource("Brushes.Dark.Border");
                Application.Current.Resources["LoginCircle"] = Application.Current.FindResource("LoginCircleLogoDarkBrush");
                Application.Current.Resources["TextColorBrush"] = Application.Current.FindResource("Brushes.Dark.TextColor");
            }
            // Set the value
            foreach (var setter in _themeSetters)
            {
                setter.SetTheme(isLight);
            }
        }
    }
}