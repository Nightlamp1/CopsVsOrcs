using UnityEngine;
using System.Collections.Generic;

public class GameVars : MonoBehaviour 
{
  // Scene constants (These need to change anytime we build.
  //  We should no longer use stuff like "Application.loadedLevel == 3"
  //  And use these constants instead.
  public const int MAIN_MENU_SCENE    = 0;
  public const int ENDLESS_RUN_SCENE  = 1;
  public const int GAME_OVER_SCENE    = 2;
  public const int CREDITS_SCENE      = 3;

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
  public float mDistance;
  public List<AudioClip> sounds;
  public int mOrcKills = 0;
  public int mOrcHits = 0;
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

  public void setDistance(float distance) {
    mDistance = distance;
  }

  public float getDistance() {
    return mDistance;
  }

  public void incrementDistance(float inc) {
    setDistance(getDistance() + inc);
  }

  public void setOrcKills(int orcKills) {
    mOrcKills = orcKills;
  }

  public int getOrcKills() {
    return mOrcKills;
  }

  public void incrementOrcKills(int inc) {
    setOrcKills(getOrcKills() + inc);
  }

  public void setOrcHits(int orcHits) {
	mOrcHits = orcHits;
  }
	
  public int getOrcHits() {
	return mOrcHits;
  }
	
  public void incrementOrcHits(int inc) {
	setOrcHits(getOrcHits() + inc);
  }

  public void setUserHasStarted(bool userHasStarted) {
    mUserHasStarted = userHasStarted;
  }

  public bool getUserHasStarted() {
    return mUserHasStarted;
  }


  public float getScore() {
		mScore = (mDistance / 2) + (mOrcKills * 2) - (mOrcHits * 2);
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
