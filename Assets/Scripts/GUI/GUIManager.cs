using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GUIManager : MonoBehaviour {
  public static GUIManager singleton;

  private int previousLevel = -1;

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
        gameObject.AddComponent<DebugGUI>();
#endif
        break;
      default:
        break;
    }
  }
}
