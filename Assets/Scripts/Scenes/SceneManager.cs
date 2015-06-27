using UnityEngine;
using System.Collections.Generic;

public delegate void BeforeSceneChangeEventHandler  (int sceneFrom, int sceneTo);
public delegate void SceneChangedEventHandler       (int sceneFrom, int sceneTo);

public class SceneManager : MonoBehaviour {
  public event BeforeSceneChangeEventHandler  BeforeSceneChange;
  public event SceneChangedEventHandler       SceneChanged;

  private static  bool          initialized = false;
  private static  SceneManager  singleton;

  private         int           currentScene = 0;

  void Awake() {
    if (initialized) {
      Destroy(gameObject);
      return;
    }

    initialized = true;
    singleton = this;

    DontDestroyOnLoad(gameObject);
  }

  public static SceneManager getInstance() {
    return singleton;
  }

  public void changeScene(int newScene) {
    // If any event handlers have attached, process them.
    if (BeforeSceneChange != null) {
      BeforeSceneChange(currentScene, newScene);
    }
    
    Application.LoadLevel(newScene);

    if (SceneChanged != null) {
      SceneChanged(currentScene, newScene);
    }

    currentScene = newScene;
  }

  // This wraps changeScene as a static function and is just intended to simplify usage of
  //  SceneManager
  public static void LoadLevel(int newLevel) {
    getInstance().changeScene(newLevel);
  }
}

