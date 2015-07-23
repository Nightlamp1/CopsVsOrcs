using UnityEngine;
using System.Collections.Generic;

public delegate void BeforeSceneChangeEventHandler  (SceneManager.Scene sceneFrom, SceneManager.Scene sceneTo);
public delegate void SceneChangedEventHandler       (SceneManager.Scene sceneFrom, SceneManager.Scene sceneTo);

public class SceneManager : MonoBehaviour {
  public const string USER_LEFT_APP = "User Left App";

  // These must be in the exact same order as the build order.  If a scene is removed from the build,
  //  you must remove all scene changes to it.  Search all of the code for "LoadLevel" to find all
  //  scene changes.
  public enum Scene {
    MAIN_MENU,
    ENDLESS_RUN,
    GAME_OVER,
    CREDITS,
    DEBUG,
    AD,
  }

  private static string[] scenes;

  public event BeforeSceneChangeEventHandler  BeforeSceneChange;
  public event SceneChangedEventHandler       SceneChanged;

  private static  bool              initialized = false;
  private static  SceneManager      singleton;

  private         Scene             currentScene = 0;

  void Awake() {
    if (initialized) {
      Destroy(gameObject);
      return;
    }

    initialized = true;
    singleton = this;

    DontDestroyOnLoad(gameObject);

    scenes = new string[6];

    scenes[(int) Scene.MAIN_MENU]   = "Main Menu";
    scenes[(int) Scene.ENDLESS_RUN] = "Endless Run";
    scenes[(int) Scene.GAME_OVER]   = "Game Over";
    scenes[(int) Scene.CREDITS]     = "Credits";
    scenes[(int) Scene.DEBUG]       = "Debug";
    scenes[(int) Scene.AD]          = "Ad";
  }


  void Start() {
    string eventName;

    long secondsSinceInstall =
      System.Convert.ToInt64(
        (System.DateTime.Now - PlayerPrefs.GetDateTime(STATS.DATE_FIRST_LAUNCH)).TotalSeconds);
    long secondsSinceUpdate =
      System.Convert.ToInt64(
        (System.DateTime.Now - PlayerPrefs.GetDateTime(STATS.DATE_LAST_UPDATE)).TotalSeconds);

    Debug.LogDebug(secondsSinceInstall + " and " + secondsSinceUpdate);

    StatsManager.incLong(STATS.SCENE_PREFIX + scenes[Application.loadedLevel], StatsManager.StatSig.CUMULATIVE);
    StatsManager.incLong(STATS.TOTAL_LAUNCH_COUNT, StatsManager.StatSig.CUMULATIVE);

    if (PlayerPrefs.HasKey(STATS.DATE_FIRST_LAUNCH)) {
      // Subsequent installs

      eventName = STATS.TOTAL_LAUNCH_COUNT;
      ReportingManager.LogEvent(
        STATS.GAME_STATISTICS, STATS.STATISTICS_ON_LAUNCH, eventName, PlayerPrefs.GetLong(STATS.TOTAL_LAUNCH_COUNT));

      eventName = STATS.DAYS_SINCE_INSTALL;
      ReportingManager.LogEvent(
        STATS.GAME_STATISTICS, STATS.STATISTICS_ON_LAUNCH, eventName, secondsSinceInstall / 86400);

      if (PlayerPrefs.GetString(STATS.LAST_VERSION) != Application.version) {
        eventName = STATS.UPDATED;
        ReportingManager.LogEvent(
          STATS.GAME_STATISTICS, STATS.STATISTICS_ON_LAUNCH, eventName, secondsSinceUpdate / 86400);

        StatsManager.setDateTime(STATS.DATE_LAST_UPDATE, System.DateTime.Now);
        StatsManager.incLong(STATS.TOTAL_UPDATES, StatsManager.StatSig.CUMULATIVE);
      } else {
      }

      StatsManager.setString(STATS.LAST_VERSION, Application.version);

      eventName = STATS.TOTAL_UPDATES;
      ReportingManager.LogEvent(
          STATS.GAME_STATISTICS, STATS.STATISTICS_ON_LAUNCH, eventName, PlayerPrefs.GetLong(STATS.TOTAL_UPDATES));
    } else {
      // First install
      eventName = STATS.INSTALL;

      StatsManager.setDateTime(STATS.DATE_FIRST_LAUNCH, System.DateTime.Now, StatsManager.StatSig.FIRST_ONLY);
      StatsManager.setString(STATS.FIRST_VERSION, Application.version);

      ReportingManager.LogEvent(
        STATS.GAME_STATISTICS, STATS.STATISTICS_ON_LAUNCH, eventName);
    }

    PlayerPrefs.SetLong(STATS.SECONDS_SINCE_INSTALL, secondsSinceInstall);
    PlayerPrefs.SetLong(STATS.SECONDS_SINCE_UPDATE, secondsSinceUpdate);
    StatsManager.setDateTime(STATS.DATE_LAST_LAUNCH, System.DateTime.Now);
    StatsManager.setString(STATS.LAST_VERSION, Application.version);
  }

  public static SceneManager getInstance() {
    return singleton;
  }

  public static SceneManager.Scene getScene() {
    return (SceneManager.Scene) Application.loadedLevel;
  }

  public void changeScene(Scene newScene) {
    // If trying to load the debug scene, just return, refuse to do it.
    //  Trust me.  It's for the best.
#if !UNITY_EDITOR
    if (newScene == Scene.DEBUG) {
      // 146fcc25-42e8-45cc-9e78-ad2db99fde39 = In a potentially production environment, a change to the debug scene was requested, rejecting it.
      Debug.QuietLogError("146fcc25-42e8-45cc-9e78-ad2db99fde39");
      return;
    }
#endif

    // If any event handlers have attached, process them.
    if (BeforeSceneChange != null) {
      BeforeSceneChange(currentScene, newScene);
    }

    Application.LoadLevel((int)newScene);
    ReportingManager.LogScreen(Application.loadedLevelName);

    // If you see this because you experienced an index out of bounds exception, you probably
    //  want to fix it in the Scene enum above and also in the Awake() function.  Unfortunately,
    //  Unity doesn't let me populate this stuff based on which scenes exist in the build.
    // Other scripts may need to know about that scene as well.

    Debug.LogDebug(
        "Scene changed from " +
        scenes[(int) currentScene] + " aka " + currentScene + " [" + (int) currentScene + "] to " +
        scenes[(int) newScene] + " aka " + newScene + " [" + (int) newScene + "]");

    StatsManager.incLong(STATS.SCENE_PREFIX + scenes[(int) newScene], StatsManager.StatSig.CUMULATIVE);

    PlayerPrefs.Save();
    StatsManager.Save();

    if (SceneChanged != null) {
      SceneChanged(currentScene, newScene);
    }

    currentScene = newScene;
  }

  void OnApplicationPause(bool pauseStatus) {
    StatsManager.incLong(STATS.SCENE_PREFIX + USER_LEFT_APP, StatsManager.StatSig.CUMULATIVE);
  }

  public static void LoadLevel(int newLevel) {
    getInstance().changeScene((Scene) newLevel);
  }

  public static void LoadLevel(Scene newLevel) {
    getInstance().changeScene(newLevel);
  }
}

