using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour {
  private SFXEndedEventHandler sfxEndedEventHandler;

  private static  bool          initialized = false;
  private static  PlayerManager singleton;

  private         bool          alive = false;

  void Awake() {
    if (initialized) {
      Destroy(gameObject);
      return;
    }

    initialized = true;
    singleton = this;
  }

  void Start() {
    SceneManager.getInstance().SceneChanged += new SceneChangedEventHandler(AfterSceneChange);
  }

  public void AfterSceneChange(int oldScene, int newScene) {
    Debug.Log("Scene changed.");

    if (newScene != GameVars.ENDLESS_RUN_SCENE) {
      return;
    }

    setAlive(true);
  }

  public static PlayerManager getInstance() {
    return singleton;
  }
  
  public void killPlayer() {
    setAlive(false);

    AudioManager.getInstance().disableFiring();
    sfxEndedEventHandler = new SFXEndedEventHandler(deathSoundOver);
    AudioManager.getInstance().SFXEnded += sfxEndedEventHandler;

    AudioManager.getInstance().playDeathJingle();
  }

  private void deathSoundOver(float length, bool blocking) {
    SceneManager.LoadLevel(GameVars.GAME_OVER_SCENE);

    AudioManager.getInstance().SFXEnded -= sfxEndedEventHandler;
  }

  public bool isPlayerAlive() {
    return alive;
  }

  private void setAlive(bool pAlive) {
    AudioManager.getInstance().setFiring(pAlive);
    alive = pAlive;
  }
}
