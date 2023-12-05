using System.Diagnostics;
using System.Reflection;

namespace Challenge_10
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Challenge_10.Input-Dummy.txt");
            var problem = Parse(stream);

            Console.WriteLine(CalculateLowestLocation(problem));

            var sw = new Stopwatch();
            sw.Start();

            stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Challenge_10.Input.txt");
            problem = Parse(stream);

            Console.WriteLine(CalculateLowestLocation(problem));
            Console.WriteLine("Done!");

            sw.Stop();
            Console.WriteLine(sw.ElapsedMilliseconds);

            Console.ReadLine();
        }

        static bool IsInSeed(Int64 val, Problem problem)
        {
            foreach(var s in problem.Seeds)
            {
                if (val >= s.Item1 && val < s.Item1 + s.Item2)
                    return true;
            }

            return false;
        }

        static Int64 CalculateLowestLocation(Problem problem)
        {
            var minimum = Int64.MaxValue;

            problem.Maps.Reverse();

            for (var i = 0; i < 224309688; i++)
            {
                if (i % 1000000 == 0)
                    Console.WriteLine(i);

                var x = (Int64)i;

                foreach(var m in problem.Maps)
                {
                    x = m.GetSource(x);
                }

                if (IsInSeed(x, problem))
                    return i;
            }

            return -1;
        }

        static List<Tuple<Int64, Int64>> Parse(string seedLine)
        {
            var rslt = new List<Tuple<Int64, Int64>>();

            seedLine = seedLine.Substring(seedLine.IndexOf(":") + 1).Trim();

            var numbersS = seedLine.Split(" ");

            var numbers = numbersS.Select(n => Int64.Parse(n)).ToList();

            for(var i = 0; i < numbers.Count; i+=2)
            {
                rslt.Add(new Tuple<Int64, Int64>(numbers[i], numbers[i + 1]));
            }

            return rslt;
        }

        static Problem Parse(Stream stream)
        {
            var rslt = new Problem();

            using var sr = new StreamReader(stream);

            var seedLine = sr.ReadLine();

            rslt.Seeds = Parse(seedLine);

            sr.ReadLine();

            var lines = new List<string>();
            while (sr.Peek() > -1)
            {
                var line = sr.ReadLine();
                if (string.IsNullOrEmpty(line))
                {
                    var map = new Map();
                    map.Parse(lines);
                    rslt.Maps.Add(map);

                    lines.Clear();
                }
                else
                {
                    if (char.IsDigit(line[0]))
                        lines.Add(line);
                }
            }

            var map2 = new Map();
            map2.Parse(lines);
            rslt.Maps.Add(map2);

            return rslt;
        }
    }
}