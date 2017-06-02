using ScratchPad.Factory;
using System;

namespace ScratchPad
{
    class Program
    {
        static void Main(string[] args)
        {
            var testPacket = PacketFactory.CreatePacket<MessagePacket>();
            testPacket.Message = "Something";
            testPacket.Sender = "Someone";

            var packetJson = testPacket.ToJson();
            Console.WriteLine(packetJson);

            PacketFactory.JsonToPacket(packetJson, out IPacket testDeserialize);

            switch (testDeserialize.Type)
            {
                case PacketType.Message:
                    HandleMessage(testDeserialize as MessagePacket);
                    break;
            }

            Console.ReadLine();
        }

        private static void HandleMessage(MessagePacket packet)
        {
            if (packet == null) Console.WriteLine("Well damn");
            Console.WriteLine(packet.Content);
        }
    }
}
