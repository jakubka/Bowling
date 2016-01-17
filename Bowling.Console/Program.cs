using System;

namespace Bowling.Console
{
    using Console = System.Console;

    internal class Program
    {
        private static void Main(
            string[] args)
        {
            try
            {
                string input = Console.ReadLine();

                var parser = new BowlingLineParser();

                var line = parser.ParseLine(input);

                var totalScore = line.CalculateTotalScore();

                Console.WriteLine(totalScore);
            }
            catch (ParseException e)
            {
                Console.Error.WriteLine($"Invalid input: {e.Message}");
            }
            catch (Exception e)
            {
                Console.Error.WriteLine($"Unknown error: {e.Message}");
            }
        }
    }
}