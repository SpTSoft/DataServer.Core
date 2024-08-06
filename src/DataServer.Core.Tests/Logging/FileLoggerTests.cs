using DataServer.Core.Logging;
using DataServer.Core.Logging.Settings;

namespace DataServer.Core.Tests.Logging
{

	[TestClass]
	public class FileLoggerTests
	{
		[TestMethod]
		public void CreateInstance()
		{
			FileLoggerSettings fileLoggerSettings = new FileLoggerSettings();
			FileLogger fileLogger = new(fileLoggerSettings);
		}

		[TestMethod]
		public void LogMessage()
		{
			FileLoggerSettings fileLoggerSettings = new FileLoggerSettings();
			FileLogger fileLogger = new(fileLoggerSettings);
			fileLogger.Log("Hello world!");
		}

		[TestMethod]
		public void LogLoggerMessage()
		{
			FileLoggerSettings fileLoggerSettings = new FileLoggerSettings();
			FileLogger fileLogger = new(fileLoggerSettings);
			LoggerMessage loggerMessage = new LoggerMessage("Hello world, with LoggerMessage");
			fileLogger.Log(loggerMessage);
		}

		[TestMethod]
		public void LogLoggerExceptionMessage()
		{
			FileLoggerSettings fileLoggerSettings = new FileLoggerSettings();
			FileLogger fileLogger = new(fileLoggerSettings);
			LoggerExceptionMessage loggerExceptionMessage = new LoggerExceptionMessage("Hello world, with LoggerExceptionMessage", new OutOfMemoryException());
			fileLogger.Log(loggerExceptionMessage);
		}
	}
}
