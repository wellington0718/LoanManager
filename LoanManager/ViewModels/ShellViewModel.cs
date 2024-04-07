using CommunityToolkit.Mvvm.ComponentModel;
using LoanManager.Contracts;
using LoanManager.Views;
using Microsoft.UI.Xaml.Navigation;

namespace LoanManager.ViewModels;

public partial class ShellViewModel : ObservableRecipient
{
    [ObservableProperty]
    private bool isBackEnabled;

    [ObservableProperty]
    private object? selected;

    public INavigationService NavigationService { get; }
    public INavigationViewService NavigationViewService { get; }

    public ShellViewModel(INavigationService navigationService, INavigationViewService navigationViewService)
    {
        NavigationService = navigationService;
        NavigationViewService = navigationViewService;
        NavigationService.Navigated += OnNavigated;
    }

    private void OnNavigated(object sender, NavigationEventArgs e)
    {
        IsBackEnabled = NavigationService.CanGoBack;

        if (e.SourcePageType == typeof(SettingsPage)) { Selected = NavigationViewService.SettingsItem; return; }

        var selectedItem = NavigationViewService.GetSelectedIten(e.SourcePageType);

        if (selectedItem is not null) Selected = selectedItem;
    }
}
