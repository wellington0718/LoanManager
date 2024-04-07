using CommunityToolkit.Mvvm.ComponentModel;
using LoanManager.Contracts;
using LoanManager.ViewModels;
using LoanManager.Views;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LoanManager.Services;

public class PageService : IPageService
{
    private readonly Dictionary<string, Type> _pages = [];

    public PageService()
    {
        Configure<ShellViewModel, ShellPage>();
        Configure<MainViewModel, MainPage>();
    }

    public Type GetPageType(string key)
    {
        if (!_pages.TryGetValue(key, out Type? pageType))
        {
            throw new ArgumentException($"Page not found {key}. Did you forget to call PageService.Configure?");
        }

        return pageType;
    }

    private void Configure<VM, V>()
        where VM : ObservableObject
        where V : Page
    {
        var key = typeof(VM).FullName!;

        if (_pages.ContainsKey(key))
        {
            throw new ArgumentException($"Page key: {key} has already been registered.");
        }

        var type = typeof(V);

        if (_pages.ContainsValue(type))
        {
            throw new ArgumentException($"This type has already been registered under the key: {_pages.First(p => p.Value == type).Key}.");
        }

         _pages[key] = type;
    }
}
