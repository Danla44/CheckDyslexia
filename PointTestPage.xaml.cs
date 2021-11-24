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
using System.Windows.Threading;

namespace CheckDyslexia
{
    /// <summary>
    /// Interaction logic for PointTestPage.xaml
    /// </summary>
    public partial class PointTestPage : Page
    {
        protected Thread eventLoopThread;
        protected StreamWriter outFile;
        protected StreamReader inFile;
        protected Tobii.InteractionLib.IInteractionLib intlib = Tobii.InteractionLib.InteractionLibFactory.CreateInteractionLib(Tobii.InteractionLib.FieldOfUse.Interactive);

        string folderpath = "C:\\prog\\CheckDyslexia\\saves";
        string user_name { get; set; }

        public void EventLoop()
        {
            while (true)
            {
                intlib.WaitAndUpdate();
            }
        }

        
        public PointTestPage(string username)
        {
            InitializeComponent();

            user_name = username;
            outFile = new StreamWriter(System.IO.Path.Combine(folderpath, user_name + "2.txt"));

            // Point p = new Point(0, 0);
            //outFile.WriteLine("Moka");
            intlib.GazePointDataEvent += evt =>
            {
                //Ez fog lefutni, ha adat érkezik Tobiitól.
                // p.X = evt.x; * SystemParameters.PrimaryScreenWidth;
                // p.Y = evt.y; * SystemParameters.PrimaryScreenHeight;
                //coodrinates.Add(p);
                outFile.WriteLine(Convert.ToInt32(evt.x * SystemParameters.PrimaryScreenWidth - 30) + " " + Convert.ToInt32(evt.y * SystemParameters.PrimaryScreenHeight - 30));
            };
        }

        public static void Circle(double x, double y, int width, int height, Canvas cv, SolidColorBrush color, double opacity)
        {

            Ellipse circle = new Ellipse()
            {
                Width = width,
                Height = height,
                Stroke = color,
                Fill = color,
                StrokeThickness = 6,
                Opacity = opacity
            };

            cv.Children.Add(circle);

            circle.SetValue(Canvas.LeftProperty, (double)x);
            circle.SetValue(Canvas.TopProperty, (double)y);

            cv.UpdateLayout();
        }

        public static void Draw(int left, Canvas cv, int time)
        {
            //Circle(left, 200, 70, 70, cv, Brushes.White);
            var timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(time) };
            timer.Start();
            timer.Tick += (MainWindow, args) =>
            {
                timer.Stop();
                Circle(left - 5, 200, 80, 80, cv, Brushes.Black, 1);
                Circle(left + 300, 200, 70, 70, cv, Brushes.White, 1);
            };
        }

        public static void DrawBackward(int left, Canvas cv, int time)
        {
            var timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(time) };
            timer.Start();
            timer.Tick += (MainWindow, args) =>
            {
                timer.Stop();
                Circle(left + 295, 200, 80, 80, cv, Brushes.Black, 1);
                Circle(left, 200, 70, 70, cv, Brushes.White, 1);
            };
        }

        private void PBStart_Click(object sender, RoutedEventArgs e)
        {
            eventLoopThread = new Thread(new ThreadStart(EventLoop));
            eventLoopThread.Start();
            PBStart.IsEnabled = false;
            int left = 120;
            int time = 2;
            Circle(left, 200, 70, 70, PointCanvas, Brushes.White, 1);
            for (int i = 0; i < 9; i++)
            {


                if (i == 4 || i == 8)
                {
                    time += 2;
                }
                else time += 1;
                if (i < 4)
                {
                    Draw(left, PointCanvas, time);
                    left += 300;
                }
                else
                {
                    left -= 300;
                    DrawBackward(left, PointCanvas, time);
                }
            }
            var timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(time) };
            timer.Start();
            timer.Tick += (MainWindow, args) =>
            {
                timer.Stop();
                eventLoopThread.Abort();
                outFile.Close();
                PBFinish.IsEnabled = true;
            };
            
        }

        private void PBFinish_Click(object sender, RoutedEventArgs e)
        {
            eventLoopThread.Abort();
            outFile.Close();
            inFile = new StreamReader(System.IO.Path.Combine(folderpath, user_name + "2.txt"));
            string line;

            while ((line = inFile.ReadLine()) != null)
            {
                var extract = line.Split(' ');
                double X = Double.Parse(extract[0]);
                double Y = Double.Parse(extract[1]);
                Circle(X, Y, 30, 30, PointCanvas, Brushes.Red, 0.3);
            }

            PointCanvas.UpdateLayout();
            PBStart.IsEnabled = true;
            PBFinish.IsEnabled = false;

            //Save canvas as image
            Rect rect = new Rect(PointCanvas.RenderSize);
            RenderTargetBitmap rtb = new RenderTargetBitmap((int)rect.Right,
              (int)rect.Bottom, 96d, 96d, System.Windows.Media.PixelFormats.Default);
            rtb.Render(PointCanvas);
            //endcode as PNG
            BitmapEncoder pngEncoder = new PngBitmapEncoder();
            pngEncoder.Frames.Add(BitmapFrame.Create(rtb));

            //save to memory stream
            System.IO.MemoryStream ms = new System.IO.MemoryStream();

            pngEncoder.Save(ms);
            ms.Close();
            System.IO.File.WriteAllBytes(System.IO.Path.Combine(folderpath, user_name + "2.png"), ms.ToArray());

            inFile.Close();
        }
    }
}
