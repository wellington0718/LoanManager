using LoanManager.Contracts;
using LoanManager.ViewModels;
using Microsoft.UI.Xaml;
using System.Threading.Tasks;

namespace LoanManager.Activataion;

public class DefaultActivationHandler(INavigationService navigationService) : ActivationHandler<LaunchActivatedEventArgs>
{
    private readonly INavigationService _navigationService = navigationService;

    protected override bool CanHandleInternal(LaunchActivatedEventArgs arg) => _navigationService.Frame?.Content == null;  

    protected override Task HandleInternalAsync(LaunchActivatedEventArgs arg)
    {
        _navigationService.NavigateTo(typeof(MainViewModel).FullName!, arg.Arguments);
        return Task.CompletedTask;
    }
   
}
