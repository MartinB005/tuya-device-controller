using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using IWshRuntimeLibrary;
using Microsoft.Win32;
using File = System.IO.File;

namespace Room_light
{
    internal static class Program
    {

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            NotifyIcon notifyIcon = new NotifyIcon();
            notifyIcon.Visible = true;
            notifyIcon.Icon = new Icon(LoadResource("Room_light.bulb-active.ico"));
            notifyIcon.Click += NotifyIcon_Click;
            Application.Run();
        }

        public static Stream LoadResource(string path)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            return assembly.GetManifestResourceStream(path);
        }

        private static void NotifyIcon_Click(object sender, EventArgs e)
        {
            MainWindow window = new MainWindow();

            window.Show();
            window.Activate();
        }
    }
}
