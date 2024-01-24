using CSUI_Teams_Sync.Components.Configurations;
using Microsoft.Extensions.Options;
using NLog;
using NLog.Targets;
using LogLevel = NLog.LogLevel;

namespace CSUI_Teams_Sync.Components.Commons
{
    public class LogManagerCustom : Logger
    {
        private string mainLogFileFullName = "";
        private string currentLogFileFullName = "";
        public readonly Logger logger;
        public readonly LoggerConfig _config;

        public LogManagerCustom(IOptions<LoggerConfig> config)
        {
            _config = config.Value;
            logger = LogManager.GetCurrentClassLogger();

        }

        public void InitializeLogger()
        {
            string LogDirectory = _config.LogDirectory;
            string LogFileName_Main = _config.LogFileName_Main;
            string LogFileLevel = _config.LogFileLevel;
            string LogConsoleLevel = _config.LogConsoleLevel;

            string LogFilePath = getNewLogFullPath(LogDirectory, LogFileName_Main);

            var config = new NLog.Config.LoggingConfiguration();

            var logfile = new FileTarget("logfile")
            {
                FileName = LogFilePath,
                Layout = "${date:format=yyyyMMdd-HHmmss}|${level}|${callsite}|${message}"
            };
            var logconsole = new ConsoleTarget("logconsole");

            config.AddRule(LogLevel.FromString(LogConsoleLevel), LogLevel.Fatal, logconsole);
            config.AddRule(LogLevel.FromString(LogFileLevel), LogLevel.Fatal, logfile);

            LogManager.Configuration = config;
            currentLogFileFullName = LogFilePath;
            mainLogFileFullName = LogFilePath;
        }
        public void changeLogFileName(string newLogName)
        {
            string LogDirectory = _config.LogDirectory;
            changeLogFileName(LogDirectory, newLogName);
        }
        public void changeLogFileName(string logSubDirectory, string newLogName)
        {
            string LogDirectory = Path.Combine(_config.LogDirectory, logSubDirectory);
            string logFilePath = getNewLogFullPath(LogDirectory, newLogName);
            logger.Info("changing logfile path to " + logFilePath);
            FileTarget target = LogManager.Configuration.FindTargetByName<FileTarget>("logfile");
            target.FileName = logFilePath;
            currentLogFileFullName = logFilePath;
        }
        public void resetLogFileFullName()
        {
            FileTarget target = LogManager.Configuration.FindTargetByName<FileTarget>("logfile");
            target.FileName = mainLogFileFullName;
            currentLogFileFullName = mainLogFileFullName;
        }
        private string getNewLogFullPath(string logDirectory, string logName)
        {
            string logExtension = ".txt";
            string logFilePath = Path.Combine(logDirectory, logName);
            return logFilePath + "_" + System.DateTime.Now.ToString("yyyyMMdd-HHmmss") + logExtension;
        }
        public string getCurrentLogFilePath()
        {
            return currentLogFileFullName;
        }
    }
}
