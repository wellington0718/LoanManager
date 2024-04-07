using LoanManager.Activataion;
using LoanManager.Contracts;
using LoanManager.Views;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoanManager.Services;

public class ActivationService(ActivationHandler<LaunchActivatedEventArgs> defaultHandler, IEnumerable<IActivationHandler> activations) : IActivationService
{
    private readonly ActivationHandler<LaunchActivatedEventArgs> _defaultHandler = defaultHandler;
    private readonly IEnumerable<IActivationHandler> _activations = activations;
    private UIElement? _shell;

    public async Task ActivateAsync(object activationArgs)
    {
        if(App.MainWindow.Content == null)
        {
            _shell = App.MainWindow.Content = App.GetService<ShellPage>();

            App.MainWindow.Content = _shell?? new Frame();
        }

        await HandleActivationAsync(activationArgs);

        App.MainWindow.Activate();
    }

    private async Task HandleActivationAsync(object activationArgs)
    {
        var activationHandler = _activations.FirstOrDefault(h => h.CanHandle(activationArgs));

        if(activationHandler != null)
        {
           await activationHandler.HandleAsync(activationArgs);
        }

        if(_defaultHandler.CanHandle(activationArgs))
        {
           await _defaultHandler.HandleAsync(activationArgs);
        }
    }
}
