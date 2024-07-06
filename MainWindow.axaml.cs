using System;
using Avalonia.Controls;
using FluentAvalonia.Core;
using FluentAvalonia.UI.Controls;
using FluentAvalonia.UI.Media.Animation;
using Microsoft.Extensions.DependencyInjection;
using UIEA.Models;
using UIEA.Services;
using UIEA.Views;

namespace UIEA;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        ViewModelLocator.Instance.Provider.GetRequiredService<NavigationService>().RegisterHandler(Navigate);

        var menu = new PageLink[]
        {
            new("Enhanced", "StarEmphasis", typeof(Enhanced)),
            new("Album", "ImageMultiple ", typeof(Album))
        };

        var footer = new PageLink[]
        {
            new("Settings", "Settings", typeof(Config))
        };

        Navigation.MenuItemsSource = menu;
        Navigation.FooterMenuItemsSource = footer;
        Navigation.SelectedItem = IEnumerableExtensions.ElementAt(Navigation.MenuItemsSource, 0);
        Navigation.IsPaneOpen = false;
        Navigate(menu[0].Page);
    }

    private void Navigate(Type pageType, object? parameter = null, NavigationTransitionInfo? transition = null)
    {
        if (transition != null)
        {
            Root.Navigate(pageType, parameter);
        }
        else if (parameter != null)
        {
            Root.Navigate(pageType, parameter);
        }
        else
        {
            Root.Navigate(pageType);
        }
    }

    private void Navigation_OnItemInvoked(object? sender, NavigationViewItemInvokedEventArgs e)
    {
        if (e.InvokedItemContainer.Tag is PageLink link)
            ViewModelLocator.Instance.Provider.GetRequiredService<NavigationService>()
                .Navigate(link.Page, e.RecommendedNavigationTransitionInfo);
    }
}