using System.Reflection;

namespace Challenge_8
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Challenge_8.Input-Dummy.txt");
            var cards = Parse(stream);

            Console.WriteLine(CalculateTotal(cards));

            stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Challenge_8.Input.txt");
            cards = Parse(stream);

            Console.WriteLine(CalculateTotal(cards));

            Console.ReadLine();
        }

        static int CalculateTotal(List<Card> cards)
        {
            var arr = new int[cards.Count];

            for (var i = 0; i < cards.Count; i++)
            {
                var c = cards[i];
                arr[i] += 1;
                for (var j = 0; j < c.Won(); j++)
                {
                    arr[i + j + 1] += arr[i];
                }
            }

            return arr.Sum();
        }

        static List<Card> Parse(Stream stream)
        {
            var rslt = new List<Card>();

            using var sr = new StreamReader(stream);

            while (sr.Peek() > -1)
            {
                var line = sr.ReadLine();
                rslt.Add(Parse(line));
            }

            return rslt;
        }

        static Card Parse(string line)
        {
            var rslt = new Card();

            var start = line.IndexOf(":") + 1;
            line = line.Substring(start);
            
            var split = line.IndexOf("|");
            var winningString = line.Substring(0, split).Trim();
            var numberString = line.Substring(split + 1).Trim();

            rslt.Winning
                .AddRange(winningString.Split(" ")
                .Where(s => !string.IsNullOrEmpty(s))
                .Select(s => int.Parse(s)));
            rslt.Numbers
                .AddRange(numberString.Split(" ")
                .Where(s => !string.IsNullOrEmpty(s))
                .Select(s => int.Parse(s)));

            return rslt;
        }
    }
}