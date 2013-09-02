using System;
using System.Windows.Forms;

namespace Hermes
{
    static class Program
    {
        static Hermes hermes = null;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ExceptionLogger.Register();
            Application.ApplicationExit += ApplicationExit;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            hermes = new Hermes();
            Application.Run();
        }

        static void ApplicationExit(object sender, EventArgs e)
        {
            if (hermes != null)
            {
                hermes.Dispose();
                hermes = null;
            }
        }
    }
}
