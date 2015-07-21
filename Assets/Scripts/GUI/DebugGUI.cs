using UnityEngine;
using System.Collections;

public class DebugGUI : MonoBehaviour {
  private Vector2 scrollView;

  void Start() {
  }

  void Update() {
  }

  void OnGUI() {
    string currentValue;
    string futureValue;

    keys = PlayerPrefs.getKeys();

    GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height));

    scrollView = GUILayout.BeginScrollView(scrollView);//new Rect(0.05f*Screen.width, 0.05f*Screen.height, 0.9f*Screen.width, 0.9f*Screen.height));

    for (int i = 0; i < 50; ++i) {
      GUILayout.Button("This is a test button " + i + " is my number.");
    }

    GUILayout.FlexibleSpace();
    GUILayout.EndHorizontal();

    scrollView = GUILayout.BeginScrollView(scrollView);

    for (int i = 0; i < keys.Length; ++i) {
      GUILayout.BeginHorizontal();

      GUILayout.TextField(keys[i], new GUILayoutOption[] {GUILayout.Width(Screen.width * 0.15f)});

      string key = keys[i];
      PlayerPrefs.PlayerPrefsType type = PlayerPrefs.getKeyType(key);

      switch (type) {
        case PlayerPrefs.PlayerPrefsType.INT:
          // Int Type
          if (GUILayout.Button(getPPTTexture(DebugGuiTexturesEnum.UNSET_TYPE_BUTTON), buttonOptions)) {
            PlayerPrefs.deleteKeyAndSetType(key, PlayerPrefs.PlayerPrefsType.UNSET);
            break;
          }

          GUILayout.Button(getPPTTexture(DebugGuiTexturesEnum.INT_TYPE_PRESSED), buttonOptions);

          if (GUILayout.Button(getPPTTexture(DebugGuiTexturesEnum.FLOAT_TYPE_BUTTON), buttonOptions)) {
            PlayerPrefs.deleteKeyAndSetType(key, PlayerPrefs.PlayerPrefsType.FLOAT);
            break;
          }

          if (GUILayout.Button(getPPTTexture(DebugGuiTexturesEnum.STRING_TYPE_BUTTON), buttonOptions)) {
            PlayerPrefs.deleteKeyAndSetType(key, PlayerPrefs.PlayerPrefsType.STRING);
            break;
          }

          PlayerPrefs.SetInt(key, int.Parse(GUILayout.TextField(PlayerPrefs.GetInt(keys[i]).ToString())));

          break;
        case PlayerPrefs.PlayerPrefsType.FLOAT:
          // Float Type
          if (GUILayout.Button(getPPTTexture(DebugGuiTexturesEnum.UNSET_TYPE_BUTTON), buttonOptions)) {
            PlayerPrefs.deleteKeyAndSetType(key, PlayerPrefs.PlayerPrefsType.UNSET);
            break;
          }

          if (GUILayout.Button(getPPTTexture(DebugGuiTexturesEnum.INT_TYPE_BUTTON), buttonOptions)) {
            PlayerPrefs.deleteKeyAndSetType(key, PlayerPrefs.PlayerPrefsType.INT);
            break;
          }

          GUILayout.Button(getPPTTexture(DebugGuiTexturesEnum.FLOAT_TYPE_PRESSED), buttonOptions);

          if (GUILayout.Button(getPPTTexture(DebugGuiTexturesEnum.STRING_TYPE_BUTTON), buttonOptions)) {
            PlayerPrefs.deleteKeyAndSetType(key, PlayerPrefs.PlayerPrefsType.STRING);
            break;
          }

          PlayerPrefs.SetFloat(key, float.Parse(GUILayout.TextField(PlayerPrefs.GetFloat(keys[i]).ToString())));

          break;
        case PlayerPrefs.PlayerPrefsType.STRING:
          // String Type
          if (GUILayout.Button(getPPTTexture(DebugGuiTexturesEnum.UNSET_TYPE_BUTTON), buttonOptions)) {
            PlayerPrefs.deleteKeyAndSetType(key, PlayerPrefs.PlayerPrefsType.UNSET);
            break;
          }

          if (GUILayout.Button(getPPTTexture(DebugGuiTexturesEnum.INT_TYPE_BUTTON), buttonOptions)) {
            PlayerPrefs.deleteKeyAndSetType(key, PlayerPrefs.PlayerPrefsType.INT);
            break;
          }

          if (GUILayout.Button(getPPTTexture(DebugGuiTexturesEnum.FLOAT_TYPE_BUTTON), buttonOptions)) {
            PlayerPrefs.deleteKeyAndSetType(key, PlayerPrefs.PlayerPrefsType.FLOAT);
            break;
          }

          GUILayout.Button(getPPTTexture(DebugGuiTexturesEnum.STRING_TYPE_PRESSED), buttonOptions);

          PlayerPrefs.SetString(keys[i], GUILayout.TextArea(PlayerPrefs.GetString(keys[i])));

          break;
        case PlayerPrefs.PlayerPrefsType.LONG:
          // Long Type
          if (GUILayout.Button(getPPTTexture(DebugGuiTexturesEnum.UNSET_TYPE_BUTTON), buttonOptions)) {
            PlayerPrefs.deleteKeyAndSetType(key, PlayerPrefs.PlayerPrefsType.UNSET);
            break;
          }

          if (GUILayout.Button(getPPTTexture(DebugGuiTexturesEnum.INT_TYPE_BUTTON), buttonOptions)) {
            PlayerPrefs.deleteKeyAndSetType(key, PlayerPrefs.PlayerPrefsType.INT);
            break;
          }

          if (GUILayout.Button(getPPTTexture(DebugGuiTexturesEnum.FLOAT_TYPE_BUTTON), buttonOptions)) {
            PlayerPrefs.deleteKeyAndSetType(key, PlayerPrefs.PlayerPrefsType.FLOAT);
            break;
          }

          GUILayout.Button(getPPTTexture(DebugGuiTexturesEnum.STRING_TYPE_PRESSED), buttonOptions);

          currentValue = PlayerPrefs.GetLong(keys[i]).ToString();
          futureValue = GUILayout.TextArea(currentValue);

          try {
            PlayerPrefs.SetLong(keys[i], System.Convert.ToInt64(futureValue));
          } catch (System.Exception ex) {
            Debug.LogException(ex);
          }

          break;
        default:
          // Unset
          GUILayout.Button(getPPTTexture(DebugGuiTexturesEnum.UNSET_TYPE_PRESSED), buttonOptions);

          if (GUILayout.Button(getPPTTexture(DebugGuiTexturesEnum.INT_TYPE_BUTTON), buttonOptions)) {
            PlayerPrefs.deleteKeyAndSetType(key, PlayerPrefs.PlayerPrefsType.INT);
            break;
          }

          if (GUILayout.Button(getPPTTexture(DebugGuiTexturesEnum.FLOAT_TYPE_BUTTON), buttonOptions)) {
            PlayerPrefs.deleteKeyAndSetType(key, PlayerPrefs.PlayerPrefsType.FLOAT);
            break;
          }

          if (GUILayout.Button(getPPTTexture(DebugGuiTexturesEnum.STRING_TYPE_BUTTON), buttonOptions)) {
            PlayerPrefs.deleteKeyAndSetType(key, PlayerPrefs.PlayerPrefsType.STRING);
            break;
          }

          GUILayout.TextArea("");

          break;
      }

      if (GUILayout.Button(getPPTTexture(DebugGuiTexturesEnum.DELETE_BUTTON), buttonOptions)) {
        PlayerPrefs.DeleteKey(key);
      }

      GUILayout.EndHorizontal();
    }


    GUILayout.EndScrollView();

    GUILayout.EndArea();
  }
}
