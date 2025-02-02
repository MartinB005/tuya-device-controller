using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Room_light
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private async void login_btn_Click(object sender, EventArgs e)
        {
            TuyaApi tuyaApi = new TuyaApi();
            bool success = await tuyaApi.Login(email.Text, password.Text);

            if (success)
            {
                Properties.Settings.Default.SID = tuyaApi.SID;
                Properties.Settings.Default.Username = email.Text;
                Properties.Settings.Default.Save();
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show("Email or password is invalid !");
            }
        }
    }
}
