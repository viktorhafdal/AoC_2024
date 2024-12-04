namespace AoC.Day2 {

  public class Day2 {
    private static List<List<int>> _reports = new();

    private static string Input = File.ReadAllText("../input/day2_input.txt");
    private static string TestInput = File.ReadAllText("../input/day2_test_input.txt");

    public static void Main(string[] args) {
      SplitInput(TestInput);
      System.Console.WriteLine($"Number of safe reports: {Part1()}.");
      System.Console.WriteLine($"Number of safe reports: {Part2()}.");
      // System.Console.WriteLine(Part1());
      // System.Console.WriteLine(Part2());
    }

    private static int Part1() {
      Dictionary<int, bool> reportStatuses = CheckReports(_reports);

      int safeReports = 0;
      foreach (var kvpair in reportStatuses) {
        if (kvpair.Value == true) {
          safeReports++;
        }
      }

      return safeReports;
    }

    private static string Part2() {
      Dictionary<int, bool> reportStatuses = CheckPart2Reports();

      int safeReports = 0;
      foreach (var kvpair in reportStatuses) {
        if (kvpair.Value == true) {
          safeReports++;
        }
      }

      return $"Number of safe reports: {safeReports}";
    }

    private static void SplitInput(string input) {
      foreach (string line in input.ReplaceLineEndings("\n").TrimEnd().Split("\n")) {
        List<int> currentLine = new();
        var lineParts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        for (int i = 0; i < lineParts.Length; i++) {
          if (Int32.TryParse(lineParts[i], out int entry)) currentLine.Add(entry);
        }
        _reports.Add(currentLine);
      }
    }

    private static Dictionary<int, bool> CheckReports(List<List<int>> reports) {
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
      return reportStatuses;
    }

    private static Dictionary<int, bool> CheckPart2Reports() {
      Dictionary<int, bool> reportStatuses = CheckReports(_reports);
      Dictionary<int, bool> _newReportStatuses = new();
      Dictionary<int, int> indexesToRemove = new();
      List<List<int>> _r = _reports;

      foreach (List<int> row in _reports) {
        bool asc = true;
        bool desc = true;
        bool withinBounds = true;

        // loop through each col of row and check if asc or desc
        for (int i = 1; i < row.Count; i++) {
          if (row[i - 1] >= row[i]) asc = false;
          if (row[i - 1] <= row[i]) desc = false;

          // if both descending and ascending are false, check if by removing any element we achieve true
          if (!asc && !desc) {
            // loop through each col of row and try removing
            for (int j = 0; j < row.Count; j++) {
              int counter = 0;
              List<int> rowBuffer = row;
              int oldSafeReports = Part1();

              if (!withinBounds) {
                for (int k = 0; k < row.Count; k++) {
                  rowBuffer.RemoveAt(k);
                  _r.Add(rowBuffer);
                  _newReportStatuses = CheckReports(_r);
                }
              }

              int newSafeReports = 0;
              foreach (var kvpair in _newReportStatuses) {
                if (kvpair.Value == true) {
                  newSafeReports++;
                }
              }

              if (newSafeReports > oldSafeReports) {
                System.Console.WriteLine($"Extra safe report found, by removing index {i} in {row}");
                indexesToRemove.Add(counter, i);
                continue;
              }

              rowBuffer.RemoveAt(j); // remove element from row
              _r.Add(rowBuffer); // add element to collection to check

              foreach (var kvpair in _newReportStatuses) {
                if (kvpair.Value == true) {
                  newSafeReports++;
                }
              }

              if (newSafeReports > oldSafeReports) {
                System.Console.WriteLine($"Extra safe report found, by removing index {i} in {row}");
                if (!indexesToRemove.ContainsKey(counter)) indexesToRemove.Add(counter, i);
                continue;
              }
            }

          }

          foreach (int index in indexesToRemove.Keys) {

          }

        }
      }
      return reportStatuses = CheckReports(_r); // check new collection, outputting num of safeReports in this collection
    }
  }
}