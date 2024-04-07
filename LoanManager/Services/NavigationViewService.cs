using LoanManager.Contracts;
using LoanManager.Helpers;
using LoanManager.ViewModels;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LoanManager.Services;

public class NavigationViewService(INavigationService navigationService, IPageService pageService) : INavigationViewService
{
    private readonly INavigationService _navigationService = navigationService;
    private readonly IPageService _pageService = pageService;
    private NavigationView? _navigationView;

    public IList<object>? MeniItems => _navigationView?.MenuItems;

    public object? SettingsItem => _navigationView?.SettingsItem;

    public NavigationViewItem? GetSelectedIten(Type pageType)
    {
        if(_navigationView is not null)
        {
            return GetSelectedIten(_navigationView.MenuItems, pageType) ??
                GetSelectedIten(_navigationView.FooterMenuItems, pageType);
        }

        return null;
    }

    private NavigationViewItem? GetSelectedIten(IList<object> menuItems, Type pageType)
    {
        foreach(var menuItem in menuItems.OfType<NavigationViewItem>()) 
        {
            if(IsMenuItemForPageType(menuItem, pageType))
            {
                return menuItem;
            }

            var selectedItem = GetSelectedIten(menuItem.MenuItems, pageType);

            if(selectedItem != null)
            {
                return selectedItem;
            }
        }

        return null;
    }

    private bool IsMenuItemForPageType(NavigationViewItem menuItem, Type pageType)
    {
       if(menuItem.GetValue(NavigationHelper.NavigateToProperty) is string pageKey)
        {
            return _pageService.GetPageType(pageKey) == pageType;   
        }

       return false;
    }

    public void Initialize(NavigationView navigationView)
    {
        _navigationView = navigationView;
        _navigationView.BackRequested += OnBackRequested;
        _navigationView.ItemInvoked += OnItemInvoked;
    }

    private void OnItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
    {
        if (args.IsSettingsInvoked)
        {
            _navigationService.NavigateTo(typeof(SettingsViewModel).FullName!);
        }
        else
        {
            var selectedItem = args.InvokedItemContainer as NavigationViewItem;

            if (selectedItem?.GetValue(NavigationHelper.NavigateToProperty) is
                string pageKey)
            {
                _navigationService.NavigateTo(pageKey);
            }
        }
    }

    private void OnBackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args) => _navigationService.GoBack();


    public void UnRegisterEvents()
    {
        if (_navigationView != null)
        {
            _navigationView.BackRequested -= OnBackRequested;
            _navigationView.ItemInvoked -= OnItemInvoked;
        }
    }
}
