using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using tool_turnbased_ai;
using tool_turnbased_ai.Controller;
using System.Windows;
using System.Windows.Controls;

namespace tool_turnbased_ai
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        ApplicationController applicationController;
        public Window window;

        public App()  {       }

        void App_Startup(object sender, StartupEventArgs e)
        {            
            this.window                = new MainWindow();            
            this.applicationController = new ApplicationController(this);
            window.Show();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);            
        }
    }
}
