using Google.Protobuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Google.Protobuf;

namespace ePQTiktokLive.protobuf
{
    public class WebcastWebsocketRequest
    {
        [JsonPropertyName("payloadType")]
        public string PayloadType { get; set; }

        [JsonPropertyName("payloadEncoding")]
        public string PayloadEncoding { get; set; } = "pb";

        [JsonPropertyName("payload")]
        public string Payload { get; set; } // base64 chuỗi sau khi Serialize Protobuf
    }    
}
