using Newtonsoft.Json;
using System;

namespace ScratchPad.Factory
{
    public interface IPacket
    {
        string Content { get; }
        PacketType Type { get; }
    }

    public abstract class Packet : IPacket
    {
        public abstract string Content { get; }
        public abstract PacketType Type { get; }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

    public class RoomPacket : Packet
    {
        public override PacketType Type => PacketType.Room;
        public override string Content => throw new NotImplementedException();

    }

    public class MessagePacket : Packet
    {
        public override PacketType Type => PacketType.Message;
        public override string Content => $"{Sender} sent message: {Message}";

        public string Message { get; set; }
        public string Sender { get; set; }

    }

    public class JoinPacket : Packet
    {
        public override PacketType Type => PacketType.Join;
        public override string Content => throw new NotImplementedException();

        public string UserName { get; set; }
    }
}
