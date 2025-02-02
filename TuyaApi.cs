using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Immutable;
using Newtonsoft.Json;
using System.Net.Http;
using System.Security.Cryptography;
using Newtonsoft.Json.Linq;
using System.Windows.Forms;

namespace Room_light
{
    internal class TuyaApi
    {
        public static readonly string HOST_URL = "https://a1.tuyaeu.com/api.json";
        public static readonly string CLIENT_ID = "fx3fvkvusmw45d7jn8xh";
        public static readonly string TUYA_KEY = "A_armptsqyfpxa4ftvtc739ardncett3uy_cgqx3ku34mh5qdesd7fcaru3gx7tyurr";
        public static readonly string TUYA_USER_AGENT = "TY-UA=APP/Android/1.1.6/SDK/null";
        public static readonly string COUNTRY_CODE = "43";

        public static readonly string LIST_GROUPS = "tuya.m.location.list";
        public static readonly string LIST_DEVICES = "tuya.m.my.group.device.list";
        public static readonly string DEVICE_PUBLISH = "tuya.m.device.dp.publish";
        public static readonly string CREATE_TOKEN = "tuya.m.user.email.token.create";
        public static readonly string LOGIN = "tuya.m.user.email.password.login";

        private static string sid;

        private static readonly HashSet<string> KEYS_TO_SIGN = new HashSet<string>
        {
            "a", "v", "lat", "lon", "lang", "deviceId", "imei", "imsi", "appVersion", "ttid", "isH5",
            "h5Token", "os", "clientId", "postData", "time", "requestId", "n4h5", "sid", "sp", "et"
        };

        private static readonly ImmutableDictionary<string, string> API_VERSION_FOR_ACTION =
            new Dictionary<string, string>
            {
                { "tuya.m.infrared.keydata.get", "2.0" },
                { "tuya.m.device.sub.list", "1.1" }
            }.ToImmutableDictionary();


        private static readonly HttpClient client = new HttpClient();

        public TuyaApi(string sid = null)
        {
            TuyaApi.sid = sid;
        }

        public string SID
        {
            get { return sid; }
            set { sid = value; }
        }

        public async Task<bool> Login(string email, string password)
        {
            var response = await Request(CREATE_TOKEN, new {
                countryCode = COUNTRY_CODE, email
            });

            if (!(bool)response["success"]) return false;

            string publicKey = response["result"]["publicKey"].ToString();
            int exponent = (int) response["result"]["exponent"];

            var payload = new
            {
                countryCode = COUNTRY_CODE,
                email,
                ifencrypt = 1,
                options = "{\"group\": 1}",
                passwd = PasswdUtils.Encrypt(publicKey, exponent, password),
                token = response["result"]["token"]
            };

            response = await Request(LOGIN, payload);

            Console.WriteLine(response);

            if (!(bool)response["success"]) return false;

            sid = response["result"]["sid"].ToString();
            return true;
        }

        public async Task<Dictionary<int, string>> GetGroups()
        {
            Dictionary<int, string> groups = new Dictionary<int, string>();
            JObject response = await Request(LIST_GROUPS, null);

            if (!(bool)response["success"]) return null;
            JArray json_groups = (JArray) response["result"];

            foreach (JObject group in json_groups)
            {
                int groupdId = (int) group["groupId"];
                groups[groupdId] = group["name"].ToString();
            }

            return groups;
        }

        public async Task<List<Light>> GetDevices(int groupId)
        {
            List<Light> lights = new List<Light>();

            Dictionary<string, string> extraParams = new Dictionary<string, string> {
                { "gid", Convert.ToString(groupId) } 
            };


            JObject response = await Request(LIST_DEVICES, null, extraParams);

            if ((bool)response["success"])
            {
                foreach (JObject device in response["result"])
                {
                    Light light = new Light(this, device);
                    lights.Add(light);
                }

            } else Console.WriteLine($"error list devices: {response["errorMsg"]}");

            return lights;
        }

        public async Task<JObject> Request(string action, object data, Dictionary<string, string> extraParams = null)
        {
            string api_version;
            bool v_found = API_VERSION_FOR_ACTION.TryGetValue(action, out api_version);
            Dictionary<string, string> query_params = new Dictionary<string, string>
            {
                {"a", action },
                {"clientId", CLIENT_ID},
                {"os", "Android"},
                {"et", "0.0.1"},
                {"v", v_found ? api_version : "1.0"},
                {"sid", sid}
            };

            if (extraParams != null)
            {
                foreach(KeyValuePair<string, string> pair in extraParams) query_params[pair.Key] = pair.Value;
            }


            string queryString = string.Join("&", query_params.Select(kv => $"{kv.Key}={kv.Value}"));
            string postData = data != null ? JsonConvert.SerializeObject(data) : null;
            if (postData != null) query_params["postData"] = postData;

            var formData = new Dictionary<string, string>
            {
                {"postData", postData }
            };

            var content = new FormUrlEncodedContent(formData);

            queryString += "&sign=" + Sign(query_params);


            Console.WriteLine(HOST_URL + "?" + queryString);
            var response = await client.PostAsync(HOST_URL + "?" + queryString, content);

            var responseString = await response.Content.ReadAsStringAsync();
            return JObject.Parse(responseString);
        }

        private string Sign(Dictionary<string, string> data)
        {
            StringBuilder strToSign = new StringBuilder();
           
            SignDict(strToSign, data);

            using (var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(TUYA_KEY)))
            {
                var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(strToSign.ToString()));
                return BitConverter.ToString(hash).Replace("-", "").ToLower();
            }
        }

        private void SignDict(StringBuilder strToSign, Dictionary<string, string> dict)
        {
            var sortedKeys = dict.Keys.OrderBy(k => k).ToList();

            foreach (var key in sortedKeys)
            {
                if (!KEYS_TO_SIGN.Contains(key) || !dict.ContainsKey(key) || string.IsNullOrEmpty(dict[key]))
                    continue;

                if (key == "postData")
                {
                    if (strToSign.Length > 0) strToSign.Append("||");
                    strToSign.Append(key).Append("=").Append(PasswdUtils.MobileHash(dict[key]));
                }
                else
                {
                    if (strToSign.Length > 0) strToSign.Append("||");
                    strToSign.Append(key).Append("=").Append(dict[key]);
                }
            }
        }
    }
}
