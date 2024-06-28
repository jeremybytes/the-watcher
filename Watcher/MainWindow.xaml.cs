using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace watcher;

public partial class MainWindow : Window
{
    private DispatcherTimer charTimer;
    private readonly TimeSpan startInterval = new(0, 0, 5);
    private readonly TimeSpan showInterval = new(0, 0, 10);
    private readonly TimeSpan gapInterval = new(0, 5, 0);

    private int currentPhrase = 10;
    private List<string> phrases = [
        "Json is watching you!",
        "I know where you live.",
        "Dude, Seriously?",
        "Urge to kill, rising.",
        "Don't make me angry.",
        "Json is not amused.",
        "Ding Dong",
    ];

    public MainWindow()
    {
        InitializeComponent();
        charTimer = new();
        charTimer.Tick += charTimer_Tick;
        charTimer.Interval = startInterval;

        Loaded += MainWindow_Loaded;
    }

    void MainWindow_Loaded(object sender, RoutedEventArgs e)
    {
        // If process is already running, then kill this one
        if (Process.GetProcessesByName("Watcher").Count() > 1)
            Close();

        // Get the screen width and set app location
        var screenWidth = SystemParameters.VirtualScreenWidth;
        Left = screenWidth - 425;
        Top = 50;

        Visibility = Visibility.Collapsed;

        charTimer.Start();
    }

    private void charTimer_Tick(object? sender, EventArgs e)
    {
        if (Visibility == Visibility.Visible)
        {
            charTimer.Interval = gapInterval;
            Visibility = Visibility.Collapsed;
        }
        else
        {
            charTimer.Interval = showInterval;
            J_sonTextBlock.Text = NextPhrase();
            Visibility = Visibility.Visible;
        }
    }

    private string NextPhrase()
    {
        currentPhrase++;
        if (currentPhrase > phrases.Count - 1)
            currentPhrase = 0;
        return phrases[currentPhrase];
    }

    private void Border_MouseDown(object sender, MouseButtonEventArgs e)
    {
        DragMove();
    }
}
