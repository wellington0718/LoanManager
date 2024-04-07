﻿using CommunityToolkit.WinUI.UI.Animations;
using LoanManager.Contracts;
using LoanManager.Helpers;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Diagnostics.CodeAnalysis;

namespace LoanManager.Services;

public class NavigationService(IPageService pageService) : INavigationService
{
    private object? _lastParameterUsed;
    private Frame? _frame;
    private readonly IPageService _pageService = pageService;

    [MemberNotNullWhen(true, nameof(Frame), nameof(_frame))]
    public bool CanGoBack => Frame != null && Frame.CanGoBack;

    public Frame? Frame
    {
        get
        {
            if (_frame is null)
            {
                _frame = App.MainWindow.Content as Frame;

                RegisterFrameEvents();
            }

            return _frame;
        }

        set
        {
            UnRegisterFrameEvents();
            _frame = value;
            RegisterFrameEvents();
        }

    }

    public event NavigatedEventHandler? Navigated;

    public bool GoBack()
    {
        if (CanGoBack)
        {
            var vmBeforeNavigated = _frame.GetPageViewModel();

            if (vmBeforeNavigated is INavigationAware navigationAware)
            {
                navigationAware.OnNavigatedFrom();
            }

            return true;
        }

        return false;
    }

    private void OnNavigated(object sender, NavigationEventArgs e)
    {
        if (sender is Frame frame)
        {
            var clearNavigation = (bool)frame.Tag;

            if (clearNavigation)
            {
                frame.BackStack.Clear();
            }

            if (frame.GetPageViewModel() is INavigationAware navigationAware)
            {
                navigationAware.OnNavigatedTo(e.Parameter);
            }

            Navigated?.Invoke(sender, e);
        }
    }

    private void UnRegisterFrameEvents()
    {
        if (_frame is not null)
        {
            _frame.Navigated -= OnNavigated;
        }
    }

    private void RegisterFrameEvents()
    {
        if (_frame is not null)
        {
            _frame.Navigated += OnNavigated;
        }
    }

    public bool NavigateTo(string pageKey, object? parameter = null, bool clearNavigation = false)
    {
        var pageType = _pageService.GetPageType(pageKey);

        if (_frame is not null && _frame.Content?.GetType() != pageType && parameter is not null && !parameter.Equals(_lastParameterUsed))
        {
            _frame.Tag = clearNavigation;
            var vmBeforeNavigation = _frame.GetPageViewModel();
            var navigated = _frame.Navigate(pageType, parameter);

            if (navigated)
            {
                _lastParameterUsed = parameter;

                if (vmBeforeNavigation is INavigationAware navigationAware)
                {
                    navigationAware.OnNavigatedFrom();
                }
            }

            return navigated;
        }

        return false;
    }

    public void SetListDataItemForNextConnectedAnimation(object item) => Frame.SetListDataItemForNextConnectedAnimation(item);
}
