using System;
using System.Windows;
using System.IO;
using System.Threading;

namespace CheckDyslexia
{
    /// Interaction logic for App.xaml
    public partial class App : Application
    {
        
        protected StreamWriter outFile;
        protected Tobii.InteractionLib.IInteractionLib intlib = Tobii.InteractionLib.InteractionLibFactory.CreateInteractionLib(Tobii.InteractionLib.FieldOfUse.Interactive);


        private void eventLoop()
        {
            const int max_cycles = 200;
            var cycle = 0;
            while (cycle++ < max_cycles)
            {
                intlib.WaitAndUpdate();
            }
        }
        protected override void OnStartup(StartupEventArgs e)
        {
           outFile = new StreamWriter("test.txt");
            outFile.WriteLine("Mukszik");
            intlib.GazePointDataEvent += evt =>
            {
               outFile.WriteLine(
                    "x: " + evt.x
                    + ", y: " + evt.y
                    + ", validity: " + (evt.validity == Tobii.InteractionLib.Validity.Valid ? "valid" : "invalid")
                    + ", timestamp: " + evt.timestamp_us + " us"
                );
            };
            Thread tr = new Thread(new ThreadStart(eventLoop));
            tr.Start();
            base.OnStartup(e);
        }


        protected override void OnExit(ExitEventArgs e)
        {
            // Cleans up the Interaction Library WPF host instance before the application exits.
            //IntlibWpf?.Dispose();
            outFile?.Close();
            intlib?.Dispose();
            base.OnExit(e);
        }
    }
}
