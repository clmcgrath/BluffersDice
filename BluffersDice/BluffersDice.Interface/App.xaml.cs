using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace BluffersDice.Interface
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        
        public App()
        {
        
        }

        protected override void OnExit(ExitEventArgs e)
        {
            
            
            base.OnExit(e);
        }
        protected override void OnStartup(StartupEventArgs e)
        {

            this.ShutdownMode = System.Windows.ShutdownMode.OnMainWindowClose;

            MainWindow = new MainWindow();

            MainWindow.Show();

            base.OnStartup(e);
        }

    }







}
