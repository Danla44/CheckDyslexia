using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using System.Windows.Navigation;

namespace CheckDyslexia
{
    /// Interaction logic for MainWindow.xaml
    public partial class MainWindow : NavigationWindow
    {
        public MainWindow()
        {
            // Here we attach the current window to the already created instance of Interaction Library WpfBinding host.
            // Note, we are doing this before calling InitializeComponent so that WpfBinding has proper values
            // as soon as the window is loaded, without waiting for NotifyPropertyChanged method to be called.

            //((App)Application.Current).IntlibWpf?.WpfBinding?.AddWindow(this);
            

            InitializeComponent();
        }
    }
}
