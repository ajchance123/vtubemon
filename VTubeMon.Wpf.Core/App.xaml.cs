using Autofac;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows;
using VTubeMon.API;
using VTubeMon.Common;
using VTubeMon.Wpf.Core.IOC;
using VTubeMon.Wpf.Core.Resources.Strings;
using VTubeMon.Wpf.Core.Themes;

namespace VTubeMon.Wpf.Core
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public IContainer Container
        {
            get; set;
        }

        private const long MAX_FILE_SIZE = 52428800; //should be 50Mb if I can do math
        private string vtubeCommon = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "VTube");
        private string logFile;
        private Window mainWindow = null;

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            if(!Directory.Exists(vtubeCommon))
            {
                Directory.CreateDirectory(vtubeCommon);
            }

            logFile = Path.Combine(vtubeCommon, "Log.txt");

            LogThread = new Thread(new ThreadStart(LogLoop)) { IsBackground = true };
            LogThread.Start();

            var themeResourceDictionary = new ThemeResourceDictionary();
            this.Resources.MergedDictionaries.Add(themeResourceDictionary);

            var stringsResourceDictionary = new StringsResourceDictionary();
            this.Resources.MergedDictionaries.Add(stringsResourceDictionary);

            ContainerBuilder cb = new ContainerBuilder();

            var logger = new EventLogger();
            logger.OnLog += Logger_OnLog;
            cb.RegisterInstance(themeResourceDictionary);
            cb.RegisterInstance(stringsResourceDictionary);
            cb.RegisterInstance(logger).As<ILogger>();

            cb.RegisterModule<IOModule>();
            cb.RegisterModule<DatabaseModule>();
            cb.RegisterModule<DiscordModule>();
            cb.RegisterModule<ViewModelModule>();
            cb.RegisterModule<CoreModule>();
            cb.RegisterModule<SettingsModule>();

            Container = cb.Build();

            try
            {
                logger.Log("------ New Startup ------");
                Container.Resolve<IIOService>().SetPath(vtubeCommon);
                mainWindow = new MainWindow();
                mainWindow.ShowDialog();
            }
            catch(Exception ex)
            {
                logger.Log(ex);
                MessageBox.Show(ex.ToString(),"Fatal Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            logger.Log("------ Session End ------");
        }

        private Thread LogThread;
        private ManualResetEvent LogSignal = new ManualResetEvent(false);
        private Queue<string> LogQueue = new Queue<string>();

        private void LogLoop()
        {
            while(Application.Current != null)
            {
                LogSignal.WaitOne();
                while(LogQueue.Count > 0)
                {
                    var log = LogQueue.Dequeue();
                    File.AppendAllText(logFile, $"{log}{Environment.NewLine}");
                }
                CheckFileSize();
                LogSignal.Reset();
            }
        }

        private void CheckFileSize()
        {
            if (new FileInfo(logFile).Length > MAX_FILE_SIZE)
            {
                //implement file rename logic
            }
        }

        private void Logger_OnLog(object sender, string e)
        {
            LogQueue.Enqueue($"{DateTime.Now.ToString("G")}\t{e}");
            LogSignal.Set();
        }
    }
}
