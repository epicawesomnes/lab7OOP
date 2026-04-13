using ds;

namespace LinkedListUsage;

public class Program
{
    public static int Main()
    {
        Console.WriteLine("Constructors demo (and indexer + enumerator): ");

        Console.WriteLine("1) Constructor without params");
        List list1 = new();
        DisplayList(list1);

        Console.WriteLine("2) Constructor with len = 5 param");
        List list2 = new(5);
        DisplayList(list2);

        Console.WriteLine("3) Constructor with [1, 2, 3, 4] param");
        List list3 = new([1, 2, 3, 4]);
        DisplayList(list3);

        Console.WriteLine("Methods demo");
        Console.WriteLine("Starting list: ");
        List list4 = new([-4, 6, 8, 7, 9, 15]);
        DisplayList(list4);

        Console.WriteLine("1) Get the first multiple of 5");
        Console.WriteLine($"{list4.GetFirstmultiple(5)}\n");

        Console.WriteLine("2) Get the amount of positive values");
        Console.WriteLine($"{list4.GetNumPositives()}\n");

        Console.WriteLine("3) Get a new single linked list of nums more than 8");
        DisplayList(list4.GetListOfElementsBiggerThan(8));

        Console.WriteLine("4) Delete nodes with values > avg");
        list4.DeleteElementsBiggerThanAvarage();
        DisplayList(list4);

        return 0;
    }

    public static void DisplayList(List list)
    {
        foreach (var node in list)
        {   
            Console.Write(node.Next is null ? $"{node.Value}" : $"{node.Value} -> ");
        }
        Console.WriteLine("\n");
    }
}