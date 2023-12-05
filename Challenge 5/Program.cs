﻿using System.Reflection;

namespace Challenge_5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Challenge_5.Input-Dummy.txt");
            var schematic = Parse(stream);

            var total = schematic.Parts.Where(p => schematic.Symbols.Any(s => p.IsAdjacent(s))).Sum(p => p.Id);
            var parts = schematic.Parts.Where(p => schematic.Symbols.Any(s => p.IsAdjacent(s))).ToList();
            Console.WriteLine(total);

            stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Challenge_5.Input.txt");
            schematic = Parse(stream);

            total = schematic.Parts.Where(p => schematic.Symbols.Any(s => p.IsAdjacent(s))).Sum(p => p.Id);
            parts = schematic.Parts.Where(p => schematic.Symbols.Any(s => p.IsAdjacent(s))).ToList();
            Console.WriteLine(total);

            Console.WriteLine(total);
            Console.ReadLine();
        }

        static int IndexNotNumber(string str)
        {
            for (var i = 0; i < str.Length; i++)
            {
                if (!Char.IsDigit(str[i]))
                    return i;
            }

            return -1;
        }

        static Schematic Parse(Stream stream)
        {
            var schematic = new Schematic();

            using var sr = new StreamReader(stream);

            var y = 0;
            while (sr.Peek() > -1)
            {
                var line = sr.ReadLine();

                var number = "";
                for (var i = 0; i < line.Length; i++)
                {
                    if (Char.IsDigit(line[i]))
                    {
                        number += line[i];
                    } else
                    {
                        if (!string.IsNullOrEmpty(number))
                        {
                            schematic.Parts.Add(new Part()
                            {
                                Id = int.Parse(number),
                                Start = new Point(i - number.Length, y)
                            });

                            number = "";
                        }

                        if (line[i] != '.')
                        {
                            schematic.Symbols.Add(new Point(i, y));
                        }
                    }
                }

                if (!string.IsNullOrEmpty(number))
                {
                    schematic.Parts.Add(new Part()
                    {
                        Id = int.Parse(number),
                        Start = new Point(line.Length - number.Length, y)
                    });

                    number = "";
                }

                y++;
            }

            return schematic;
        }
    }
}