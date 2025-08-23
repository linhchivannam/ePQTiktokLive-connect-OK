using PuppeteerSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ePQTiktokLive.TIKTOK
{
    internal class TikTokSessionSaver
    {
        public static async Task<TikTokRoomInfo> SaveSessionAsync(string liveUrl)
        {
            await new BrowserFetcher().DownloadAsync();

            var launchOptions = new LaunchOptions
            {
                Headless = false, // mở cửa sổ để user login
                DefaultViewport = null,
                Args = new[] { "--start-maximized", "--disable-blink-features=AutomationControlled" }               
            };

             var browser = await Puppeteer.LaunchAsync(launchOptions);
             var page = await browser.NewPageAsync();

            Console.WriteLine("👉 Đang mở TikTok, vui lòng đăng nhập...");

            await page.GoToAsync(liveUrl);


            while (true)
            {
                var cookies = await page.GetCookiesAsync();
                if (cookies.Any(c => c.Name == "sessionid"))
                {
                    Console.WriteLine("✅ Đăng nhập thành công!");
                    break;
                }

                await Task.Delay(1000); // kiểm tra lại mỗi giây
            }



            // lấy thông tin từ SIGI_STATE
            var jsonRoom = await page.EvaluateFunctionAsync<string>(
     @"() => {
    let el = document.querySelector('script[id=""SIGI_STATE""]');
    if (!el) return {};

    let data = JSON.parse(el.textContent);

    // toàn bộ thông tin LiveRoom
    let liveRoom = data?.LiveRoom || {};
    let user = liveRoom?.liveRoomUserInfo?.user || {};

    // roomId nằm ở LiveRoom, không phải user
    let roomId = user?.roomId || '';

    let hostUid = user?.id || '';
    let uniqueId = user?.uniqueId || '';
    let hostName = user?.nickname || '';
    let hostAvatar = user?.avatarLarger || user?.avatarThumb || user?.avatarMedium || '';
    let signature = user?.signature || '';   
    let status = user?.status === 2; // 2 = live, 0 = offline

    
    let result = {
        room_id: roomId,
        host_id: user?.id || '',
        unique_id: user?.uniqueId || '',
        host_name: user?.nickname || '',
        host_avatar: user?.avatarLarger || user?.avatarThumb || user?.avatarMedium || '',
        signature: user?.signature || '',
        is_live: (user?.status === 2)
    };

    return JSON.stringify(result);

}"
);

            
            var roomInfo = JsonSerializer.Deserialize<TikTokRoomInfo>(jsonRoom);

            

            File.WriteAllText("roominfo.json", JsonSerializer.Serialize(roomInfo, new JsonSerializerOptions { WriteIndented = true }));
            Console.WriteLine("💾 roominfo.json saved.");



            // save cookies
            var cookiesAll = await page.GetCookiesAsync();
            var cookiesJson = JsonSerializer.Serialize(cookiesAll, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText("cookies.json", cookiesJson);
            Console.WriteLine("💾 cookies.json saved.");

            // save localStorage
            var localStorage = await page.EvaluateFunctionAsync<Dictionary<string, object>>(
    @"() => { 
        let store = {}; 
        for (let i=0; i<localStorage.length; i++) { 
            let key = localStorage.key(i); 
            store[key] = localStorage.getItem(key); 
        } 
        return store; 
    }"
);
            File.WriteAllText("localstorage.json", JsonSerializer.Serialize(localStorage, new JsonSerializerOptions { WriteIndented = true }));
            Console.WriteLine("💾 localstorage.json saved.");

            // save sessionStorage
            var sessionStorage = await page.EvaluateFunctionAsync<Dictionary<string, object>>(
    @"() => { 
        let store = {}; 
        for (let i=0; i<sessionStorage.length; i++) { 
            let key = sessionStorage.key(i); 
            store[key] = sessionStorage.getItem(key); 
        } 
        return store; 
    }"
);
            File.WriteAllText("sessionstorage.json", JsonSerializer.Serialize(sessionStorage, new JsonSerializerOptions { WriteIndented = true }));
            Console.WriteLine("💾 sessionstorage.json saved.");

            var wsInfo = new
            {
                room_id = roomInfo.room_id,
                msToken = sessionStorage.ContainsKey("msToken") ? sessionStorage["msToken"] : null,
                verifyFp = localStorage.ContainsKey("verifyFp") ? localStorage["verifyFp"] : null
            };
            File.WriteAllText("wsinfo.json", JsonSerializer.Serialize(wsInfo, new JsonSerializerOptions { WriteIndented = true }));
            Console.WriteLine("💾 wsinfo.json saved.");

            await browser.CloseAsync();
            Console.WriteLine("👉 Đã đóng browser. Bạn có thể dùng WebSocket ngay.");
            return roomInfo;
            
        }
        public static TikTokRoomInfo ReadRoomInfo(string filePath = "roominfo.json")
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine("❌ Không tìm thấy file roominfo.json");
                return null;
            }

            try
            {
                string json = File.ReadAllText(filePath);
                var info = JsonSerializer.Deserialize<TikTokRoomInfo>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (info == null || string.IsNullOrEmpty(info.room_id))
                {
                    Console.WriteLine("⚠️ Không lấy được room_id từ file.");
                    return null;
                }

                Console.WriteLine("✅ Đọc roominfo.json thành công!");
                Console.WriteLine($"   room_id   : {info.room_id}");
                Console.WriteLine($"   host_name : {info.host_name}");
                Console.WriteLine($"   unique_id : {info.unique_id}");
                Console.WriteLine($"   is_live   : {info.is_live}");

                return info;
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ Lỗi đọc roominfo.json: " + ex.Message);
                return null;
            }
        }
    }
}
