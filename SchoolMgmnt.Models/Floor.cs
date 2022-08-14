using System.Text.Json.Serialization;

namespace SchoolMgmnt.Models;

public class Floor
{
    public int Number { get; set; }

    private readonly List<Room> _rooms;
    public IEnumerable<Room> Rooms => _rooms;

    public Floor(int number)
        : this(number, new List<Room>())
    {
    }

    [JsonConstructor]
    public Floor(int number, IEnumerable<Room> rooms)
    {
        Number = number;
        _rooms = rooms.ToList();

        foreach (var room in _rooms)
        {
            room.Floor = this;
        }
    }

    public void AddRoom(Room room)
    {
        if (room.Number < 0)
        {
            Console.WriteLine("room number must be greater than 0");
            return;
        }

        for (int i = 0; i < _rooms.Count; i++)
        {
            Room r = _rooms[i];
            if (r.Number == room.Number)
            {
                Console.WriteLine("This room number already exists");
                return;
            }
        }

        _rooms.Add(room);
        room.Floor = this;
    }

    public void Print()
    {
        Console.WriteLine($"Floor: {Number} Rooms count: {Rooms.Count()}");
        foreach (Room room in Rooms)
        {
            room.Print();
        }
    }
}
