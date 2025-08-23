using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ePQTiktokLive.TIKTOK
{


    public class TikTokWebSocketHelper
    {
        private static async Task<string> GetRoomIdFromCheckAlive(string username)
        {
             var client = new HttpClient();

            // Fake headers như TikTok App
            client.DefaultRequestHeaders.Add("User-Agent", "com.zhiliaoapp.musically/2022103040 (Linux; Android 11)");
            client.DefaultRequestHeaders.Add("Referer", "https://www.tiktok.com/");

            // Gửi đến API "check_alive" với username
            var url = $"https://www.tiktok.com/api/live/check_alive/?username={username}&aid=1988";
            var response = await client.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();

            var match = Regex.Match(content, @"""room_id_str"":\s*""(\d+)""");
            return match.Success ? match.Groups[1].Value : null;
        }
        private static async Task<string> GetWebSocketUrlFromRoomId(string roomId)
        {
             var client = new HttpClient();

            client.DefaultRequestHeaders.Add("User-Agent", "com.zhiliaoapp.musically/2022103040 (Linux; Android 11)");
            client.DefaultRequestHeaders.Add("Referer", "https://www.tiktok.com/");

            var url = $"https://webcast.tiktok.com/webcast/room/info/?room_id={roomId}&aid=1988";

            var response = await client.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();

            var match = Regex.Match(content, @"""websocket_url"":\s*""(.*?)""");
            return match.Success ? WebUtility.HtmlDecode(match.Groups[1].Value) : null;
        }
        public static async Task<string> GetWebSocketFromUsername(string username)
        {
            string roomId = await GetRoomIdFromCheckAlive(username);
            if (string.IsNullOrEmpty(roomId))
            {
                Console.WriteLine("❌ Không lấy được room_id từ check_alive");
                return null;
            }

            Console.WriteLine($"✅ Lấy được room_id: {roomId}");

            string wsUrl = await GetWebSocketUrlFromRoomId(roomId);
            if (wsUrl == null)
            {
                Console.WriteLine("❌ Không lấy được websocket_url");
            }
            else
            {
                Console.WriteLine($"✅ WebSocket URL: {wsUrl}");
            }

            return wsUrl;
        }
        //public static async Task<string> GetWebSocketUrlAsync(string userUniqueId, string sessionId, string ttTargetIdc, int maxRetry = 3)
        //{
        //    string baseUrl = "https://webcast.tiktok.com/webcast/room/info/";
        //    string query = $"aid=1988&user_unique_id={userUniqueId}&device_platform=web&sessionid={sessionId}&tt-target-idc={ttTargetIdc}";
        //    string finalUrl = $"{baseUrl}?{query}";

        //     var client = new HttpClient();
        //    client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0");
        //    client.DefaultRequestHeaders.Add("Cookie", $"sessionid={sessionId}; tt_target_idc={ttTargetIdc}");
        //    client.DefaultRequestHeaders.Add("Referer", "https://www.tiktok.com/");

        //    for (int attempt = 1; attempt <= maxRetry; attempt++)
        //    {
        //        try
        //        {
        //            var response = await client.GetAsync(finalUrl);
        //            var json = await response.Content.ReadAsStringAsync();
        //            JObject data = JObject.Parse(json);
        //            string wsUrl = data["data"]?["websocket_url"]?.ToString();
        //            if (!string.IsNullOrEmpty(wsUrl)) return wsUrl;
        //        }
        //        catch { }
        //        await Task.Delay(1000 * attempt);
        //    }
        //    return null;
        //}
        public static string ExtractUserUniqueId(string tiktokUrl)
        {
            try
            {
                var uri = new Uri(tiktokUrl);
                string[] parts = uri.AbsolutePath.Split('/');

                for (int i = 0; i < parts.Length; i++)
                {
                    if (parts[i].Length > 0)
                    {
                        if (parts[i].Substring(0, 1) == "@")
                        {
                            return parts[i].Substring(1, parts[i].Length - 1);
                        }
                    }
                }
                
                return null;
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.ToString());
                return null;
            }
        }
    }

}
