using UnityEngine;
using System.Collections.Generic;

public class GameVars : MonoBehaviour 
{
#if UNITY_WEBPLAYER
  private const string DEFAULT_PLAYER_NAME = "WEB_PLAYER";
#elif UNITY_STANDALONE || UNITY_EDITOR_WIN
  private const string DEFAULT_PLAYER_NAME = "STANDALONE";
#elif UNITY_ANDROID
  private const string DEFAULT_PLAYER_NAME = "ANDROID";
#endif
  private static GameVars singleton;
  
  public Transform somePrefab;
  
  private GameObject mPlayer;

  private float mScore;
  public float distance;
  public List<AudioClip> sounds;
  public int orcKills = 0;
  public string debugMessage = "";
  private bool mUserHasStarted = false;

  private AudioSource audioSource;

  void Awake()
  {
    audioSource = GetComponent<AudioSource>();

    mUserHasStarted = false;
    // TODO Does this code even do anything consequential?
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

    if (getPlayerName() == null || getPlayerName() == "") {
      setPlayerName(DEFAULT_PLAYER_NAME);
    }
  }

  void Update() {
    switch (Application.loadedLevel) {
      case 0:
        if (audioSource.isPlaying) {
          audioSource.Stop();
        }
        break;
      case 1:
        if (!audioSource.isPlaying) {
          audioSource.Play();
        }
        break;
      default:
        // Do nothing.
        break;
    }
  }

  public static GameVars getInstance()
  {
    if (singleton == null) {
      singleton = new GameVars();
    }

    return singleton;
  }

  public void setPlayerName(string playerName)
  {
    PlayerPrefs.SetString("playerName", playerName);
  }

  public string getPlayerName()
  {
    return PlayerPrefs.GetString("playerName");
  }

  public void setUserHasStarted(bool userHasStarted) {
    mUserHasStarted = userHasStarted;
  }

  public bool getUserHasStarted() {
    return mUserHasStarted;
  }

  public void incrementScore(float increment) {
    mScore += increment;
  }

  public void setScore(float score) {
    mScore = score;
  }

  public float getScore() {
    return mScore;
  }
  
  public GameObject getPlayer()
  {
    if (mPlayer == null)
    {
      mPlayer = GameObject.Find("HeroCop(Clone)");
    }

    return mPlayer;
  }
}
