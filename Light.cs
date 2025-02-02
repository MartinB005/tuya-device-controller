using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Room_light
{
    internal class Light
    {
        public static readonly int POWER = 20;
        public static readonly int BRIGHT_VAL = 22;
        public static readonly int TEMP_VAL = 23;

        public static readonly int INTERVAL = 100;

        private readonly Dictionary<int, object> dps = new Dictionary<int, object>();
        private readonly string id;
        private readonly string name;
        private readonly TuyaApi api;
        private readonly Stopwatch stopwatch = new Stopwatch();

        private bool online;

        public Light(TuyaApi tuyaApi, JObject jobject)
        {
            api = tuyaApi;
            id = jobject["devId"].ToString();
            name = jobject["name"].ToString();
            online = (bool) jobject["moduleMap"]["mcu"]["isOnline"];
            UpdateDps((JObject)jobject["dps"]);
        }

        public void UpdateDps(JObject jObject)
        {
            int[] keys = { POWER, BRIGHT_VAL, TEMP_VAL };

            foreach (int key in keys)
            {
                dps[key] = jObject.GetValue(key.ToString());
            }
        }

        public bool PowerOn
        {
            get
            {
                return Convert.ToBoolean(dps[POWER]);
            }
            set
            {
                dps[POWER] = value;
                SetDps(POWER, value);
            }
        }

        public int BrightValue
        {
            get
            {
                int raw = Convert.ToInt32(dps[BRIGHT_VAL]);
                return (int) Math.Round((raw - 10) / 990.0 * 100);
            }
            set
            {
                int new_val = (int)Math.Round(value / 100.0 * 990 + 10);
                dps[BRIGHT_VAL] = new_val;
                SetDps(BRIGHT_VAL, new_val);
            }
        }

        public bool IsOnline()
        {
            return online;
        }

        public string GetName()
        {
            return name;
        }

        public int Temperature
        {
            get
            {
                return (int) Math.Round(Convert.ToInt32(dps[TEMP_VAL]) / 10f);
            }
            set
            {
                dps[TEMP_VAL] = value * 10;
                SetDps(TEMP_VAL, value * 10);
            }
        }

        public async void SetDps(int dps, object value)
        {
            if (!stopwatch.IsRunning || stopwatch.ElapsedMilliseconds > INTERVAL)
            {
                stopwatch.Restart();
                var payload = new
                {
                    devId = id,
                    gwId = id,
                    dps = $"{{{dps}: {Convert.ToString(value).ToLower()}}}"
                };

                var response = await api.Request("tuya.m.device.dp.publish", payload);
                if (!(bool)response["success"]) ShowNotification();
            }
        }

        public void ShowNotification()
        {
            var notifyIcon = new NotifyIcon
            {
                Icon = SystemIcons.Application,
                Visible = true,
                Text = "Room Light"
            };

            notifyIcon.ShowBalloonTip(3000, name, "Zariadenie je offline.", ToolTipIcon.Info);

        }

        public async void UpdateDps()
        {
            var payload = new
            {
                devId = id,
                gwId = id,
                dps = $"{{{BRIGHT_VAL}: {dps[BRIGHT_VAL]}, {TEMP_VAL}: {dps[TEMP_VAL]}}}"
            };

            await api.Request("tuya.m.device.dp.publish", payload);
        }
    }
}
