namespace AvaloniaListBoxBug1
{
    using Avalonia;
    using Avalonia.ReactiveUI;

    class Program
    {
        public static int Main(string[] args)
        {
            return BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
        }

        public static AppBuilder BuildAvaloniaApp()
        {
            var result = AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .LogToTrace()
                .UseReactiveUI();

            return result;
        }
    }
}
