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
using Microsoft.Win32;

namespace CheckDyslexia
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
        }

        public void Button_Test(object sender, RoutedEventArgs e) //At button click takes to TestPage
        {
            ReadingPage testPage = new ReadingPage();
            this.NavigationService.Navigate(testPage);
        }

        public void Button_Save(object sender, RoutedEventArgs e)//Opens file dialog 
        {
            //    SavePage savePage = new SavePage();
            //    this.NavigationService.Navigate(savePage);

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = @"C:\prog\CheckDyslexia\saves";
            if (ofd.ShowDialog() == true)
            {
                ImageWindow imageWindow = new ImageWindow(); //Opens new ImageWinndow and show chosen image in it
                imageWindow.Show();
                imageWindow.imgPhoto.Source = new BitmapImage(new Uri(ofd.FileName));
            }
        }
    }

}
