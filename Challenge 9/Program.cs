using System.Reflection;

namespace Challenge_9
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Challenge_9.Input-Dummy.txt");
            var problem = Parse(stream);

            Console.WriteLine(CalculateLowestLocation(problem));

            stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Challenge_9.Input.txt");
            problem = Parse(stream);

            Console.WriteLine(CalculateLowestLocation(problem));

            Console.ReadLine();
        }

        static Int64 CalculateLowestLocation(Problem problem)
        {
            var minimum = Int64.MaxValue;

            foreach (var s in problem.Seeds)
            {
                var x = s;

                foreach (var m in problem.Maps)
                {
                    x = m.GetDestination(x);
                }

                if (x < minimum)
                    minimum = x;
            }

            return minimum;
        }

        static List<Int64> Parse(string seedLine)
        {
            seedLine = seedLine.Substring(seedLine.IndexOf(":") + 1).Trim();

            var numbers = seedLine.Split(" ");

            return numbers.Select(n => Int64.Parse(n)).ToList();
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