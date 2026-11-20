using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using IM.Client.Features.Main;

namespace IM.Client;

public class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load( this );
#if DEBUG
        this.AttachDeveloperTools();
#endif
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if ( ApplicationLifetime is IActivityApplicationLifetime activity )
            activity.MainViewFactory = CreateMainView;
        else if ( ApplicationLifetime is ISingleViewApplicationLifetime singleView )
            singleView.MainView = CreateMainView();

        base.OnFrameworkInitializationCompleted();
    }

    private static MainView CreateMainView()
    {
        return new MainView
        {
            DataContext = new MainViewModel()
        };
    }
}