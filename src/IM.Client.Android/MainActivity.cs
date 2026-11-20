using Android.App;
using Android.Content.PM;
using Avalonia.Android;

namespace IM.Client.Android;

[Activity(
    Label = "Inventory Manager",
    Theme = "@style/MyTheme.NoActionBar",
    Icon = "@drawable/icon",
    MainLauncher = true,
    ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize | ConfigChanges.UiMode )]
public class MainActivity : AvaloniaMainActivity { }