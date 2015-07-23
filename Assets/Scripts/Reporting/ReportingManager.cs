using UnityEngine;

class ReportingManager : MonoBehaviour {
  public GoogleAnalyticsV4 googleAnalytics = null;
  private static ReportingManager singleton;
  private static bool initialized = false;

  void Awake() {
    if (initialized) {
      Destroy(gameObject);
      return;
    }

    initialized = true;
    singleton = this;
  }

  public static ReportingManager getInstance() {
    return singleton;
  }

  public static void LogEvent(string category, string action, string label, long value = 0) {
    ga().LogEvent(category, action, label, value);
  }

  public static void LogScreen(string title) {
    ga().LogScreen(title);
  }

  public static GoogleAnalyticsV4 ga() {
    return getInstance().googleAnalytics;
  }
}
