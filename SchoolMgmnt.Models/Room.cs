using Newtonsoft.Json;

namespace SchoolMgmnt.Models;

public class Room
{
    public int Number { get; set; }

    [JsonIgnore]
    public Floor Floor { get; set; }

    public RoomType Type { get; set; }

    public Room(int number, RoomType type, Floor floor)
    {
        Number = number;
        Type = type;
        Floor = floor;
    }

    public void Print()
    {
        Console.WriteLine($"Room: {Number}, {Type}, Floor: {Floor.Number}");
    }
}
