using mialco.abstractions;
using NLog;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace mialco.utilities
{
    public class MialcoLogger: IMialcoLogger
    {

		//Implementing a singleton

		private static MialcoLogger _loggerInstance;
		NLog.Logger _fileLogger;
		NLog.Logger _consoleLogger;
		NLog.Logger _colorConsoleLogger;
		NLog.Logger _dbLogger;

		static MialcoLogger()
		{
			_loggerInstance = new MialcoLogger();
		}

		public void Configure(string logName)
		{
			var config = new NLog.Config.LoggingConfiguration();
			var logFile = new NLog.Targets.FileTarget("logFile") { FileName = logName };
			//var logconsole = new NLog.Targets.ConsoleTarget("logconsole");
			var logColorConsole = new NLog.Targets.ColoredConsoleTarget("logcolorconsole");

			//Rules for mappling logger to targets
			//config.AddRule(LogLevel.Info, LogLevel.Fatal, logconsole);
			config.AddRule(LogLevel.Debug, LogLevel.Fatal, logFile);
			//config.AddRule(LogLevel.Debug, LogLevel.Fatal, logconsole) ;
			config.AddRule(LogLevel.Debug, LogLevel.Fatal, logColorConsole);


			// Apply config
			NLog.LogManager.Configuration = config;
			_fileLogger = NLog.LogManager.GetLogger("logFile");
			//_consoleLogger = NLog.LogManager.GetLogger("consolelogger");
			//_dbLogger = NLog.LogManager.CreateNullLogger();
			//_colorConsoleLogger = NLog.LogManager.GetLogger("logcolorconsole");
			_consoleLogger= NLog.LogManager.GetLogger("logcolorconsole");

			NLog.LogManager.AutoShutdown = true;
			
		}
		public static MialcoLogger GetLogger() 
		{
			return _loggerInstance;
		}
		private List<MialcoLogEntryBase> _logs { get; set; }

		public void LogWarning(string message, string referenceId, [CallerMemberName] string caller = "")
		{
			var logmessage = buildLogMessage(message, referenceId, caller);
			_consoleLogger.Warn(logmessage);
			//_colorConsoleLogger.Warn(logmessage);
			//_fileLogger.Warn(logmessage);
			//_dbLogger.Warn(logmessage);

		}

		public void LogInfo(string message, string referenceId, [CallerMemberName] string caller = "")
		{
			var logmessage = buildLogMessage(message, referenceId, caller);
			_consoleLogger.Info(logmessage);
			//_colorConsoleLogger.Info(logmessage);
			//_fileLogger.Info(logmessage);
			//_dbLogger.Info(logmessage);
		}

		public void LogError(string message, string referenceId, [CallerMemberName] string caller = "")
		{
			var logmessage = buildLogMessage(message, referenceId, caller);
			_consoleLogger.Error(logmessage);
			//_colorConsoleLogger.Error(logmessage);
			//_fileLogger.Error(logmessage);
			//_dbLogger.Error(logmessage);
		}

		public void LogException(Exception ex,  string message, string referenceId, [CallerMemberName] string caller = "")
		{
			var logmessage = buildLogMessage(message, referenceId, caller);
			_consoleLogger.Error(ex, logmessage);
			//_colorConsoleLogger.Error(ex,logmessage);
			//_fileLogger.Error(ex, logmessage);
			//_dbLogger.Error(ex, logmessage);
		}

		public void LogException(Exception ex)
		{
			_consoleLogger.Error(ex);
			//_colorConsoleLogger.Error(ex);
			//_fileLogger.Error(ex);
			//_dbLogger.Error(ex);
		}


		private void WriteLog(string message, string referenceId, string caller, LogType type)
		{
			MialcoLogEntryBase log = new MialcoLogEntry
			{
				Message = message,
				ReferenceId = referenceId,
				Caller = caller,
				TimeStamp = DateTime.Now.ToString(),
				LogType = type.ToString()
			};
			var logger  = NLog.LogManager.GetLogger("consolelogger");
			//logger.
			//_logs.Add(log);
		}

		private string buildLogMessage(string message, string referenceId, [CallerMemberName] string caller = "")
		{			
			var logmessage = $"{referenceId??string.Empty}|{message??string.Empty}|{caller}";
			return logmessage;
		}
	}
}
