﻿using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace LoanManager.Contracts;

public interface INavigationService
{
   event NavigatedEventHandler Navigated;

    bool CanGoBack { get; }

    Frame?  Frame { get; set; }

    bool GoBack();
    bool NavigateTo(string pageKey, object? parameter = null, bool clearNavigation = false);
    void SetListDataItemForNextConnectedAnimation(object item);
}
