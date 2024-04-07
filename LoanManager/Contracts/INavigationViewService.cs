using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;

namespace LoanManager.Contracts;

public interface INavigationViewService
{
    IList<object>? MeniItems {  get; } 
    object? SettingsItem { get; }
    void Initialize(NavigationView navigationView);
    void UnRegisterEvents();
    NavigationViewItem? GetSelectedIten(Type pageType);
}
