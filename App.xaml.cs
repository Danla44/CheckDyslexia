using System;
using System.Windows;
using System.IO;
using System.Threading;

namespace CheckDyslexia
{
    /// Interaction logic for App.xaml
    public partial class App : Application
    {
        
        //protected StreamWriter outFile;
        //protected Tobii.InteractionLib.IInteractionLib intlib = Tobii.InteractionLib.InteractionLibFactory.CreateInteractionLib(Tobii.InteractionLib.FieldOfUse.Interactive);
        //protected Thread eventLoopThread;

        /*public void eventLoop()
        {
            while (true)
            {
                intlib.WaitAndUpdate();
            }
        }*/
        protected override void OnStartup(StartupEventArgs e)
        {
          /* outFile = new StreamWriter("test.txt");
            outFile.WriteLine("Mukszik");
            intlib.GazePointDataEvent += evt =>
            {
                //Ez fog lefutni, ha adat érkezik Tobiitól.
               outFile.WriteLine(
                    "x: " + evt.x
                    + ", y: " + evt.y
                    + ", validity: " + (evt.validity == Tobii.InteractionLib.Validity.Valid ? "valid" : "invalid")
                    + ", timestamp: " + evt.timestamp_us + " us"
                );
            };
            //Szál indítása, ami figyel a beérkező adatokra (szál azért kell, hogy miközben rögzít, ne fagyjon le a UI)
            //eventLoopThread = new Thread(new ThreadStart(eventLoop));
            //eventLoopThread.Start();

            // később megállítani:
            //eventLoopThread.Abort();*/

            base.OnStartup(e);
        }


        protected override void OnExit(ExitEventArgs e)
        {
            // Cleans up the Interaction Library WPF host instance before the application exits.
            //IntlibWpf?.Dispose();
            
            
            // Tobii adatok figyelésének megállítása:
            //eventLoopThread.Abort();


            //outFile?.Close();
            //intlib?.Dispose();
            base.OnExit(e);
        }
    }
}
