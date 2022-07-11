using SchoolMgmnt;
using SchoolMgmnt.Models;

using static SchoolMgmnt.ConsoleHelpers;

Console.WriteLine("Welcome to the School Management System!");

while (true)
{
    ShowMenu(Context.School);

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
            Context.School?.Print();
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

    Context.School = school;

    Console.WriteLine();
    Console.WriteLine($"School '{school.Name}' successfully added");
    school.Print();
    Console.WriteLine();
}

Address GetSchoolAddress()
{
    var country = GetValueFromConsole("Enter school country: ");
    var city = GetValueFromConsole("Enter school city or town: ");
    var street = GetValueFromConsole("Enter school street: ");
    var postalCode = GetIntValueFromConsole("Enter school postal code: ");

    return new(country, city, street, postalCode);
}

void AddFloor()
{
    var floorNumber = GetIntValueFromConsole("Enter floor number: ");
    Floor floor = new(floorNumber);

    Context.School?.AddFloor(floor);
    Context.School?.Print();
    Console.WriteLine();
}

void AddRoom()
{
    while (true)
    {
        var floorNumber = GetIntValueFromConsole("Enter floor number: ");
        var floor = Context.School?.Floors.FirstOrDefault(f => f.Number == floorNumber);

        if (floor is null)
        {
            Console.WriteLine($"Floor {floorNumber} does not exists. Either add new floor or enter correct floor number");
            continue;
        }

        var roomNumber = GetIntValueFromConsole("Enter room number: ");
        var roomType = GetRoomTypeFromConsole("Enter room type: ");

        floor.AddRoom(new(roomNumber, roomType, floor));
        break;
    }

    Context.School?.Print();
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
            Context.School?.AddTeacher(firstName, lastName, age);
            break;
        }
        else if (type == "D")
        {
            Context.School?.AddDirector(firstName, lastName, age);
            break;
        }
        else
        {
            Console.WriteLine("Wrong employee type");
        }
    }

    Context.School?.Print();
    Console.WriteLine();
}
