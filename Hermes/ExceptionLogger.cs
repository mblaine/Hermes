using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace Hermes
{
    public class ExceptionLogger
    {
        public static EventHandler BeforeExceptionLog;

        public static EventHandler AfterExceptionLog;

        public static void Register()
        {
            AppDomain.CurrentDomain.UnhandledException += UnhandledException;
            Application.ThreadException += ThreadException;
        }

        private static void UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            if (e.ExceptionObject is Exception)
                LogException((Exception)e.ExceptionObject);
        }

        private static void ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            LogException(e.Exception);
        }

        private static void LogException(Exception ex)
        {
            try
            {
                if (BeforeExceptionLog != null)
                    BeforeExceptionLog(null, null);
            }
            catch (Exception)
            {
            }

            try
            {
                DateTime dt = DateTime.Now;
                String assembly = Assembly.GetExecutingAssembly().Location;
                String path = String.Format("{0}{1}exception-{2:yyyy-MM-dd-HH-mm-ss}.txt", Path.GetDirectoryName(assembly), Path.DirectorySeparatorChar, dt);
                FileVersionInfo info = FileVersionInfo.GetVersionInfo(assembly);
                using (StreamWriter sw = new StreamWriter(path, true))
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendFormat("{0} version {1}, modified {2:M/d/yyy h:mm:ss tt}{3}", info.ProductName, info.FileVersion, File.GetLastWriteTime(assembly), Environment.NewLine);
                    sb.AppendFormat("Time: {0:M/d/yyy h:mm:ss tt}{1}{1}", dt, Environment.NewLine);

                    Regex pattern = new Regex(@"^(.* in )(.*)(\:line.*)$");
                    String[] lines = ex.ToString().Split('\r', '\n');
                    foreach (String line in lines)
                    {
                        if (line.Length > 0)
                        {
                            //remove full paths from trace
                            Match m = pattern.Match(line);
                            if (m.Success)
                                sb.AppendFormat("{0}{1}{2}{3}", m.Groups[1].Value, Path.GetFileName(m.Groups[2].Value), m.Groups[3].Value, Environment.NewLine);
                            else
                                sb.AppendLine(line);
                        }
                    }

                    sw.Write(sb.ToString());
                    sw.Close();
                }

                MessageBox.Show(null, String.Format("{0} has encountered an unexpected error. The log has been saved to \"{1}\".", info.ProductName, path), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception e)
            {
                MessageBox.Show("Error saving error log: " + e.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            try
            {
                if (AfterExceptionLog != null)
                    AfterExceptionLog(null, null);
            }
            catch (Exception)
            {
            }
        }
    }
}
