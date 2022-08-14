using System.Text.Json.Serialization;
using SchoolMgmnt.Models;

namespace SchoolMgmnt.Data;

public class Context
{
    private List<School> _schools;

    public IEnumerable<School> Schools => _schools;

    [JsonIgnore]
    public School? CurrentSchool { get; set; }

    public Context()
    {
        _schools = new();
    }

    [JsonConstructor]
    public Context(IEnumerable<School> schools)
    {
        SetSchools(schools);
    }

    public void SetSchools(IEnumerable<School> schools)
    {
        _schools = schools.ToList();
    }

    public void AddSchool(School school)
    {
        _schools.Add(school);
    }
}
