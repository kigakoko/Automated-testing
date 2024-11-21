using System.Diagnostics;

namespace Common;

public static class TestLogger
{
	private static readonly string logFilePath = Path.Combine(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\.."), "test_execution_log.txt");

	public static void LogExecutionTime(string testName, Stopwatch stopwatch)
	{
		using StreamWriter logFile = new(logFilePath, true);
		logFile.WriteLine($"{testName} executed in: {stopwatch.ElapsedMilliseconds} ms");
		logFile.Flush();
	}
}
