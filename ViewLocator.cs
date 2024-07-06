using System;
using Microsoft.Extensions.DependencyInjection;
using UIEA.Services;
using UIEA.ViewModels;

namespace UIEA;

public class ViewModelLocator
{
    public static ViewModelLocator Instance { get; private set; } = null!;
    public IServiceProvider Provider { get; }

    public ViewModelLocator()
    {
        Provider = ConfigureServices();

        Instance = this;
    }

    private IServiceProvider ConfigureServices()
    {
        var container = new ServiceCollection();
        container.AddSingleton<NavigationService>();
        container.AddScoped<MainWindowViewModel>();

        return container.BuildServiceProvider();
    }

    public MainWindowViewModel MainWindow => Provider.GetRequiredService<MainWindowViewModel>();
}