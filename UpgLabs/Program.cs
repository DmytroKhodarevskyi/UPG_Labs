using Labs;

public record Point(int X, int Y);

public class User
{
    public required string Email { get; init; }
}

public class Greeter(string name)
{
    public string Hello() => $"Hi {name}";
}

class Program
{
    static public void W00_1()
    {
        Point a = new(1, 2);
        Point b = new(1, 2);

        Console.WriteLine(a == b); // True
        Console.WriteLine(a.Equals(b)); // True

        var c = a with { X = 5 };

        Console.WriteLine(a == c); // False
        Console.WriteLine(a.Equals(c)); // False
    }

    static public void W00_2()
    {
        string Describe(object obj) => obj switch
        {
            int n when n < 0 => "negative list",
            Point { X: 0, Y: 0 } => "origin",
            null => "null",
            _ => "other"
        };

        //if (obj is string { Length: > 0 } s)

        Console.WriteLine(Describe(new Point(0, 0))); // "origin"
    }

    static public void W00_3()
    {
        //#nullable enable
        //string name = null;
        //string? maybe = args.Length > 0 ? args[0] : null;
        //Console.WriteLine(maybe.Length);
        //if (maybe is not null) Console.WriteLine(maybe.Length);
    }

    static public void W00_4()
    {
        var u = new User { Email = "a@b.cz" }; // OK
        //var bad = new User(); // error: property is required
        //u.Email = "x"; // error: class had proprty set as "init"
    }

    static public void W00_5()
    {
        int[] nums = [1, 2, 3];
        List<int> list = [.. nums, 4, 5]; // unpacking with .. (spread operator)
        Console.WriteLine(new Greeter("Dmytro").Hello());
        Console.WriteLine(string.Join(", ", list));
    }

    static void Main()
    {
        ///////////////////////////
        /// Record acts by value 
        /// for .Equals and ==
        /// 
        //W00_1();
        ///////////////////////////



        ///////////////////////////
        /// Pattern matching
        ///
        //W00_2();
        ///////////////////////////



        ///////////////////////////
        /// Nullable warnings?
        ///
        //W00_3();
        ///////////////////////////



        ///////////////////////////
        /// Required property 
        /// + init setter
        ///
        //W00_4(); 
        ///////////////////////////


        ///////////////////////////
        /// Primary constructors and 
        /// collection expressions
        /// 
        //W00_5();
        ///////////////////////////

        var arr = new DynamicArray<int>();

        arr.Print();

        arr.Add(1);
        arr.Add(2);
        arr.Add(3);
        arr.Add(4);
        arr.Add(5);
        arr.Add(6);
        arr.Add(7);

        arr.Insert(2, 8);
        arr.Insert(2, 8);

        arr.RemoveAt(2);
        arr.RemoveAt(2);


        Console.WriteLine(arr.IndexOf(7));

        arr.Print();   

        arr.Capacity = 20;

        arr.Print();

    }
}