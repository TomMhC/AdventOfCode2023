using Challenge_4;
using System.Reflection;

namespace AdventOfCode
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Challenge_4.Input-Dummy.txt");
            var games = LoadGames(stream);

            var total = games
                .Sum(g => g.Power());
            Console.WriteLine(total);

            stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Challenge_4.Input.txt");
            games = LoadGames(stream);

            total = games
                .Sum(g => g.Power());

            Console.WriteLine(total);
            Console.ReadLine();
        }

        static List<Game> LoadGames(Stream stream)
        {
            var games = new List<Game>();

            using (var reader = new StreamReader(stream))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();

                    var indOfColon = line.IndexOf(":");
                    var gameId = line.Substring(5, indOfColon - 5);

                    var game = new Game()
                    {
                        Id = int.Parse(gameId)
                    };

                    var continuation = line.Substring(indOfColon + 1);
                    var indOfSemi = continuation.IndexOf(";");  
                    while (indOfSemi > -1)
                    {
                        var set = ParseSet(continuation.Substring(0, indOfSemi));
                        game.Sets.Add(set);
                        continuation = continuation.Substring(indOfSemi + 1);
                        indOfSemi = continuation.IndexOf(";");
                    }

                    var lastSet = ParseSet(continuation);
                    game.Sets.Add(lastSet);

                    games.Add(game);
                }
            }

            return games;
        }

        static Set ParseSet(string input)
        {
            var set = new Set();

            int indOfSemi;

            do
            {
                indOfSemi = input.IndexOf(",");

                var indOfRed = input.IndexOf("red");
                var indOfBlue = input.IndexOf("blue");
                var indOfGreen = input.IndexOf("green");

                if (indOfRed < 0) indOfRed = int.MaxValue;
                if (indOfBlue < 0) indOfBlue = int.MaxValue;
                if (indOfGreen < 0) indOfGreen = int.MaxValue;

                if (indOfRed < indOfBlue && indOfRed < indOfGreen)
                {
                    var val = input.Substring(0, indOfRed);
                    set.Red += int.Parse(val);
                }
                else if (indOfBlue < indOfGreen)
                {
                    var val = input.Substring(0, indOfBlue);
                    set.Blue += int.Parse(val);
                }
                else
                {
                    var val = input.Substring(0, indOfGreen);
                    set.Green += int.Parse(val);
                }

                input = input.Substring(indOfSemi + 1);
            } while (indOfSemi > -1);

            return set;
        }
    }
}