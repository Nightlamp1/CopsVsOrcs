using System.Collections.Generic;

// This logging class overrides the UnityEngine.Debug class.
//  QuietLog means to log it, but only save the log information in the
//  PlayerPrefs database.
//  Other Log routines log out to the screen.
class Debug {
#if UNITY_EDITOR
  private static bool suppressAnalyticsLogging = true;
#else
  private static bool suppressAnalyticsLogging = false;
#endif

  // Default to a pretty light error log.
  private static LogLevel maxLogLevel = LogLevel.ERROR;

  private static Dictionary<string, LogLevel> logDict;

  // The order of these is crucial!! If you add one, make sure that it is in a sensible place.
  //  Higher in the list (ERROR, for example) is of higher importance.
  public enum LogLevel {
    NONE, // NONE is reserved for setting a maxLogLevel where no logging ever happens.
    ERROR,
    WARN,
    INFO,
    DEBUG
  };

  private static void initLogDict() {
    logDict = new Dictionary<string, LogLevel>();

    logDict.Add("NONE", LogLevel.NONE);
    logDict.Add("ERROR", LogLevel.ERROR);
    logDict.Add("WARN", LogLevel.WARN);
    logDict.Add("INFO", LogLevel.INFO);
    logDict.Add("DEBUG", LogLevel.DEBUG);
  }

  public static LogLevel getLogLevel() {
    return maxLogLevel;
  }

  public static void setLogLevel(LogLevel logLevel) {
    maxLogLevel = logLevel;
  }

  public static void Log(float f) {
    LogDebug(f.ToString());
  }

  public static void Log(string message) {
    LogDebug(message);
  }

  private static void LogConditional(string loggingLevelString, string message) {
    LogLevel loggingLevel;

    initLogDict();

    if (logDict.TryGetValue(loggingLevelString, out loggingLevel)) {
      LogConditional(loggingLevelString, loggingLevel, message);
    } else {
      Log("Unable to identify loggingLevel: " + loggingLevelString + ".");
    }
  }

  private static void LogConditional(string loggingLevelString, LogLevel loggingLevel, string message) {
    // Only log messages equal to or less than the maxLogLevel and only if it's not LogLevel.NONE.
    if (loggingLevel <= maxLogLevel && loggingLevel != LogLevel.NONE) {
      switch(loggingLevel) {
        case LogLevel.WARN:
          UnityEngine.Debug.LogWarning("[" + loggingLevelString + "] " + message);
          break;
        case LogLevel.ERROR:
          UnityEngine.Debug.LogError("[" + loggingLevelString + "] " + message);
          break;
        case LogLevel.DEBUG:
          if (suppressAnalyticsLogging && message.Contains("GoogleAnalyticsV4")) {
            return;
          }

          UnityEngine.Debug.Log("[" + loggingLevelString + "] " + message);
          break;
        case LogLevel.INFO:
        default:
          UnityEngine.Debug.Log("[" + loggingLevelString + "] " + message);
          break;
      }
    }
  }

  public static void LogDebug(string message) {
    LogConditional("DEBUG", message);
  }

  public static void LogError(string message) {
    LogConditional("ERROR", message);
  }

  // When you call this function, make sure you call it with a generated UUID and put
  //  a comment explaining what the message is.
  public static void QuietLogError(string message) {
    try {
      PlayerPrefs.SetInt(message, PlayerPrefs.GetInt(message) + 1);
      ReportingManager.LogEvent("Bug", "Detected", message, PlayerPrefs.GetInt(message) + 1);
    } catch (System.Exception ex) {
      // On the off chance we get an exception, let's go ahead and catch it and log it.
      LogException(ex);
    }
  }

  public static void LogException(System.Exception ex) {
    UnityEngine.Debug.LogException(ex);
  }

  public static void LogException(string loggingLevelString, System.Exception ex) {
    LogLevel loggingLevel;

    initLogDict();

    if (logDict.TryGetValue(loggingLevelString, out loggingLevel)) {
      LogException(loggingLevel, ex);
    } else {
      Log("Unable to identify loggingLevel: " + loggingLevelString + ".");
    }
  }

  public static void LogException(LogLevel loggingLevel, System.Exception ex) {
    // Only log Exceptions equal to or less than the maxLogLevel.
    //  Logging LogLevel.NONE is allowed as some Exceptions should always be logged.
    if (loggingLevel <= maxLogLevel) {
      UnityEngine.Debug.LogException(ex);
    }
  }

  public static void DrawLine(UnityEngine.Vector3 v1, UnityEngine.Vector3 v2) {
    UnityEngine.Debug.DrawLine(v1, v2);
  }

  public static void DrawLine(UnityEngine.Vector3 v1, UnityEngine.Vector3 v2, UnityEngine.Color color) {
    UnityEngine.Debug.DrawLine(v1, v2, color);
  }
}
