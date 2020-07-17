using NLog;
using NLog.Config;
using NLog.Targets;
namespace DotNetConsoleApp {

    public sealed class LogUtil {
        public static Logger logger = LogManager.GetCurrentClassLogger();

        static LogUtil() {
            LoggingConfiguration conf = new LoggingConfiguration();
            //ファイル出力定義
            FileTarget logFile = new FileTarget("file") {
                Encoding = System.Text.Encoding.UTF8,
                Layout = "${longdate} [${uppercase:${level:padding=-5}}] ${callsite}() - ${message}${exception:format=ToString}",
                FileName = ComUtil.GetCurrentDir() + "logs/process_${date:format=yyyyMMdd}.log",
                ArchiveNumbering = ArchiveNumberingMode.Date,
                ArchiveFileName = "logs/sample.log.{#}",
                ArchiveEvery = FileArchivePeriod.None,
                MaxArchiveFiles = 20
            };
            conf.AddRule(LogLevel.Debug, LogLevel.Fatal, logFile);

            ConsoleTarget logconsole = new ConsoleTarget("logconsole") {
                Layout = "${time} [${uppercase:${level:padding=-5}}] ${callsite}() - ${message}${exception:format=ToString}"
            };
            conf.AddRule(LogLevel.Debug, LogLevel.Fatal, logconsole);

            // 設定を反映する
            LogManager.Configuration = conf;
        }
    }
}
