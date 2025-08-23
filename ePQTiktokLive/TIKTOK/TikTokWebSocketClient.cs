//using ePQTiktokLive.protobuf;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TikTok;

namespace ePQTiktokLive.TIKTOK
{
    public class TikTokWebSocketClient
    {
        private readonly string _url;
        private ClientWebSocket _socket;

        public event Action<string> OnCommentReceived;

        public TikTokWebSocketClient(string url)
        {
            _url = url;
        }

        public async Task ConnectAsync()
        {
            _socket = new ClientWebSocket();
            await _socket.ConnectAsync(new Uri(_url), CancellationToken.None);

            var buffer = new byte[65536];
            while (_socket.State == WebSocketState.Open)
            {
                var result = await _socket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                if (result.MessageType == WebSocketMessageType.Close)
                    break;

                byte[] data = buffer.Take(result.Count).ToArray();
                HandleMessage(data);
            }
        }

        private void HandleMessage(byte[] data)
        {
            //using (var ms = new MemoryStream(data))
            //{
            //    try
            //    {
            //        var webcastResponse = Serializer.Deserialize<WebcastResponse>(ms);
            //        if (webcastResponse.Payload is IEnumerable<ChatMessage> messages)
            //        {
            //            foreach (var msg in messages)
            //            {
            //                if (msg.Content == "WebcastChatMessage")
            //                { 
            //                    using (var inner = new MemoryStream(Encoding.UTF8.GetBytes(msg.Content)))
            //                    {
                               
            //                        var chat = Serializer.Deserialize<WebcastChatMessage>(inner);
            //                        var text = chat.User?.Nickname + ": " + chat.Comment;
            //                        OnCommentReceived?.Invoke(text);
            //                    }
            //                }
            //            }
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        Debug.WriteLine("Protobuf decode error: " + ex.Message);
            //    }
            //}
        }
    }
}
