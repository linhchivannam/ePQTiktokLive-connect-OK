using ePQTiktokLive.TIKTOK;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using Google.Protobuf;
using TikTok;
using WebSocketSharp;
using System.IO.Compression;

namespace ePQTiktokLive.LIVE
{
    public partial class frm_TTLiveConnect : Form
    {
        public frm_TTLiveConnect()
        {
            InitializeComponent();
        }

        private static System.Threading.Timer _timer;

        private void pTiktok_Click(object sender, EventArgs e)
        {

        }

        private async void btnConnect_Click(object sender, EventArgs e)
        {
            string liveUrl = "https://www.tiktok.com/@"+txtTiktoklive.Text.Trim().ToLower()+"/live";

            // B1: Nếu chưa có cookies.json thì mở Puppeteer login và lưu session
            if (!File.Exists("cookies.json"))
            {
                var roomInfo = await TikTokSessionSaver.SaveSessionAsync(liveUrl);
                if (roomInfo != null)
                {
                    if (roomInfo.is_live)
                    {
                        btnConnect.Text = "Stop";
                        btnConnect.BackColor = Color.Green;
                        lbTiktokUser.Text = roomInfo.host_name;
                        TikTokWebsocket(roomInfo);
                    }
                    else
                    {

                    }
                }
            }
            else
            {
                var roomInfo =  TikTokSessionSaver.ReadRoomInfo("roominfo.json");
                if (roomInfo != null)
                {
                    if (roomInfo.is_live)
                    {
                        btnConnect.Text = "Stop";
                        btnConnect.BackColor = Color.Green;
                        lbTiktokUser.Text = roomInfo.host_name;
                        TikTokWebsocket(roomInfo);
                    }
                    else
                    {

                    }
                }
            }
            
        }
        private async void TikTokWebsocket(TikTokRoomInfo myroom)
        {
            string roomId = myroom.room_id;
            string wsUrl = TikTokWebSocketUrlBuilder.BuildUrl(roomId);
            string cookieHeader = TikTokWebSocketUrlBuilder.GetCookieHeader();

            using (var ws = new WebSocket(wsUrl))
            {
                ws.CustomHeaders = new Dictionary<string, string>
                {
                    ["User-Agent"] = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) " +
                                      "AppleWebKit/537.36 (KHTML, like Gecko) " +
                                      "Chrome/138.0.0.0 Safari/537.36",
                    ["Origin"] = "https://www.tiktok.com",
                    ["Cookie"] = cookieHeader,
                    ["Pragma"] = "no-cache",
                    ["Cache-Control"] = "no-cache",
                    ["Accept-Encoding"] = "gzip, deflate, br, zstd",
                    ["Accept-Language"] = "en-US,en;q=0.9"
                };

                ws.SslConfiguration.EnabledSslProtocols = System.Security.Authentication.SslProtocols.Tls12;

                ws.OnOpen += async (s, e) =>
                {
                    Console.WriteLine("✅ Connected to TikTok WebSocket!");
                    await Task.Run(async () =>
                    {
                        while (ws.IsAlive)
                        {
                            try
                            {
                                // Tạo heartbeat protobuf
                                var pingMsg = new WebcastWebsocketMessage { Type = "ping", Payload = ByteString.Empty };
                                var pushFrame = new WebcastPushFrame
                                {
                                    PayloadType = "hb",
                                    Payload = ByteString.CopyFrom(pingMsg.ToByteArray()),
                                    LogId = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()
                                };

                                // Serialize + gzip
                                byte[] frameBytes = pushFrame.ToByteArray().GzipCompress();

                                ws.Send(frameBytes);
                                await Task.Delay(10000); // heartbeat_duration = 10s
                            }
                            catch { break; }
                        }
                    });
                };

                ws.OnMessage += (s, e) =>
                {
                    // Giải nén gzip nếu compress
                    byte[] data = e.RawData.GzipDecompress();
                    // TODO: decode protobuf WebcastPushFrame
                    Console.WriteLine($"📩 Message received ({data.Length} bytes)");
                };

                ws.OnError += (s, e) => Console.WriteLine($"❌ Error: {e.Message}");
                ws.OnClose += (s, e) => Console.WriteLine($"🔌 Closed: {e.Reason}");

                ws.Connect();
            }
        }

        // Extension method nén/gải nén gzip
        

        public static void StopHeartbeat()
        {
            _timer?.Dispose();
            Console.WriteLine("💔 Heartbeat stopped");
        }
        
    }
    public static class GzipExtensions
    {
        public static byte[] GzipCompress(this byte[] data)
        {
            using (var ms = new MemoryStream())
            using (var gzip = new GZipStream(ms, CompressionMode.Compress))
            {
                gzip.Write(data, 0, data.Length);
                gzip.Close();
                return ms.ToArray();
            }
        }

        public static byte[] GzipDecompress(this byte[] data)
        {
            using (var ms = new MemoryStream(data))
            using (var gzip = new GZipStream(ms, CompressionMode.Decompress))
            using (var outMs = new MemoryStream())
            {
                gzip.CopyTo(outMs);
                return outMs.ToArray();
            }
        }
    }
}
