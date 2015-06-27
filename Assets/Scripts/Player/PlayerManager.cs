using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour {
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

    alive = true;
  }

  public static PlayerManager getInstance() {
    return singleton;
  }
  
  public void killPlayer() {
    AudioManager.getInstance().disableFiring();
    AudioManager.getInstance().SFXEnded += new SFXEndedEventHandler(deathSoundOver);

    AudioManager.getInstance().playDeathJingle();
  }

  private void deathSoundOver(float length, bool blocking) {
    SceneManager.LoadLevel(GameVars.GAME_OVER_SCENE);
  }

  public bool isPlayerAlive() {
    return alive;
  }
}
