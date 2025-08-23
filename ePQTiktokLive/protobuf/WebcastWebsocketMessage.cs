using Google.Protobuf;

using Google.Protobuf.Reflection;
using System;

namespace TikTok
{
    public sealed partial class WebcastWebsocketMessage : IMessage<WebcastWebsocketMessage>, IEquatable<WebcastWebsocketMessage>
    {
        private static readonly MessageParser<WebcastWebsocketMessage> _parser =
            new MessageParser<WebcastWebsocketMessage>(() => new WebcastWebsocketMessage());

        public static MessageParser<WebcastWebsocketMessage> Parser => _parser;

        // 🔥 Descriptor bắt buộc (fake thôi)
        public static MessageDescriptor Descriptor => null;
        MessageDescriptor IMessage.Descriptor => Descriptor;

        public string Type { get; set; } = "";
        public ByteString Payload { get; set; } = ByteString.Empty;

        public void MergeFrom(WebcastWebsocketMessage message)
        {
            if (message == null) return;
            if (!string.IsNullOrEmpty(message.Type)) Type = message.Type;
            if (message.Payload != null && message.Payload.Length > 0) Payload = message.Payload;
        }

        public void MergeFrom(CodedInputStream input)
        {
            while (input.ReadTag() is uint tag && tag != 0)
            {
                switch (tag)
                {
                    case 10: Type = input.ReadString(); break;
                    case 18: Payload = input.ReadBytes(); break;
                    default: input.SkipLastField(); break;
                }
            }
        }

        public void WriteTo(CodedOutputStream output)
        {
            if (!string.IsNullOrEmpty(Type))
            {
                output.WriteRawTag(10);
                output.WriteString(Type);
            }
            if (Payload != null && Payload.Length > 0)
            {
                output.WriteRawTag(18);
                output.WriteBytes(Payload);
            }
        }

        public int CalculateSize()
        {
            int size = 0;
            if (!string.IsNullOrEmpty(Type))
                size += 1 + CodedOutputStream.ComputeStringSize(Type);
            if (Payload != null && Payload.Length > 0)
                size += 1 + CodedOutputStream.ComputeBytesSize(Payload);
            return size;
        }

        public WebcastWebsocketMessage Clone() => new WebcastWebsocketMessage
        {
            Type = this.Type,
            Payload = this.Payload
        };

        // ✅ Implement IEquatable
        public bool Equals(WebcastWebsocketMessage other)
        {
            if (other == null) return false;
            return this.Type == other.Type && this.Payload.Equals(other.Payload);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as WebcastWebsocketMessage);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + Type.GetHashCode();
                hash = hash * 23 + Payload.GetHashCode();
                return hash;
            }
        }
    }
}
