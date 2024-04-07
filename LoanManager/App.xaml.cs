using LoanManager.Activataion;
using LoanManager.Contracts;
using LoanManager.Services;
using LoanManager.ViewModels;
using LoanManager.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.UI.Xaml;
using System;

namespace LoanManager;

public partial class App : Application
{
    public IHost Host { get; }

    public static T GetService<T>()
    {
        if((Current as App)!.Host.Services.GetService(typeof(T)) is not T service)
        {
            throw new ArgumentException($"Service  {typeof(T)} needs to be registered in ConfigureServices within the App.xaml.");
        }

        return service;
    }

    public static WindowEx MainWindow { get; } = new MainWindow();

    public App()
    {
        InitializeComponent();

        Host = Microsoft.Extensions.Hosting.Host
            .CreateDefaultBuilder()
            .UseContentRoot(AppContext.BaseDirectory)
            .ConfigureServices((contetx, services) =>
            {
                services.AddSingleton<IActivationService, ActivationService>();
                services.AddSingleton<INavigationService, NavigationService>();
                services.AddSingleton<INavigationViewService, NavigationViewService>();
                services.AddSingleton<IPageService, PageService>();
                services.AddSingleton<ActivationHandler<LaunchActivatedEventArgs>, DefaultActivationHandler>();

                services.AddTransient<MainViewModel>();
                services.AddTransient<ShellViewModel>();

                services.AddTransient<ShellPage>();
                services.AddTransient<MainPage>();
            })
            .Build();
    }

    protected override void OnLaunched(LaunchActivatedEventArgs args)
    {
        try
        {
            GetService<IActivationService>().ActivateAsync(args);
        }
        catch (Exception ex)
        {

            throw;
        }
    }
}
