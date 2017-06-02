using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScratchPad.Factory
{
    public interface IPacket
    {
        string Content { get; }
    }

    public class RoomPacket : IPacket
    {
        public string Content => throw new NotImplementedException();
    }

    public class MessagePacket : IPacket
    {
        public string Content => throw new NotImplementedException();
    }

    public class JoinPacket : IPacket
    {
        public string Content => throw new NotImplementedException();
        public string UserName { get; set; }
    }
}
