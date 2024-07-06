using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using I18N.Avalonia;
using I18N.Avalonia.Interface;
using Splat;
using UIEA.Services;
using UIEA.ViewModels;
using UIEA.Views;

namespace UIEA;

public partial class App : Application
{
    public override void Initialize()
    {
        ConfigService.LoadConfig();
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                DataContext = new MainWindowViewModel(),
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
    
    public override void RegisterServices()
    {
        base.RegisterServices();
        Locator.CurrentMutable.RegisterLazySingleton(() => new Localizer(I18N.Resources.ResourceManager),
            typeof(ILocalizer));
    }
}