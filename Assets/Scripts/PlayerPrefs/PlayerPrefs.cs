using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public delegate void AfterDeletedKeyEventHandler  (string key);
public delegate void BeforeDeletingKeyEventHandler (string key);

public class PlayerPrefs : MonoBehaviour {
  private const string KEY_TYPE_SUFFIX = "_KEY_TYPE_SUFFIX_fcd61b9d-76cb-47f3-ab36-8792f3fc38d5";
  private const string ENCODING_SUFFIX = "_ENCODING_SUFFIX_ab10ca5a-dd8d-46d5-a97f-9a037876dec1";

  private static  bool        initialized = false;
  private static  PlayerPrefs singleton;

  private static List<string> keys;

  public event BeforeDeletingKeyEventHandler BeforeDeletingKey;
  public event AfterDeletedKeyEventHandler   AfterDeletedKey;

  public enum PlayerPrefsType {
    UNSET,
    FLOAT,
    INT,
    STRING,
    LONG,
    DATETIME,
  }

  public enum PlayerPrefsEncoding {
    NONE,
    BASE64,
  }

  void Awake() {
    if (initialized) {
      Destroy(gameObject);
      return;
    }

    initialized = true;
    singleton = this;

		DontDestroyOnLoad (gameObject);
  }

  public static PlayerPrefs getInstance() {
    return singleton;
  }

  public static void init_keys() {
    if (keys != null) {
      return;
    }

    keys = new List<string>();

    // Add known keys here.  This should be updated anytime we add new playerprefs unless we're using this manager to add them.
    keys.Add(PREFS.AD_CLICKS);
    keys.Add(PREFS.GLOBAL_HIGHSCORES_SELECTED);
    keys.Add(PREFS.GLOBAL_SCORES);
    keys.Add(PREFS.LOCAL_SCORES);
    keys.Add(PREFS.MUTE_STATE);
    keys.Add(PREFS.PLAYER_NAME);
    keys.Add(PREFS.SERIALIZED_PLAYER_PREFS_KEYS);
    keys.Add(PREFS.PLAYER_UUID);

    foreach (string key in STATS.getAllStatsKeys()) {
      keys.Add(key);
    }

    deserializeKeys();
  }

  public static void addValidKey(string key) {
    if (key.Contains(ENCODING_SUFFIX) || key.Contains(KEY_TYPE_SUFFIX)) {
      return;
    }

    init_keys();

    // add the key to the internal list, save off the internal list
    if (!keys.Contains(key)) {
      keys.Add(key);

      serializeKeys();
    }
  }

  public static void serializeKeys() {
    SetListString(PREFS.SERIALIZED_PLAYER_PREFS_KEYS, keys);
    Save();
  }

  public static void deserializeKeys() {
    List<string> deserializedKeys = GetListString(PREFS.SERIALIZED_PLAYER_PREFS_KEYS);

    for (int i = 0; i < deserializedKeys.Count; ++i) {
      if (!keys.Contains(deserializedKeys[i])) {
        Debug.LogDebug(deserializedKeys[i]);

        keys.Add(deserializedKeys[i]);
      }
    }
  }

  public static List<string> getKeysList() {
    return keys;
  }

  public static string[] getKeys() {
    string[] keysArray = new string[keys.Count];

    for (int i = 0; i < keys.Count; ++i) {
      keysArray[i] = keys[i];
    }

    return keysArray;
  }

  // Given a key, what type is stored?
  public static PlayerPrefsType getKeyType(string key) {
    return (PlayerPrefsType) UnityEngine.PlayerPrefs.GetInt(key + KEY_TYPE_SUFFIX);
  }

  public static void deleteKeyAndSetType(string key, PlayerPrefsType type) {
    DeleteKey(key);

    switch (type) {
      case PlayerPrefsType.FLOAT:
        SetFloat(key);
        break;
      case PlayerPrefsType.INT:
        SetInt(key);
        break;
      case PlayerPrefsType.STRING:
        SetString(key);
        break;
      default:
        break;
    }
  }

  public static void SetLong(string key, long value) {
    UnityEngine.PlayerPrefs.SetString(key, "" + value);
    UnityEngine.PlayerPrefs.SetInt(key + KEY_TYPE_SUFFIX, (int) PlayerPrefsType.LONG);
    UnityEngine.PlayerPrefs.SetInt(key + ENCODING_SUFFIX, (int) PlayerPrefsEncoding.NONE);
    addValidKey(key);
  }
  
  public static long GetLong(string key, long defaultValue = 0) {
    if (HasKey(key)) {
      try {
        defaultValue = System.Convert.ToInt64(UnityEngine.PlayerPrefs.GetString(key));
        addValidKey(key);
      } catch (System.Exception ex) {
        Debug.LogException(ex);
      }
    }

    return defaultValue;
  }

  public static void SetDateTime(string key, System.DateTime dt) {
    long value = 0;

    try {
      value = dt.ToBinary();

      UnityEngine.PlayerPrefs.SetString(key, "" + value);
      UnityEngine.PlayerPrefs.SetInt(key + KEY_TYPE_SUFFIX, (int) PlayerPrefsType.DATETIME);
      UnityEngine.PlayerPrefs.SetInt(key + ENCODING_SUFFIX, (int) PlayerPrefsEncoding.NONE);
      addValidKey(key);
    } catch (System.Exception ex) {
      Debug.LogException(ex);
    }
  }

  public static System.DateTime GetDateTime(string key) {
    return GetDateTime(key, new System.DateTime());
  }

  public static System.DateTime GetDateTime(string key, System.DateTime defaultValue) {
    long value = 0;
    System.DateTime dt = new System.DateTime();

    try {
      value = System.Convert.ToInt64(UnityEngine.PlayerPrefs.GetString(key));
      dt = System.DateTime.FromBinary(value);
    } catch (System.Exception ex) {
      Debug.LogException(ex);
    }

    return dt;
  }

  // Essentially Overloads
  public static void DeleteAll() {
    UnityEngine.PlayerPrefs.DeleteAll();
  }

  public static void DeleteKey(string key) {
    DeletingKey(key);
    UnityEngine.PlayerPrefs.DeleteKey(key);
    UnityEngine.PlayerPrefs.DeleteKey(key + KEY_TYPE_SUFFIX);
    UnityEngine.PlayerPrefs.DeleteKey(key + ENCODING_SUFFIX);
    keys.Remove(key);
    DeletedKey(key);
  }

  public static void DeletingKey(string key) {
    PlayerPrefs pp = getInstance();

    if (pp.BeforeDeletingKey != null) {
      pp.BeforeDeletingKey(key);
    }
  }

  public static void DeletedKey(string key) {
    PlayerPrefs pp = getInstance();

    if (pp.AfterDeletedKey != null) {
      pp.AfterDeletedKey(key);
    }
  }

  public static float GetFloat(string key, float defaultValue = 0.0f) {
    if (HasKey(key)) {
      defaultValue = UnityEngine.PlayerPrefs.GetFloat(key, defaultValue);
      addValidKey(key);
    }

    return defaultValue;
  }

  public static int GetInt(string key, int defaultValue = 0) {
    if (HasKey(key)) {
      defaultValue = UnityEngine.PlayerPrefs.GetInt(key, defaultValue);
      addValidKey(key);
    }

    return defaultValue;
  }

  public static string GetString(string key, string defaultValue = "") {
    if (HasKey(key)) {
      defaultValue = UnityEngine.PlayerPrefs.GetString(key, defaultValue);

      if (HasKey(key + ENCODING_SUFFIX) &&
          UnityEngine.PlayerPrefs.GetInt(key + ENCODING_SUFFIX) == (int) PlayerPrefsEncoding.BASE64) {
        defaultValue = Base64.decodeString(defaultValue);
      }

      addValidKey(key);
    }

    return defaultValue;
  }

  public static List<string> GetListString(string key) {
    List<string> list = new List<string>();
    string encodedString;
    string[] encodedStrings;

    if (HasKey(key)) {
      encodedString = GetString(key);

      if (encodedString != "") {
        encodedStrings = encodedString.Split(',');

        foreach (string s in encodedStrings) {
          list.Add(s);
        }
      }
    }

    return list;
  }

  public static bool HasKey(string key) {
    if (UnityEngine.PlayerPrefs.HasKey(key)) {
      return true;
    }

    return false;
  }

  public static void Save() {
    UnityEngine.PlayerPrefs.Save();
  }

  private static void SetFloat(string key) {
    SetFloat(key, 0f);
  }

  public static void SetFloat(string key, float value) {
    UnityEngine.PlayerPrefs.SetFloat(key, value);
    UnityEngine.PlayerPrefs.SetInt(key + KEY_TYPE_SUFFIX, (int) PlayerPrefsType.FLOAT);
    UnityEngine.PlayerPrefs.SetInt(key + ENCODING_SUFFIX, (int) PlayerPrefsEncoding.NONE);
    addValidKey(key);
  }

  public static void SetInt(string key) {
    SetInt(key, 0);
  }

  public static void SetInt(string key, int value) {
    UnityEngine.PlayerPrefs.SetInt(key, value);
    UnityEngine.PlayerPrefs.SetInt(key + KEY_TYPE_SUFFIX, (int) PlayerPrefsType.INT);
    UnityEngine.PlayerPrefs.SetInt(key + ENCODING_SUFFIX, (int) PlayerPrefsEncoding.NONE);
    addValidKey(key);
  }

  public static void SetString(string key) {
    SetString(key, "");
  }

  public static void SetString(string key, string value) {
    UnityEngine.PlayerPrefs.SetString(key, value);
    UnityEngine.PlayerPrefs.SetInt(key + KEY_TYPE_SUFFIX, (int) PlayerPrefsType.STRING);
    UnityEngine.PlayerPrefs.SetInt(key + ENCODING_SUFFIX, (int) PlayerPrefsEncoding.NONE);
    addValidKey(key);
  }

  public static void SetListString(string key, List<string> value) {
    string output = "";

    for (int i = 0; i < value.Count; ++i) {
      if (i != value.Count - 1) {
        output += Base64.encodeString(value[i] + ",");
      } else {
        output += Base64.encodeString(value[i]);
      }
    }

    UnityEngine.PlayerPrefs.SetString(key, output);
    UnityEngine.PlayerPrefs.SetInt(key + KEY_TYPE_SUFFIX, (int) PlayerPrefsType.STRING);
    UnityEngine.PlayerPrefs.SetInt(key + ENCODING_SUFFIX, (int) PlayerPrefsEncoding.BASE64);
  }
}
