using ScratchPad.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScratchPad
{
    class Program
    {
        static void Main(string[] args)
        {
            // Old way
            IPacket iPacket = PacketFactory.CreatePacket(PacketType.Join);
            // iPacket is an "IPacket" so you can't access the stuff you want to. Instead you'll have to cast it.
            JoinPacket joinPacket = iPacket as JoinPacket;
            // joinPacket can now access join specific fields
            joinPacket.UserName = "Test";

            // New way
            JoinPacket joinPacket2 = PacketFactory.CreatePacket<JoinPacket>();
            // joinPacket is a "JoinPacket" so you can automatically access its fields.
            joinPacket2.UserName = "Test";

            Console.WriteLine(joinPacket.UserName);
            Console.WriteLine(joinPacket2.UserName);
            Console.ReadLine();
        }
    }
}
