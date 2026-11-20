using System;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using IM.Client.Features.Main;

namespace IM.Client.Desktop;

internal sealed class Program
{
    // Initialization code. Don't use any Avalonia, third-party APIs or any
    // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
    // yet and stuff might break.
    [STAThread]
    public static void Main( string[] args )
    {
        BuildAvaloniaApp()
            .StartWithClassicDesktopLifetime( args );
    }

    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp()
    {
        return AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace()
            .AfterSetup( builder =>
            {
                if ( builder.Instance?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop )
                {
                    desktop.MainWindow = new MainWindow
                    {
                        DataContext = new MainViewModel()
                    };
                }
            } );
    }
}