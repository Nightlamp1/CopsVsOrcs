using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerPrefs : MonoBehaviour {
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

    deserializeKeys();
  }

  public static void addValidKey(string key) {
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

  public List<string> getKeysList() {
    return keys;
  }

  public string[] getKeys() {
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
  
  public static long GetLong(string key) {
    long value = 0;

    try {
      value = System.Convert.ToInt64(UnityEngine.PlayerPrefs.GetString(key));
      addValidKey(key);
    } catch (System.Exception ex) {
      Debug.LogException(ex);
    }

    return value;
  }

  // Essentially Overloads
  public static void DeleteAll() {
    UnityEngine.PlayerPrefs.DeleteAll();
  }

  public static void DeleteKey(string key) {
    UnityEngine.PlayerPrefs.DeleteKey(key);
    addValidKey(key);
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
          //Debug.LogDebug(
        }
      }
    }

    return list;
  }

  public static bool HasKey(string key) {
    if (UnityEngine.PlayerPrefs.HasKey(key)) {
      addValidKey(key);
      return true;
    }

    return false;
  }

  public static void Save() {
    UnityEngine.PlayerPrefs.Save();
  }

  public static void SetFloat(string key, float value) {
    UnityEngine.PlayerPrefs.SetFloat(key, value);
    addValidKey(key);
  }

  public static void SetInt(string key, int value) {
    UnityEngine.PlayerPrefs.SetInt(key, value);
    addValidKey(key);
  }

  public static void SetString(string key, string value) {
    UnityEngine.PlayerPrefs.SetString(key, value);
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

    SetString(key, output);
  }
}
