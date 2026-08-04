using Avalonia.Controls;

namespace BrasilLauncher.Views {
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
        }

        private void WebView_NavigationCompleted(object? sender, WebViewNavigationCompletedEventArgs args) {
            if (args.IsSuccess) {
                // Navigation completed successfully
            }
        }
    }
}