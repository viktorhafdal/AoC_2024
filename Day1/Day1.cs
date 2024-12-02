namespace AoC.Day1 {

  public class Day1 {
    private static long[] _ListA;
    private static long[] _ListB;
    private static Dictionary<long, int> _Occurences = new();
    private static long _SimilScore { get; set; }

    private static string Input = File.ReadAllText("../input/day1_input.txt");
    private static string TestInput = File.ReadAllText("../input/day1_test_input.txt");

    public static void Main(string[] args) {
      // System.Console.WriteLine(Part1(TestInput));
      // System.Console.WriteLine(Part2(TestInput));
      System.Console.WriteLine(Part1(Input));
      System.Console.WriteLine(Part2(Input));
    }

    private static string Part1(string input) {
      // buffers to contain the split parts
      List<long> listABuffer = new();
      List<long> listBBuffer = new();

      // replace line endings for consistent splitting
      foreach (string line in input.ReplaceLineEndings("\n").TrimEnd().Split("\n")) {
        var parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries); // remove empty entries and whitespaces from substrings

        if (parts.Length >= 2) {
          if (long.TryParse(parts[0], out long first)) listABuffer.Add(first);
          if (long.TryParse(parts[1], out long second)) listBBuffer.Add(second);
        }
      }

      // returning to original for part 2 
      _ListA = listABuffer.ToArray();
      _ListB = listBBuffer.ToArray();

      // order by ascending
      var sortedA = _ListA.Order().ToList();
      var sortedB = _ListB.Order().ToList();

      long _totalDistance = 0;
      for (int i = 0; i < sortedA.Count(); i++) {
        _totalDistance += Math.Abs(sortedA[i] - sortedB[i]);
      }

      return _totalDistance.ToString();
    }

    private static string Part2(string input) {
      foreach (long number in _ListA) {
        List<long> _B = _ListB.ToList();

        for (int i = 0; i < _ListB.Length; i++) {
          if (_B[i].Equals(number)) {
            if (_Occurences.ContainsKey(number)) _Occurences[number]++;
            else _Occurences[number] = 1;
          }
        }
      }

      foreach (long number in _Occurences.Keys) {
        _SimilScore += (number * _Occurences.GetValueOrDefault(number));
      }

      return _SimilScore.ToString();
    }
  }
}