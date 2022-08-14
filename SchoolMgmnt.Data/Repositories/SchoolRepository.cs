using System.Text.Json;
using System.Text.Json.Serialization;
using SchoolMgmnt.Models;

namespace SchoolMgmnt.Data.Repositories;

public class SchoolRepository
{
    private readonly Context _ctx;
    private readonly string _fileName;

    public SchoolRepository(Context ctx, string fileName)
    {
        _ctx = ctx;
        _fileName = fileName;

        LoadData();
    }

    private void LoadData()
    {
        var content = File.ReadAllText(_fileName);
        if (string.IsNullOrEmpty(content))
        {
            return;
        }

        var ctx = JsonSerializer.Deserialize<Context>(content);
        if (ctx is null)
        {
            return;
        }

        _ctx.SetSchools(ctx.Schools);
    }

    public IEnumerable<School> GetSchools()
    {
        return _ctx.Schools;
    }

    public School? GetSchool(string name)
    {
        return _ctx.Schools.Where(s => s.Name == name).SingleOrDefault();
    }

    public School? GetCurrentSchool()
    {
        return _ctx.CurrentSchool;
    }

    public void SetCurrentSchool(School? school)
    {
        _ctx.CurrentSchool = school;
    }

    public void AddSchool(School school)
    {
        _ctx.AddSchool(school);
        SetCurrentSchool(school);
        SaveContext();
    }

    public void AddFloorToCurrentSchool(Floor floor)
    {
        _ctx.CurrentSchool?.AddFloor(floor);
        SaveContext();
    }

    public Floor? GetFloor(int floorNumber)
    {
        return _ctx.CurrentSchool?.Floors.Where(f => f.Number == floorNumber).FirstOrDefault();
    }

    public void AddRoomToCurrentSchool(Room room, Floor floor)
    {
        floor.AddRoom(room);
        SaveContext();
    }

    private void SaveContext()
    {
        var json = JsonSerializer.Serialize(_ctx, new JsonSerializerOptions
        {
            WriteIndented = true,
            ReferenceHandler = ReferenceHandler.IgnoreCycles
        });
        File.WriteAllText(_fileName, json);
    }
}