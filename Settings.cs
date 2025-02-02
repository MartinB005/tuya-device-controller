using IWshRuntimeLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using File = System.IO.File;

namespace Room_light
{
    public partial class Settings : Form
    {
        TuyaApi tuyaApi = new TuyaApi(Properties.Settings.Default.SID);

        public Settings()
        {
            InitializeComponent();

            if (Properties.Settings.Default.SID.Length == 0)
            {
                AddToStartup();
                Login();
            }

            LoadData();
        }  
        
        private async void LoadData()
        {
            LoadAccount();
            Dictionary<int, string> groups = await tuyaApi.GetGroups();

            while (groups == null)
            {
                Login();
                Console.WriteLine(tuyaApi.SID);
                groups = await tuyaApi.GetGroups();
            }

            BindingSource bindingSource = new BindingSource();
            bindingSource.DataSource = groups;
            group_select.DataSource = bindingSource;
            group_select.DisplayMember = "Value";
            group_select.ValueMember = "Key";

            if (groups.ContainsKey(Properties.Settings.Default.Group))
            {
                group_select.SelectedValue = Properties.Settings.Default.Group;
                LoadDevices();
            }

            group_select.SelectedValueChanged += group_select_ValueChanged;

            start_at_boot.Checked = Properties.Settings.Default.StartAtBoot;
            start_at_boot.CheckedChanged += Start_at_boot_CheckedChanged;
        }

        private async void LoadDevices()
        {
            device_select.Items.Clear();
            device_select.SelectedValueChanged -= device_select_ValueChanged;

            List<Light> lights = await tuyaApi.GetDevices(Properties.Settings.Default.Group);
            device_select.Items.AddRange(lights.Select(light => light.GetName()).ToArray());

            if (Properties.Settings.Default.DefaultDevice < lights.Count)
                device_select.SelectedIndex = Properties.Settings.Default.DefaultDevice;

            device_select.SelectedValueChanged += device_select_ValueChanged;
        }

        private void LoadAccount()
        {
            string saved = Properties.Settings.Default.Username;
            username.Text = saved.Length > 0 ? saved : "Neprihlásený";
            login_btn.Text = saved.Length == 0 ? "Prihlásiť sa" : "Odhlásiť sa";
        }

        private void group_select_ValueChanged(object sender, EventArgs e)
        {
            Console.WriteLine(group_select.SelectedValue + " type " + group_select.SelectedValue.GetType());
            Properties.Settings.Default.Group = (int)group_select.SelectedValue;
            Properties.Settings.Default.Save();
            LoadDevices();
        }

        private void device_select_ValueChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.DefaultDevice = device_select.SelectedIndex;
            Properties.Settings.Default.Save();
        }

        private void Start_at_boot_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.StartAtBoot = start_at_boot.Checked;
            Properties.Settings.Default.Save();

            if (start_at_boot.Checked) AddToStartup();
            else RemoveFromStartup();
        }

        public static void AddToStartup()
        {
            string startupPath = Environment.GetFolderPath(Environment.SpecialFolder.Startup);
            string shortcutPath = Path.Combine(startupPath, "Light.lnk");

            WshShell shell = new WshShell();
            IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortcutPath);
            shortcut.TargetPath = Application.ExecutablePath;
            shortcut.Save();
        }

        public static void RemoveFromStartup()
        {
            string startupPath = Environment.GetFolderPath(Environment.SpecialFolder.Startup);
            string shortcutPath = Path.Combine(startupPath, "Light.lnk");

            if (File.Exists(shortcutPath))
            {
                File.Delete(shortcutPath);
            }
        }

        private void login_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.Username.Length > 0)
            {
                username.Text = "Neprihlásený";
                login_btn.Text = "Prihlásiť sa";
                Properties.Settings.Default.Username = "";
                Properties.Settings.Default.SID = "";
                Properties.Settings.Default.Save();
            }

            Login();
        }

        private void Login()
        {
            using (Login login = new Login())
            {
                DialogResult result = login.ShowDialog();

                if (result == DialogResult.OK)
                {
                    username.Text = Properties.Settings.Default.Username;
                    login_btn.Text = "Odhlásiť sa";
                }
            }
        }
    }
}
