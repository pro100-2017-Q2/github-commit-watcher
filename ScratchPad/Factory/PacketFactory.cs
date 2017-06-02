using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScratchPad.Factory
{
    public static class PacketFactory
    {
        // This is what is considered a traditional Factory
        public static IPacket CreatePacket(PacketType type)
        {
            IPacket packet = null;
            switch (type) {
                case PacketType.Room:
                    packet = new RoomPacket();
                    break;
                case PacketType.Join:
                    packet = new JoinPacket();
                    break;
                case PacketType.Message:
                    packet = new MessagePacket();
                    break;
                default:
                    Console.WriteLine("Crud");
                    break;
            }
            return packet;
        }

        // Generics makes this whole thing much nicer
        // This function takes a type, ensures that it is of type "IPacket" and that it has a default constructor (no parameters)
        public static T CreatePacket<T>() where T : IPacket, new()
        {
            return new T();
        }
    }
}
