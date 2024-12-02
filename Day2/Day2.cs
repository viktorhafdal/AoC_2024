namespace AoC.Day2 {

  public class Day2 {
    private static List<List<int>> reports = new();

    private static string Input = File.ReadAllText("../input/day2_input.txt");
    private static string TestInput = File.ReadAllText("../input/day2_test_input.txt");

    public static void Main(string[] args) {
      // System.Console.WriteLine(Part1(TestInput));
      System.Console.WriteLine(Part1(Input));
    }

    private static string Part1(string input) {
      foreach (string line in input.ReplaceLineEndings("\n").TrimEnd().Split("\n")) {
        List<int> currentLine = new();
        var lineParts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        for (int i = 0; i < lineParts.Length; i++) {
          if (Int32.TryParse(lineParts[i], out int entry)) currentLine.Add(entry);
        }
        reports.Add(currentLine);
      }

      Dictionary<int, bool> reportStatuses = new();

      int counter = 0;
      foreach (List<int> row in reports) {
        bool asc = true;
        bool desc = true;
        bool withinBounds = true;

        // loop through each col of row and check if asc or desc
        for (int i = 1; i < row.Count; i++) {
          if (row[i - 1] >= row[i]) asc = false;
          if (row[i - 1] <= row[i]) desc = false;

          if (Math.Abs(row[i] - row[i - 1]) > 3) withinBounds = false;
        }

        if ((asc || desc) && withinBounds) {
          reportStatuses.Add(counter, true);
        } else {
          reportStatuses.Add(counter, false);
        }
        counter++;
      }

      int safeReports = 0;
      foreach (var kvpair in reportStatuses) {
        if (kvpair.Value == true) {
          safeReports++;
        }
      }

      return $"Number of safe reports: {safeReports}";
    }

    private static void Part2(string input) {

    }
  }
}
