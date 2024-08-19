﻿
using CanonCameraExternal_Sample_WPF.Services;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;


namespace CanonCameraExternal_Sample_WPF.Xaml
{
    [ValueConversion(typeof(State?), typeof(Visibility))]
    public class LiveViewStateToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (State?) value == State.Live || (State?) value == State.Stop
                ? Visibility.Visible
                : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}