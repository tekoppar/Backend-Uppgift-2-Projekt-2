using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projekt2.Classes;

namespace Projekt2FuckAll.Classes
{
    public enum RoomType
    {
        SingleBed,
        DoubleBed,
        PresidentialSuite
    }

    public class RoomSystem
    {
        static public RoomSystem RMS;
        private Dictionary<int, Room> Rooms = new Dictionary<int, Room>();
        public List<Room> ListRooms = new List<Room>();
        public Dictionary<int, int> ListRoomsLUT = new Dictionary<int, int>();

        public RoomSystem()
        {
            RoomSystem.RMS = this;
            this.GetRoomsDB();
        }

        static public Room GetRoom(int room)
        {
            if (RoomSystem.RMS.Rooms.ContainsKey(room) == true)
                return RoomSystem.RMS.Rooms[room];

            return null;
        }

        private void GetRoomsDB()
        {
            List<DPRoom> dps = DatabaseSystem.DBS.Select<DPRoom>("room", new List<string>() { "Id", "roomNumber", "floor", "roomType" });

            foreach (DPRoom dp in dps)
            {
                this.AddNewRoom(dp);
            }
        }

        private void AddNewRoom(DPRoom room)
        {
            if (this.Rooms.ContainsKey(room.RoomNumber) == false)
            {
                this.Rooms.Add(room.RoomNumber, new Room(room));
                this.ListRooms.Add(new Room(room));
                this.ListRoomsLUT.Add(room.Id, this.ListRooms.Count - 1);
            }
        }

        static public void AddRoomDB(DPRoom room)
        {
            DatabaseSystem.DBS.InsertSingle<DPRoom>("rom", new List<string>() { "Id", "roomNumber", "floor", "roomType" }, room);
            DPRoom dp = DatabaseSystem.DBS.SelectSingleWhere<DPRoom>("room", new List<string>() { "Id", "roomNumber", "floor", "roomType" }, new List<string>() { "Id" }, new List<string>() { "max(Id)" });
            RoomSystem.RMS.AddNewRoom(dp);
        }
    }

    public class DPRoom
    {
        public int Id { get; set; }
        public int RoomNumber { get; set; }
        public int Floor { get; set; }
        public int RoomType { get; set; }
    }

    public class Room
    {
        private int DBID;
        public int RoomNumber;
        public int Floor;
        public RoomType Type;
        public string RoomName { get; set; }

        public Room(int roomNumber, int floor, RoomType type = RoomType.SingleBed)
        {
            this.RoomNumber = roomNumber;
            this.Floor = floor;
            this.Type = type;
            this.RoomName = "Room: " + this.RoomNumber.ToString() + " - Floor: " + this.Floor.ToString() + " - Type: " + this.Type.ToString();
        }

        public Room(DPRoom dp)
        {
            this.DBID = dp.Id;
            this.RoomNumber = dp.RoomNumber;
            this.Floor = dp.Floor;
            string roomType = DatabaseSystem.DBS.SelectSingleWhere<string>("roomType", new List<string>() { "type" }, new List<string>() { "Id" }, new List<string>() { dp.RoomType.ToString() });
            roomType = roomType.Replace(" ", "");
            this.Type = (RoomType)Enum.Parse(typeof(RoomType), roomType);
            this.RoomName = "Room: " + this.RoomNumber.ToString() + " - Floor: " + this.Floor.ToString() + " - Type: " + this.Type.ToString();
        }

        public int GetRoomDB()
        {
            return this.DBID;
        }
    }
}
