using Guna.UI2.WinForms;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;

namespace Room_light
{
    public partial class MainWindow : Form
    {

        [DllImport("user32.dll")]
        private static extern bool AnimateWindow(IntPtr hWnd, int time, int flags);

        private const int AW_SLIDE = 0x00040000;
        private const int AW_VER_NEGATIVE = 0x00000008;
        private const int AW_BLEND = 0x00080000;

        public static readonly int GROUP_ID = 68956111;

        private List<Light> lights;

        public MainWindow()
        {
            Init();
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            var screenBounds = Screen.PrimaryScreen.WorkingArea;

            int xPosition = screenBounds.Width - Width - 10;
            int yPosition = screenBounds.Height - Height - 10;

            Location = new Point(xPosition, yPosition);

            AnimateWindow(this.Handle, 150, AW_SLIDE | AW_VER_NEGATIVE);
        }

        private async void Init()
        {

            TuyaApi api = new TuyaApi(Properties.Settings.Default.SID);
            lights = await api.GetDevices(Properties.Settings.Default.Group);

            if (lights.Count > 0)
            {
                light_select.Items.AddRange(lights.Select(light => light.GetName()).ToArray());
                light_select.SelectedIndex = Properties.Settings.Default.DefaultDevice;
            }
            else
            {
                Settings settings = new Settings();
                settings.Show();
                settings.Activate();
            }

        }

        private void UpdateData()
        {
            power.CheckedChanged -= power_CheckedChanged;
            bright_slider.ValueChanged -= slider_ValueChanged;
            temp_slider.ValueChanged -= temp_slider_ValueChanged;

            int selected = light_select.SelectedIndex;
            power.Checked = lights[selected].PowerOn;
            bright_slider.Value = lights[selected].BrightValue;
            temp_slider.Value = lights[selected].Temperature;

            power.Enabled = lights[selected].IsOnline();
            bright_slider.Enabled = lights[selected].IsOnline();
            temp_slider.Enabled = lights[selected].IsOnline();

            power.CheckedChanged += power_CheckedChanged;
            bright_slider.ValueChanged += slider_ValueChanged;
            temp_slider.ValueChanged += temp_slider_ValueChanged;
        }


        protected override void OnDeactivate(EventArgs e)
        {
            Close();
        }

        private void power_Click(object sender, EventArgs e)
        {
            power.Checked = !power.Checked;
        }

        private void power_CheckedChanged(object sender, EventArgs e)
        {
            lights[light_select.SelectedIndex].PowerOn = power.Checked;
        }

        private void slider_ValueChanged(object sender, EventArgs e)
        {
            lights[light_select.SelectedIndex].BrightValue = bright_slider.Value;
        }

        private void light_select_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateData();
        }

        private void temp_slider_ValueChanged(object sender, EventArgs e)
        {
            lights[light_select.SelectedIndex].Temperature = temp_slider.Value;
        }

        private void temp_slider_DragLeave(object sender, EventArgs e)
        {
            lights[light_select.SelectedIndex].UpdateDps();
        }

        private void bright_slider_MouseUp(object sender, MouseEventArgs e)
        {
            lights[light_select.SelectedIndex].UpdateDps();
        }

        private void temp_slider_MouseUp(object sender, MouseEventArgs e)
        {
            lights[light_select.SelectedIndex].UpdateDps();
        }

        private void settings_Click(object sender, EventArgs e)
        {
            Settings settings = new Settings();
            settings.Show();
            settings.Activate();
        }
    }
}
