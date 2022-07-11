using SchoolMgmnt.Models;

namespace SchoolMgmnt;

public static class ConsoleHelpers
{
    public static void ShowMenu(School? school)
    {
        Console.WriteLine();
        Console.WriteLine("What do you want to perform?");

        Dictionary<MenuItems, string> menuItems = new()
        {
            { MenuItems.AddSchool, "Create school" },
            { MenuItems.AddFloor, "Add floor to the school" },
            { MenuItems.AddRoom, "Add room to the floor" },
            { MenuItems.AddEmployee, "Add employee" },
            { MenuItems.ShowAll, "Show all information" },
            { MenuItems.Exit, "Exit" }
        };

        foreach (var item in menuItems)
        {
            if (item.Key != MenuItems.AddSchool || school is null)
            {
                Console.WriteLine($"{(int)item.Key}: {item.Value}");
            }
        }
    }

    public static MenuItems? GetMenuChoice()
    {
        return Enum.TryParse<MenuItems>(Console.ReadLine(), out var choice)
            ? choice
            : (MenuItems?)null;
    }


    public static string GetValueFromConsole(string message)
    {
        string? consoleValue;
        while (true)
        {
            Console.Write(message);
            consoleValue = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(consoleValue))
            {
                break;
            }
        }
        return consoleValue;
    }

    public static int GetIntValueFromConsole(string message)
    {
        int intValue;
        while (true)
        {
            var strValue = GetValueFromConsole(message);
            if (int.TryParse(strValue, out intValue))
            {
                break;
            }
            Console.WriteLine($"{strValue} is not correct number");
        }
        return intValue;
    }


    public static RoomType GetRoomTypeFromConsole(string message)
    {
        RoomType roomType;

        while (true)
        {
            ShowRoomTypes();
            var strValue = GetValueFromConsole(message);

            if (Enum.TryParse<RoomType>(strValue, out roomType))
            {
                return roomType;
            }
            Console.WriteLine($"Incorrect room type: {strValue}");
        }

        void ShowRoomTypes()
        {
            foreach (var type in RoomTypeExt.RoomTypes)
            {
                Console.WriteLine($"{type.Key} - {type.Value}");
            }

            Console.WriteLine("Please choose the room type. If there could be more than one type you combine them by adding numbers. For example: 'Regular' and 'Biology' will be 1 + 4 = 5");
        }
    }

    public static DateOnly GetOpeningDateFromConsole(string message)
    {
        DateOnly openingDate;

        while (true)
        {
            var strValue = GetValueFromConsole(message);

            if (DateOnly.TryParse(strValue, out openingDate))
            {
                break;
            }
            Console.WriteLine($"{strValue} is not correct date format. Try 'YYYY-MM-DD'");
        }
        return openingDate;
    }
}