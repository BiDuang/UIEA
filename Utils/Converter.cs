using System;
using System.Globalization;
using Avalonia;
using Avalonia.Data;
using Avalonia.Data.Converters;
using Avalonia.Platform;
using Avalonia.Styling;
using UIEA.I18N;
using UIEA.Models;
using UIEA.Services;

namespace UIEA.Utils;

public class I18NConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not string key) return new BindingNotification(new InvalidCastException(), BindingErrorType.Error);

        return Resources.ResourceManager.GetString(key, ConfigService.Instance!.GetAppCultureInfo());
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) =>
        throw new NotSupportedException();
}

public static class ThemeConverter
{
    public static ThemeVariant PlatformThemeVar2AppThemeVar(PlatformThemeVariant systemTheme)
    {
        return systemTheme switch
        {
            PlatformThemeVariant.Light => ThemeVariant.Light,
            PlatformThemeVariant.Dark => ThemeVariant.Dark,
            _ => throw new ArgumentOutOfRangeException(nameof(systemTheme), systemTheme, null)
        };
    }

    public static ThemeVariant ThemeMode2AppThemeVar(ThemeModeEnum themeModeEnum)
    {
        return themeModeEnum switch
        {
            ThemeModeEnum.Light => ThemeVariant.Light,
            ThemeModeEnum.Dark => ThemeVariant.Dark,
            ThemeModeEnum.SyncWithSystem => PlatformThemeVar2AppThemeVar(Application.Current!.PlatformSettings!
                .GetColorValues().ThemeVariant),
            _ => throw new ArgumentOutOfRangeException(nameof(themeModeEnum), themeModeEnum, null)
        };
    }
}