using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StatsManager : MonoBehaviour {
  private static  bool          initialized = false;
  private static  StatsManager  singleton;

  private static Dictionary<string, long>             LongStats;
  private static Dictionary<string, System.DateTime>  DateTimeStats;

  private static Dictionary<string, StatSig> StatSigs;

  /*
   * All n elements in this must always remain the same as the first
   * n elements from PlayerPrefs.PlayerPrefsType
   */
  public enum StatsType {
    UNSET,
    FLOAT,
    INT,
    STRING,
    LONG,
    DATETIME,
  }

  public enum StatSig {
    NONE,        // Don't send this up
    NORMAL,
    DAYS_PASSED, // NOW - DATETIME_IN_QUESTION
    //These are just ideas
    DIFFERENCE_SINCE_LAST,
    //STD_DEV_BASED_ON_RUNS,
  }

	void Awake(){
    if (initialized) {
      Destroy(gameObject);
      return;
    }

    initialized = true;
    singleton = this;

    LongStats     = new Dictionary<string, long>();
    DateTimeStats = new Dictionary<string, System.DateTime>();
    StatSigs      = new Dictionary<string, StatSig>();
  }

  public static StatsManager getInstance() {
    return singleton;
  }

  void Start() {
    // Hook into Level Changed, so we can save every time the level changes.
  }

  public static void Save() {
    long value = 0;
    long oldValue = 0;
    long newValue = 0;

    Debug.LogDebug("Saving Stats.");

    foreach (string key in LongStats.Keys) {
      value = LongStats[key];

      if (value != 0) {
        oldValue = PlayerPrefs.GetLong(key);
        newValue = oldValue + value;

        Debug.LogDebug("LongStats[key=" + key + "]=" + LongStats[key] + " = oldValue=" + oldValue + " + value=" + value + " = newValue=" + newValue);

        switch (StatSigs[key]) {
          case StatSig.NONE:

            break;

          case StatSig.NORMAL:
            SceneManager.getInstance().googleAnalytics.LogEvent(
                "Statistics", "StatisticsOnScene", key, value);

            break;

          default:
            Debug.LogError("Statistical Significance " + StatSigs[key] + " Not implemented.");

            break;
        }

        PlayerPrefs.SetLong(key, PlayerPrefs.GetLong(key) + value);
      }
    }

    foreach (string key in DateTimeStats.Keys) {
      Debug.LogError("Have not implemented DateTimeStats yet." + key);
    }

    LongStats.Clear();

    PlayerPrefs.Save();
  }

  // Do not ever override unless you know what you are doing.
  public static void SetStatSig(string key, StatSig ss, bool overrideSSProtection = false) {
    if (StatSigs.ContainsKey(key)) {
      if (StatSigs[key] != ss && !overrideSSProtection) {
        Debug.LogError("Trying to change the statistical significance is a problem.  Please fix this immediately.");

        return;
      }

      StatSigs[key] = ss;
    } else {
      StatSigs.Add(key, ss);
    }
  }

  // This only modifies the Stats value for this key.  Save() will get
  //  called automatically and add this value to the saved preferences.
  public static void incLong(string key, long value = 1, StatSig ss = StatSig.NORMAL) {
    if (LongStats.ContainsKey(key)) {
      Debug.LogDebug("Incrementing LongStats[key=" + key + "]=" + LongStats[key] + " by " + value);
      LongStats[key] += value;
      SetStatSig(key, ss);
    } else {
      setLong(key, value, ss);
    }
  }

  // This only gets the Stats value for this key.
  public static long getLong(string key) {
    if (LongStats.ContainsKey(key)) {
      return LongStats[key];
    } else {
      return 0;
    }
  }

  // This only sets the Stats value for this key.  Save() will get
  //  called automatically and add this value to the saved preferences.
  public static void setLong(string key, long value, StatSig ss = StatSig.NORMAL) {
    if (LongStats.ContainsKey(key)) {
      LongStats[key] = value;
    } else {
      LongStats.Add(key, value);
    }
  }

  public static System.DateTime getDateTime(string key) {
    if (LongStats.ContainsKey(key)) {
      return DateTimeStats[key];
    } else {
      return new System.DateTime();
    }
  }

  public static void setDateTime(string key, System.DateTime value, StatSig ss = StatSig.DAYS_PASSED) {
    if (LongStats.ContainsKey(key)) {
      DateTimeStats.Add(key, value);
    } else {
      DateTimeStats[key] = value;
    }
  }
}
