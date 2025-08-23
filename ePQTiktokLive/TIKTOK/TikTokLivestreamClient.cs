using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ePQTiktokLive.TIKTOK
{
    public class TikTokLivestreamClient
    {
        private readonly string roomId;
        private readonly ListBox listBox;
        private ClientWebSocket webSocket;

        public TikTokLivestreamClient(string roomId, ListBox listBoxComments)
        {
            this.roomId = roomId;
            this.listBox = listBoxComments;
        }

        public async Task StartAsync()
        {
            webSocket = new ClientWebSocket();
            string wsUrl = $"wss://webcast.tiktok.com/ws/webcast/im/fetch/?room_id={roomId}&aid=1988";

            try
            {
                await webSocket.ConnectAsync(new Uri(wsUrl), CancellationToken.None);
                Log($"✅ Connected to room {roomId}");

                _ = Task.Run(() => ReceiveLoopAsync());
            }
            catch (Exception ex)
            {
                Log($"❌ WebSocket error: {ex.Message}");
            }
        }

        private async Task ReceiveLoopAsync()
        {
            var buffer = new byte[8192];

            while (webSocket.State == WebSocketState.Open)
            {
                try
                {
                    var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

                    if (result.MessageType == WebSocketMessageType.Close)
                    {
                        await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", CancellationToken.None);
                        Log("🔌 Connection closed by server.");
                        break;
                    }

                    string message = Encoding.UTF8.GetString(buffer, 0, result.Count);
                    ProcessMessage(message);
                }
                catch (Exception ex)
                {
                    Log($"⚠️ Receive error: {ex.Message}");
                    break;
                }
            }
        }

        private void ProcessMessage(string json)
        {
            if (json.Contains("ChatMessage"))
            {
                string message = ExtractTextBetween(json, "\"content\":\"", "\"");
                string user = ExtractTextBetween(json, "\"nickname\":\"", "\"");

                if (!string.IsNullOrEmpty(message) && !string.IsNullOrEmpty(user))
                    Log($"💬 {user}: {message}");
            }
        }

        private string ExtractTextBetween(string input, string start, string end)
        {
            int startIndex = input.IndexOf(start);
            if (startIndex == -1) return null;
            startIndex += start.Length;

            int endIndex = input.IndexOf(end, startIndex);
            if (endIndex == -1) return null;

            return input.Substring(startIndex, endIndex - startIndex);
        }

        private void Log(string text)
        {
            if (listBox.InvokeRequired)
            {
                listBox.BeginInvoke(new Action(() => listBox.Items.Add(text)));
            }
            else
            {
                listBox.Items.Add(text);
            }
        }

        public async Task StopAsync()
        {
            if (webSocket != null && webSocket.State == WebSocketState.Open)
            {
                await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closed by user", CancellationToken.None);
                Log("⛔ Disconnected.");
            }
        }
    }

}
