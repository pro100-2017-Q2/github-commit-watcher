using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ScratchPad.Factory
{
    public static class PacketFactory
    {
        // Create a dictionary of enum type - to - class type
        private static Dictionary<PacketType, Type> packetTypeToClassType = new Dictionary<PacketType, Type>
        {
            { PacketType.Message, typeof(MessagePacket) },
            { PacketType.Join, typeof(JoinPacket) },
            { PacketType.Room, typeof(RoomPacket) }
        };

        // This is what is considered a traditional Factory
        public static IPacket CreatePacket(PacketType type)
        {
            IPacket packet = null;
            switch (type)
            {
                case PacketType.Join:
                    packet = new JoinPacket();
                    break;
                case PacketType.Message:
                    packet = new MessagePacket();
                    break;
                case PacketType.Room:
                    packet = new RoomPacket();
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

        public static bool JsonToPacket<T>(string json, out T packet) where T : IPacket
        {
            // With JSON.Net (Newtonsoft.Json Nuget Package)
            try
            {
                packet = JsonConvert.DeserializeObject<T>(json, new PacketDeseralizerConverter());
            }
            catch
            {
                packet = default(T);
                return false;
            }

            return true;
        }

        // Using JSON.Net, define a custom deserializer that checks the type of the JSON object and returns the correct packet type
        class PacketDeseralizerConverter : JsonConverter
        {
            public override bool CanConvert(Type objectType)
            {
                // If the class we're converting is a packet, or a subclass of packet
                return objectType == typeof(Packet) || objectType.IsSubclassOf(typeof(Packet));
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                // If the given type is a subclass of packet, just return it.
                if (objectType.IsSubclassOf(typeof(Packet))) return JsonConvert.DeserializeObject(jo.ToString(), objectType);

                JObject jo = JObject.Load(reader);
                var packetType = (PacketType)jo["Type"].Value<Int64>();
                // If the given type is NOT a subclass of packet (which means it is packet), grab the type and convert type like that
                if (packetTypeToClassType.ContainsKey(packetType))
                {
                    return JsonConvert.DeserializeObject(jo.ToString(), packetTypeToClassType[packetType]);
                }

                // Throw an exception you care about you can catch in the "JsonToPacket" method
                throw new NotImplementedException();
            }

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                throw new NotImplementedException();
            }
        }
    }
}
