using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace Watcher
{
    public partial class MainWindow : Window
    {
        private TimeSpan showInterval = TimeSpan.FromSeconds(10);
        private TimeSpan hideInterval = TimeSpan.FromMinutes(5);

        // Intervals for easier testing
        //private TimeSpan showInterval = TimeSpan.FromSeconds(3);
        //private TimeSpan hideInterval = TimeSpan.FromSeconds(3);

        private DispatcherTimer popupTimer;
        private int currentPhrase = 10;
        private List<string> phrases = new List<string>
        {
            "Json is watching you!",
            "I know where you live.",
            "Dude, Seriously?",
            "Urge to kill, rising.",
            "Don't make me angry.",
            "Json is not amused.",
            "Ding Dong",
        };

        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // If process is already running, then kill this one
            if (Process.GetProcessesByName("Watcher").Count() > 1)
                Close();

            // Get the screen width to set app location
            var screenWidth = SystemParameters.VirtualScreenWidth;
            Left = screenWidth - 425;
            Top = 50;

            Visibility = Visibility.Collapsed;

            popupTimer = new DispatcherTimer();
            popupTimer.Interval = TimeSpan.FromSeconds(5);
            popupTimer.Tick += charTimer_Tick;
            popupTimer.Start();
        }

        void charTimer_Tick(object sender, EventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                Visibility = Visibility.Collapsed;
                popupTimer.Interval = hideInterval;
            }
            else
            {
                J_sonTextBlock.Text = GetNextPhrase();
                Visibility = Visibility.Visible;
                popupTimer.Interval = showInterval;
            }
        }

        private string GetNextPhrase()
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
}
