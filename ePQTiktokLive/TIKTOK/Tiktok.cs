
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Web;


namespace ePQTiktokLive.TIKTOK
{
    internal class Tiktok
    {
    }
    // Định nghĩa các lớp để deserialize JSON (giống như ví dụ trước)
    public class TikTokLiveInfo
    {
        public bool alive { get; set; }
        public long room_id { get; set; }
        public string room_id_str { get; set; }
    }

    public class TikTokApiResponse
    {
        public List<TikTokLiveInfo> data { get; set; }
        public object extra { get; set; }
        public int status_code { get; set; }
    }
    public class TikTokRoomInfo
    {
        public string room_id { get; set; }
        public string host_id { get; set; }
        public string unique_id { get; set; }
        public string host_name { get; set; }
        public string host_avatar { get; set; }
        public string signature { get; set; }
        public bool is_live { get; set; }
    }
    class TikTokWebSocketUrlBuilder
    {
        private const string CookieFile = "cookies.json";
        private const string LocalStorageFile = "localstorage.json";
        private const string SessionStorageFile = "sessionstorage.json";
        public static string GetCookieHeader()
        {
            var json = File.ReadAllText(CookieFile);
            var cookies = JsonSerializer.Deserialize<List<Dictionary<string, object>>>(json);

            // lấy cookie theo key, cái cuối cùng ghi đè cái trước
            var dict = new Dictionary<string, string>();
            foreach (var c in cookies)
            {
                string name = c["Name"].ToString();
                string value = c["Value"].ToString();
                dict[name] = value;
            }

            return string.Join("; ", dict.Select(kv => $"{kv.Key}={kv.Value}"));
        }
        

        public static Dictionary<string, object> GetLocalStorage()
        {
            if (!File.Exists(LocalStorageFile)) return new Dictionary<string, object>();
            var json = File.ReadAllText(LocalStorageFile);
            return JsonSerializer.Deserialize<Dictionary<string, object>>(json);
        }

        public static Dictionary<string, object> GetSessionStorage()
        {
            if (!File.Exists(SessionStorageFile)) return new Dictionary<string, object>();
            var json = File.ReadAllText(SessionStorageFile);
            return JsonSerializer.Deserialize<Dictionary<string, object>>(json);
        }
        public static string GetVerifyFp()
        {
            if (!File.Exists(CookieFile))
                throw new FileNotFoundException("❌ Không tìm thấy cookies.json, hãy login trước.");

            string json = File.ReadAllText(CookieFile);
            var cookies = JsonSerializer.Deserialize<List<Dictionary<string, object>>>(json);

            if (cookies == null) return null;

            var fpCookie = cookies.FirstOrDefault(c =>
                c.ContainsKey("Name") && c["Name"].ToString() == "s_v_web_id");

            var ttwid = cookies.FirstOrDefault(c =>
                c.ContainsKey("Name") && c["Name"].ToString() == "ttwid");

            if (fpCookie != null && fpCookie.ContainsKey("Value"))
            {
                string verifyFp = fpCookie["Value"].ToString();
                Console.WriteLine("✅ verifyFp = " + verifyFp);
                return verifyFp;
            }

            Console.WriteLine("⚠️ Không tìm thấy cookie s_v_web_id (verifyFp).");
            return "";
        }
        public static string TimCookies(string name)
        {
            if (!File.Exists(CookieFile))
                throw new FileNotFoundException("❌ Không tìm thấy cookies.json, hãy login trước.");

            string json = File.ReadAllText(CookieFile);
            var cookies = JsonSerializer.Deserialize<List<Dictionary<string, object>>>(json);

            if (cookies == null) return null;

            var fpCookie = cookies.FirstOrDefault(c =>
                c.ContainsKey("Name") && c["Name"].ToString() == name);

          

            if (fpCookie != null && fpCookie.ContainsKey("Value"))
            {
                string verifyFp = fpCookie["Value"].ToString();
              //  Console.WriteLine("✅ verifyFp = " + verifyFp);
                return verifyFp;
            }

            Console.WriteLine("⚠️ Không tìm thấy cookie s_v_web_id (verifyFp).");
            return "";
        }
        public static string BuildUrl(string roomId)
        {
            // Đọc LocalStorage & SessionStorage đã lưu
            var localStore = GetLocalStorage();
            var sessionStore = GetSessionStorage();
            
           

            string msToken = localStore.ContainsKey("msToken") ? localStore["msToken"].ToString() : "";
            string s_v_web_id = GetVerifyFp();
            string verifyFp = sessionStore.ContainsKey("verifyFp") ? sessionStore["verifyFp"].ToString() : "";
            if(verifyFp=="")
                verifyFp = s_v_web_id+verifyFp;

            // Build query params cơ bản
            var query = HttpUtility.ParseQueryString(string.Empty);
            query["version_code"] = "180800";
            query["device_platform"] = "web";
            query["cookie_enabled"] = "true";
            query["screen_width"] = "1366";
            query["screen_height"] = "768";
            query["browser_language"] = "en-US";
            query["browser_platform"] = "Win32";
            query["browser_name"] = "Mozilla";
            query["browser_version"] = "5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/138.0.0.0 Safari/537.36";
            query["browser_online"] = "true";
            query["tz_name"] = "Asia/Bangkok";
            query["app_name"] = "tiktok_web";
            query["sup_ws_ds_opt"] = "1";
            query["version_code"] = "270000";
            query["update_version_code"] = "2.0.0";
            query["compress"] = "gzip";
            query["webcast_language"] = "en";
            query["ws_direct"] = "1";
            query["aid"] = "1988";
            query["live_id"] = "12";
            query["app_language"] = "en";
            query["client_enter"] = "1";
            query["room_id"] = roomId;
            query["identity"] = "audience";
            query["history_comment_count"] = "6";
            query["last_rtt"] = "0";
            query["heartbeat_duration"] = "10000";
            query["resp_content_type"] = "protobuf";
            query["did_rule"] = "3";

            // Bổ sung msToken, verifyFp, s_v_web_id nếu có
            if (!string.IsNullOrEmpty(msToken))
                query["msToken"] = msToken;

            query["ttwid"] = TimCookies("ttwid");

            if (!string.IsNullOrEmpty(verifyFp))
                query["verifyFp"] = verifyFp;
            //if (!string.IsNullOrEmpty(s_v_web_id))
            //    query["s_v_web_id"] = s_v_web_id;

            // Ghép URL
            string url = "wss://webcast-ws.tiktok.com/webcast/im/ws_proxy/ws_reuse_supplement/?" + query.ToString();
            Console.WriteLine("wUrl = " + url);
            return url;
        }
    }
}
