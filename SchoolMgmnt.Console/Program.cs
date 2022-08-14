using SchoolMgmnt;
using SchoolMgmnt.Data;
using SchoolMgmnt.Data.Repositories;
using SchoolMgmnt.Models;

using static SchoolMgmnt.ConsoleHelpers;

Console.WriteLine("Welcome to the School Management System!");

Context Ctx = new();

var filePath = GetFilePath();

SchoolRepository schoolRepository = new(Ctx, filePath);

while (true)
{
    ShowMenu(Ctx);

    var choise = GetMenuChoice();

    if (!choise.HasValue)
    {
        Console.WriteLine("Wrong choise");
        continue;
    }

    if (choise == MenuItems.Exit)
    {
        Console.WriteLine("Good Bye!");
        return;
    }

    HandleChoice(choise);
}

void HandleChoice(MenuItems? choice)
{
    switch (choice)
    {
        case MenuItems.AddSchool:
            AddSchool();
            break;
        case MenuItems.SelectSchool:
            SelectSchool();
            break;
        case MenuItems.AddFloor:
            AddFloor();
            break;
        case MenuItems.AddRoom:
            AddRoom();
            break;
        case MenuItems.AddEmployee:
            AddEmployee();
            break;
        case MenuItems.ShowAll:
            // show all
            foreach (var school in Ctx.Schools)
            {
                school.Print();
            }
            break;
        default:
            Console.WriteLine("Unknown choice");
            break;
    }
}

void AddSchool()
{
    var name = GetValueFromConsole("Enter school name: ");

    var address = GetSchoolAddress();

    var openingDate = GetOpeningDateFromConsole("Enter school opening date: ");

    School school = new(name, address, openingDate);

    schoolRepository.AddSchool(school);

    Console.WriteLine();
    Console.WriteLine($"School '{school.Name}' successfully added");
    school.Print();
    Console.WriteLine();

    Address GetSchoolAddress()
    {
        var country = GetValueFromConsole("Enter school country: ");
        var city = GetValueFromConsole("Enter school city or town: ");
        var street = GetValueFromConsole("Enter school street: ");
        var postalCode = GetIntValueFromConsole("Enter school postal code: ");

        return new(country, city, street, postalCode);
    }
}

void SelectSchool()
{
    var schools = schoolRepository.GetSchools().ToArray();

    while (true)
    {
        for (var i = 0; i < schools.Length; i++)
        {
            Console.WriteLine($"{i}: {schools[i].Name}");
        }
        var schoolIndex = GetIntValueFromConsole("Choose school: ");

        if (schoolIndex < schools.Length)
        {
            schoolRepository.SetCurrentSchool(schools[schoolIndex]);
            break;
        }
        Console.WriteLine("Please choose correct number from the list above.");
    }
}

void AddFloor()
{
    var floorNumber = GetIntValueFromConsole("Enter floor number: ");

    schoolRepository.AddFloorToCurrentSchool(new(floorNumber));

    Ctx.CurrentSchool?.Print();
    Console.WriteLine();
}

void AddRoom()
{
    while (true)
    {
        var floorNumber = GetIntValueFromConsole("Enter floor number: ");

        var floor = schoolRepository.GetFloor(floorNumber);
        if (floor is null)
        {
            Console.WriteLine($"Floor {floorNumber} does not exists. Either add new floor or enter correct floor number");
            continue;
        }

        var roomNumber = GetIntValueFromConsole("Enter room number: ");
        var roomType = GetRoomTypeFromConsole("Enter room type: ");

        schoolRepository.AddRoomToCurrentSchool(new(roomNumber, roomType, floor), floor);
        break;
    }

    Ctx.CurrentSchool?.Print();
    Console.WriteLine();
}

void AddEmployee()
{
    var firstName = GetValueFromConsole("Enter employee first name: ");
    var lastName = GetValueFromConsole("Enter employee last name: ");
    var age = GetIntValueFromConsole("Enter employee age: ");

    while (true)
    {
        var type = GetValueFromConsole("If director enter (D/d), if teacher enter (T/t): ").ToUpperInvariant();

        if (type == "T")
        {
            Ctx.CurrentSchool?.AddTeacher(firstName, lastName, age);
            break;
        }
        else if (type == "D")
        {
            Ctx.CurrentSchool?.AddDirector(firstName, lastName, age);
            break;
        }
        else
        {
            Console.WriteLine("Wrong employee type");
        }
    }

    Ctx.CurrentSchool?.Print();
    Console.WriteLine();
}

string GetFilePath()
{
    var fileName = GetValueFromConsole("Enter storage file name: ");
    var desktopFolder = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
    var fullPath = Path.Combine(desktopFolder, fileName);

    return fullPath;
}