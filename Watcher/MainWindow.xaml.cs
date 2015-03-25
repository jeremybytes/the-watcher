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
        DispatcherTimer charTimer;
        int currentPhrase = 10;
        List<string> phrases = new List<string>
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
                this.Close();

            // Get the screen width to set app location
            var screenWidth = SystemParameters.VirtualScreenWidth;
            this.Left = screenWidth - 425;
            this.Top = 50;

            this.Visibility = Visibility.Collapsed;

            charTimer = new DispatcherTimer();
            charTimer.Interval = new TimeSpan(0, 0, 5);
            charTimer.Tick += charTimer_Tick;
            charTimer.Start();
        }

        void charTimer_Tick(object sender, EventArgs e)
        {
            if (this.Visibility == Visibility.Visible)
            {
                this.Visibility = Visibility.Collapsed;
                charTimer.Interval = new TimeSpan(0, 5, 0);
            }
            else
            {
                currentPhrase++;
                if (currentPhrase > phrases.Count - 1)
                    currentPhrase = 0;
                J_sonTextBlock.Text = phrases[currentPhrase];
                this.Visibility = Visibility.Visible;
                charTimer.Interval = new TimeSpan(0, 0, 10);
            }
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
