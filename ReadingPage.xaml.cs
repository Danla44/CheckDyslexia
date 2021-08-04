using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;

namespace CheckDyslexia
{
    /// <summary>
    /// Interaction logic for ReadingPage.xaml
    /// </summary>
    public partial class ReadingPage : Page
    {
        public ReadingPage()
        {
            InitializeComponent();
        }

        string username;

        public void Button_Test(object sender, RoutedEventArgs e)
        {

            //Empty testbox -> error message
            if (String.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Nincs név megadva! Név megadása kötelező!", "Hiba!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            //Save name as username and init test
            else
            {
                username = textBox1.Text;
                ReadingTestPage testPage = new ReadingTestPage(username);
                this.NavigationService.Navigate(testPage);
            }
        }
    }
}
