using Avalonia.Controls;
using Avalonia.Controls.Templates;
using IM.Client.Features.Main;
using IM.Client.ViewModels;

namespace IM.Client.Navigation;

public class ViewLocator : IDataTemplate
{
    public Control? Build( object? data )
    {
        return data switch
        {
            null => null,
            MainViewModel => new MainView(),
            _ => new TextBlock { Text = "View not found: " + data.GetType().Name }
        };
    }

    public bool Match( object? data ) { return data is ViewModelBase; }
}