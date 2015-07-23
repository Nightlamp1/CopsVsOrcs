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

    DontDestroyOnLoad(gameObject);
  }

  void Start() {
    SceneManager.getInstance().SceneChanged += new SceneChangedEventHandler(AfterSceneChange);
  }

  public void AfterSceneChange(SceneManager.Scene oldScene, SceneManager.Scene newScene) {
    if (newScene != SceneManager.Scene.ENDLESS_RUN) {
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

    StatsManager.incLong(
        STATS.TOTAL_DEATHS,
        StatsManager.StatSig.CUMULATIVE);

    StatsManager.setLong(
        STATS.PEAK_CASH_EARNED,
        System.Convert.ToInt64(Mathf.Floor(GameVars.getInstance().getScore())),
        StatsManager.StatSig.MAX);

    StatsManager.setLong(
        STATS.PEAK_DISTANCE_RUN,
        System.Convert.ToInt64(Mathf.Floor(GameVars.getInstance().getDistance())),
        StatsManager.StatSig.MAX);

    StatsManager.incLong(
        STATS.TOTAL_CASH_EARNED,
        StatsManager.StatSig.CUMULATIVE,
        System.Convert.ToInt64(Mathf.Floor(GameVars.getInstance().getScore())));

    StatsManager.incLong(
        STATS.TOTAL_DISTANCE_RUN,
        StatsManager.StatSig.CUMULATIVE,
        System.Convert.ToInt64(Mathf.Floor(GameVars.getInstance().getDistance())));
  }

  private void deathSoundOver(float length, bool blocking) {
    SceneManager.LoadLevel(SceneManager.Scene.AD);

    AudioManager.getInstance().SFXEnded -= sfxEndedEventHandler;
  }

  public bool isPlayerAlive() {
    return alive;
  }

  private void setAlive(bool pAlive) {
    AudioManager.getInstance().setFiring(pAlive);
    alive = pAlive;
  }

  public void hit() {
    //AudioManager.getInstance().playPlayerHitSound();
    AudioManager.getInstance().playOuchSound();
  }
}
