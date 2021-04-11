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
using System.IO;

namespace CheckDyslexia
{
    /// <summary>
    /// Interaction logic for ReadingTestPage.xaml
    /// </summary>
    /// 

    public partial class ReadingTestPage : Page
    {
        protected Thread eventLoopThread;
        protected StreamWriter outFile;
        protected StreamReader inFile;
        protected Tobii.InteractionLib.IInteractionLib intlib = Tobii.InteractionLib.InteractionLibFactory.CreateInteractionLib(Tobii.InteractionLib.FieldOfUse.Interactive);
        //protected List<Point> coodrinates = new List<Point>();

        public void EventLoop()
        {
            while (true)
            {
                intlib.WaitAndUpdate();
            }
        }

        public static void Circle(double x, double y, int width, int height, Canvas cv)
        {
            Ellipse circle = new Ellipse()
            {
                Width = width,
                Height = height,
                Stroke = Brushes.Red,
                Fill = Brushes.Red,
                StrokeThickness = 6,
                Opacity = 0.3
            };

            cv.Children.Add(circle);

            circle.SetValue(Canvas.LeftProperty, x);
            circle.SetValue(Canvas.TopProperty, y);
        }

        public ReadingTestPage()
        {
            InitializeComponent();

            outFile = new StreamWriter("test.txt");
            
           // Point p = new Point(0, 0);
            //outFile.WriteLine("Mukszik");
            intlib.GazePointDataEvent += evt =>
            {
                //Ez fog lefutni, ha adat érkezik Tobiitól.
               // p.X = evt.x; * SystemParameters.PrimaryScreenWidth;
               // p.Y = evt.y; * SystemParameters.PrimaryScreenHeight;
                //coodrinates.Add(p);
                outFile.WriteLine(evt.x * 800 + ";" + evt.y * 450);
            };
        }

        private void Button_Start_Click(object sender, RoutedEventArgs e)
        {
            eventLoopThread = new Thread(new ThreadStart(EventLoop));
            eventLoopThread.Start();
            BStart.IsEnabled = false;
            BStop.IsEnabled = true;
        }

        private void Button_Stop_Click(object sender, RoutedEventArgs e)
        {
            eventLoopThread.Abort();
            BStop.IsEnabled = false;
            outFile.Close();
            inFile = new StreamReader("test.txt");
            string line;
            
            while ((line = inFile.ReadLine()) != null)
            {
                var extract = line.Split(';');
                double X = Double.Parse(extract[0]);
                double Y = Double.Parse(extract[1]);
                Circle(X, Y, 30, 30, ReadingCanvas);
            }

            ReadingCanvas.UpdateLayout();
            BStart.IsEnabled = true;
        }
    }
}

