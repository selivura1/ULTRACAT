using UnityEngine;
namespace Ultracat
{
    [CreateAssetMenu]
    public class Dungeon : ScriptableObject
    {
        public new string name;
        public string desc;
        public Room[] Rooms;
        public Room[] BossRooms;
        public int RoomsToBoss = 5;
    }
}