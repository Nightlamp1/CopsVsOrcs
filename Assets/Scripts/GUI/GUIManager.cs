using UnityEngine;
using System.Collections;

public class GUIManager : MonoBehaviour {
  public static GUIManager singleton;

  private int previousLevel = -1;

  void Awake()
  {
    if(singleton != null && singleton != this)
    {
      Destroy(gameObject);
      //singleton = this;
    }
    else
    {
      DontDestroyOnLoad(gameObject);
      singleton = this;
    }
  }

  public static GUIManager getInstance() {
    return singleton;
  }

  void Start() {
  }

  void Update() {
  }

  void OnGUI() {
    if (previousLevel == Application.loadedLevel) {
      return;
    }

    // Quickly set previousLevel so we don't accidentally run this switch again if
    //  it takes too long to run the first time.
    previousLevel = Application.loadedLevel;

    switch (Application.loadedLevel) {
      case GameVars.MAIN_MENU_SCENE:
        break;
      case GameVars.ENDLESS_RUN_SCENE:
        break;
      case GameVars.GAME_OVER_SCENE:
        break;
      case GameVars.CREDITS_SCENE:
        break;
#if UNITY_EDITOR
      case GameVars.DEBUG_SCENE:
        gameObject.AddComponent<DebugGUI>();
#endif
        break;
      default:
        break;
    }
  }
}
