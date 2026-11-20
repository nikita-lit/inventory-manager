using CommunityToolkit.Mvvm.ComponentModel;
using IM.Client.ViewModels;

namespace IM.Client.Features.Main;

public partial class MainViewModel : ViewModelBase
{
    [ObservableProperty]
    public partial string Greeting { get; set; } = "Welcome to Avalonia!";
}