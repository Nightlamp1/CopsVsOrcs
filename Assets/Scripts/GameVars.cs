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

  void Awake()
  {
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

  public static GameVars getInstance()
  {
    if (singleton == null) {
      print("========== This is a major problem, this should not happen. ==========");
      return null;
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
