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

            stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Challenge_10.Input.txt");
            problem = Parse(stream);

            Console.WriteLine("Done!");
            Console.WriteLine(CalculateLowestLocation(problem));

            Console.ReadLine();
        }

        static Int64 CalculateLowestLocation(Problem problem)
        {
            var minimum = Int64.MaxValue;

            foreach (var tpl in problem.Seeds)
            {
                Console.WriteLine(tpl.ToString());

                for (var i = 0; i < tpl.Item2; i++)
                {
                    var x = tpl.Item1 + i;

                    foreach (var m in problem.Maps)
                    {
                        x = m.GetDestination(x);
                    }

                    if (x < minimum)
                        minimum = x;
                }
            }

            return minimum;
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