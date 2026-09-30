using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Labs
{
    public class BadPoint
    {
        public int X { get; set; }
        public int Y { get; set; }

        public override bool Equals(object? obj) => obj is BadPoint p && p.X == X && p.Y == Y;

        // if we remove override, and replace with
        // new - creates a separate method and hides original
        // To call original you would need to call "object" method
        //
        // if we go with new path at some point dictionary
        // calls object method and it makes hash result inconsistent
        public override int GetHashCode() => 1;
    }

    public class GoodPoint
    {
        public int X { get; set; }
        public int Y { get; set; }

        public override bool Equals(object? obj) => obj is GoodPoint p && p.X == X && p.Y == Y;

        public override int GetHashCode() => HashCode.Combine(X, Y);
    }

    public record PointRecord
    {
        public int X { get; set;
            //init;
        }
        public int Y { get; set;
            //init;
        }

        // Can't override equality in record!
        //public override bool Equals(object? obj) => obj is PointRecord p && p.X == X && p.Y == Y;

        public override int GetHashCode() => HashCode.Combine(X, Y);
    }

    public static class LabW01Wed
    {
        public static void Run()
        {
            LabUtils.RunStep(
                "Bad Hash insert and lookup",
                () =>
                {
                    Bad();
                }
            );

            LabUtils.RunStep(
                "Good Hash insert and lookup",
                () =>
                {
                    Good();
                }
            );

            LabUtils.RunStep(
                "Mutable key bug with good point",
                () =>
                {
                    MutableKeyBugGoodPoint();
                }
            );

            LabUtils.RunStep(
                "Mutable key bug with bad point",
                () =>
                {
                    MutableKeyBugBadPoint();
                }
            );

            LabUtils.RunStep(
                "Mutable key bug with point record",
                () =>
                {
                    MutableKeyBugPointRecord();
                }
            );

            LabUtils.RunStep(
                "Comparer check dictionary",
                () =>
                {
                    ComparerCheckDictionary();
                }
            );

            LabUtils.RunStep(
                "Comparer check hashset",
                () =>
                {
                    ComparerCheckHashSet();
                }
            );

            LabUtils.RunStep(
                "HashCode result",
                () =>
                {
                    GetHashCodePrint();
                }
            );
        }

        private static void Bad()
        {
            Console.WriteLine("Hash always one behaviour: ");

            var d = new Dictionary<BadPoint, int>();

            var sw = Stopwatch.StartNew();

            for (int i = 0; i < 10_000; i++)
                d[new BadPoint { X = i, Y = i }] = i;

            Console.WriteLine($"insert: {sw.ElapsedMilliseconds} ms");

            sw.Restart();

            for (int i = 0; i < 10_000; i++)
                _ = d[new BadPoint { X = i, Y = i }];

            Console.WriteLine($"lookup: {sw.ElapsedMilliseconds} ms");
        }

        private static void Good()
        {
            Console.WriteLine("Hash normal behaviour: ");

            var d = new Dictionary<GoodPoint, int>();

            var sw = Stopwatch.StartNew();

            for (int i = 0; i < 10_000; i++)
                d[new GoodPoint { X = i, Y = i }] = i;

            Console.WriteLine($"insert: {sw.ElapsedMilliseconds} ms");

            sw.Restart();

            for (int i = 0; i < 10_000; i++)
                _ = d[new GoodPoint { X = i, Y = i }];

            Console.WriteLine($"lookup: {sw.ElapsedMilliseconds} ms");
        }

        private static void MutableKeyBugGoodPoint()
        {
            var key = new GoodPoint { X = 1, Y = 2 };
            var map = new Dictionary<GoodPoint, string> { [key] = "a" };

            key.X = 99;

            // False? - False
            Console.WriteLine(map.ContainsKey(key));
            // True? - False
            Console.WriteLine(map.ContainsKey(new GoodPoint { X = 1, Y = 2 }));
            // 1? - 1
            Console.WriteLine(map.Count);
        }

        private static void MutableKeyBugBadPoint()
        {
            var key = new BadPoint { X = 1, Y = 2 };
            var map = new Dictionary<BadPoint, string> { [key] = "a" };

            key.X = 99;

            // True? - True
            Console.WriteLine(map.ContainsKey(key));
            // False? - False
            Console.WriteLine(map.ContainsKey(new BadPoint { X = 1, Y = 2 }));
            // 1? - 1
            Console.WriteLine(map.Count);
        }

        private static void MutableKeyBugPointRecord()
        {
            var key = new PointRecord { X = 1, Y = 2 };
            var map = new Dictionary<PointRecord, string> { [key] = "a" };

            // Won't compile if i make record readonly with init instead of set!
            key.X = 99;

            // False? - True
            Console.WriteLine(map.ContainsKey(key));
            // False? - False
            Console.WriteLine(map.ContainsKey(new PointRecord { X = 1, Y = 2 }));
            // 1? - 1
            Console.WriteLine(map.Count);
        }

        private static void ComparerCheckDictionary()
        {
            var names = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            names["Hello"] = 1;
            names["HELLO"] = 2;
            // 1? - 1
            Console.WriteLine(names.Count);
        }

        private static void ComparerCheckHashSet()
        {
            var names = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            names.Add("Hello");
            names.Add("HELLO");
            // 1? - 1
            Console.WriteLine(names.Count);
        }

        private static void GetHashCodePrint()
        {
            // different each run!
            Console.WriteLine("hello".GetHashCode());
        }
    }
}
