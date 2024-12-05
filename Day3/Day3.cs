using System.Text.RegularExpressions;

namespace AoC.Day3 {
  public class Day3 {
    private static string Input = File.ReadAllText("../input/day3_input.txt");
    private static string TestInput = File.ReadAllText("../input/day3_test_input.txt");

    public static void Main(string[] args) {
      //System.Console.WriteLine($"The result of each valid mul: {Part1(TestInput)}.");
      //System.Console.WriteLine(Part2());
      System.Console.WriteLine($"The result of each valid mul: {Part1(Input)}.");
    }

    public static double Part1(string input) {
      List<int> values = new();
      string pattern = @"mul\(\d+\,\d+\)";

      Match m = Regex.Match(input, pattern);

      while (m.Success) {
        System.Console.WriteLine($"'{m.Value}' found at position '{m.Index}'");
        string match = m.Value;
        pattern = @"\d+";
        Match numbers = Regex.Match(match, pattern);

        while (numbers.Success) {
          System.Console.WriteLine($"'{numbers.Value}' found at position '{numbers.Index}'");
          values.Add(Int32.Parse(numbers.Value));
          numbers = numbers.NextMatch();
        }

        m = m.NextMatch();
      }

      double result = 0;
      for (int i = 0; i < values.Count; i += 2) {
        int localResult = values[i] * values[i + 1];
        result += localResult;
      }

      return result;
    }

    public static void Part2() {

    }
  }
}