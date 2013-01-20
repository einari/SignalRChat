using System.Collections.Generic;

namespace SignalRChat
{
    public class ChatRooms
    {
        static List<string> _rooms = new List<string>();

        static ChatRooms()
        {
            _rooms.Add("Lobby");
        }

        public static void Add(string name)
        {
            _rooms.Add(name);
        }

        public static bool Exists(string name)
        {
            return _rooms.Contains(name);
        }

        public static IEnumerable<string> GetAll()
        {
            return _rooms;
        }
    }
}