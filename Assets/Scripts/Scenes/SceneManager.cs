using UnityEngine;
using System.Collections.Generic;

public delegate void BeforeSceneChangeEventHandler  (SceneManager.Scene sceneFrom, SceneManager.Scene sceneTo);
public delegate void SceneChangedEventHandler       (SceneManager.Scene sceneFrom, SceneManager.Scene sceneTo);

public class SceneManager : MonoBehaviour {
  // These must be in the exact same order as the build order.  If a scene is removed from the build,
  //  you must remove all scene changes to it.  Search all of the code for "LoadLevel" to find all 
  //  scene changes.
  public enum Scene {
    MAIN_MENU,
    ENDLESS_RUN,
    GAME_OVER,
    CREDITS,
    DEBUG,
  }

  private static string[] scenes;

  public event BeforeSceneChangeEventHandler  BeforeSceneChange;
  public event SceneChangedEventHandler       SceneChanged;

  private static  bool              initialized = false;
  private static  SceneManager      singleton;

  private         Scene             currentScene = 0;

  public          GoogleAnalyticsV4 googleAnalytics;

  void Awake() {
    if (initialized) {
      Destroy(gameObject);
      return;
    }

    initialized = true;
    singleton = this;

    DontDestroyOnLoad(gameObject);

    scenes = new string[5];

    scenes[(int) Scene.MAIN_MENU]    = "Main Menu";
    scenes[(int) Scene.ENDLESS_RUN]  = "Endless Run";
    scenes[(int) Scene.GAME_OVER]   = "Game Over";
    scenes[(int) Scene.CREDITS]     = "Credits";
    scenes[(int) Scene.DEBUG]       = "Debug";
  }

  public static SceneManager getInstance() {
    return singleton;
  }

  public void changeScene(Scene newScene) {
    // If any event handlers have attached, process them.
    if (BeforeSceneChange != null) {
      BeforeSceneChange(currentScene, newScene);
    }
    
    Application.LoadLevel((int)newScene);
    googleAnalytics.LogScreen(Application.loadedLevelName);

    // If you see this because you experienced an index out of bounds exception, you probably 
    //  want to fix it in the Scene enum above and also in the Awake() function.  Unfortunately,
    //  Unity doesn't let me populate this stuff based on which scenes exist in the build.
    // Other scripts may need to know about that scene as well.

    Debug.LogDebug(
        "Scene changed from " + 
        scenes[(int) currentScene] + " aka " + currentScene + " [" + (int) currentScene + "] to " + 
        scenes[(int) newScene] + " aka " + newScene + " [" + (int) newScene + "]");

    PlayerPrefs.Save();

    if (SceneChanged != null) {
      SceneChanged(currentScene, newScene);
    }

    currentScene = newScene;
  }

  public static void LoadLevel(int newLevel) {
    getInstance().changeScene((Scene) newLevel);
  }

  public static void LoadLevel(Scene newLevel) {
    getInstance().changeScene(newLevel);
  }
}

