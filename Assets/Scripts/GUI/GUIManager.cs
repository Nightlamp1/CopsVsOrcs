using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GUIManager : MonoBehaviour {
  private BeforeSceneChangeEventHandler leavingDebugBeforeSceneChangeEventHandler;
  public static GUIManager singleton;

  private int previousLevel = -1;
  private Component debugGuiComponent;

  [Tooltip("These are the textures for the Debug GUI.  They must match the order of the array below.")]
  public Texture2D[] debugGuiTextures;
  [Tooltip("These are the enums for the textures in the Debug GUI.  They must match the order of the array above.")]
  public DebugGUI.DebugGuiTexturesEnum[] debugGuiTexturesEnum;

//  public Texture2D getDeleteButtonAsset() {
//    return DeleteButtonAsset;
//  }

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
    // Don't run through this again if we haven't switched levels.
    if (previousLevel == Application.loadedLevel) {
      return;
    }

    // Quickly set previousLevel so we don't accidentally run this switch again if
    //  it takes too long to run the first time.
    previousLevel = Application.loadedLevel;

    switch ((SceneManager.Scene) Application.loadedLevel) {
      case SceneManager.Scene.MAIN_MENU:
        break;
      case SceneManager.Scene.ENDLESS_RUN:
        break;
      case SceneManager.Scene.GAME_OVER:
        break;
      case SceneManager.Scene.CREDITS:
        break;
#if UNITY_EDITOR
      case SceneManager.Scene.DEBUG:
        if (Debug.getLogLevel() == Debug.LogLevel.DEBUG && Application.isEditor) {
          debugGuiComponent = gameObject.AddComponent<DebugGUI>();
          leavingDebugBeforeSceneChangeEventHandler = new BeforeSceneChangeEventHandler(LeavingDebugScene);
          SceneManager.getInstance().BeforeSceneChange += leavingDebugBeforeSceneChangeEventHandler;
        } else {
          // This error is purposely vague.  I don't want users to understand it.
          //  Reduces the odds that anyone tries to reverse engineer this to
          //  figure out what the debug menu is and how to get to it.
          // This message meaning is "Tried to load the Debug Scene in an invalid configuration"
          //  because the log level didn't match or because the game isn't running in the editor.
          Debug.QuietLogError("c3689a90-1b42-4887-a018-858cd039ca76");
        }
#endif
        break;
      default:
        break;
    }
  }

  void LeavingDebugScene(SceneManager.Scene oldScene, SceneManager.Scene newScene) {
    Destroy(debugGuiComponent);
  }
}
