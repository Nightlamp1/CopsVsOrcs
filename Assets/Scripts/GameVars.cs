using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameVars : MonoBehaviour 
{
  // Scene constants (These need to change anytime we build.
  //  We should no longer use stuff like "Application.loadedLevel == 3"
  //  And use these constants instead.
  public const string VERSION_NUMBER  = "DEVELOPMENT";

  private const string DEFAULT_PLAYER_NAME = "";
  private const string PLAYER_UUID_STRING  = "PLAYER_UUID";

  private static GameVars singleton;
  
  public Transform somePrefab;
  
  private GameObject mPlayer;
	
  public float mDistance;
  public List<AudioClip> sounds;
  public int mOrcKills = 0;
  public int mOrcHits = 0;
  private int gameSession = 1;
  public int mComboOrcKills = 0;
  private int comboMultiplier = 0;
  private float mScore = 0;
  public string debugMessage = "";
  private bool mUserHasStarted = false;

  private bool validUsername = false;

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
    } else {
      validUsername = true;
      validateUsername(getPlayerName());
    }
  }

  public static GameVars getInstance()
  {
    return singleton;
  }

  public void setPlayerName(string playerName)
  {
    if (playerName != PlayerPrefs.GetString("playerName")) {
      StartCoroutine(validateUsername(playerName));
      PlayerPrefs.SetString("playerName", playerName);
    }
  }

  public IEnumerator validateUsername(string playerName) {
    if (playerName.Length > 16) {
      validUsername = false;
    } else if(playerName.Length == 0) {
      validUsername = true;
    } else {
      string url = "https://www.copsvsorcs.com/validate_username.php?username=" + WWW.EscapeURL(playerName);
      WWW www = new WWW(url);

      yield return www;

      validUsername = false;

      if (www.error == null) {
        if (www.text == "PASS") {
          validUsername = true;
        }
      }
    }
  }

  public bool getValidUsername() {
    return validUsername;
  }

  public string getPlayerName()
  {
    return PlayerPrefs.GetString("playerName");
  }

  public System.Guid getPlayerUuid() {
    string playerUuid = "";

    if (PlayerPrefs.HasKey(PLAYER_UUID_STRING)) {
      playerUuid = PlayerPrefs.GetString(PLAYER_UUID_STRING);
    }

    if (playerUuid == "") {
      playerUuid = (System.Guid.NewGuid()).ToString();

      PlayerPrefs.SetString(PLAYER_UUID_STRING, playerUuid);

      PlayerPrefs.Save();
    }

#if UNITY_EDITOR
    Debug.Log("Player UUID: " + playerUuid);
#endif

    return new System.Guid(playerUuid);
  }

  public void setDistance(float distance) {
    if (!PlayerManager.getInstance().isPlayerAlive()) return;

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

  public void setComboOrcKills(int OrcKills) {
		mComboOrcKills = OrcKills;
	}

  public int getComboOrcKills() {
		return mComboOrcKills;
	}

	public void incrementcomboOrcKills(int inc) {
    setComboOrcKills(getComboOrcKills() + inc);
	}

	public void setComboMultiplier (int value){
		comboMultiplier = value;
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

  public void setGlobalHighScoresSelected(bool globalHighScoresSelected) {
    PlayerPrefs.SetInt("GlobalHighScoresSelected", (globalHighScoresSelected ? 1 : 0));
  }

  public bool getGlobalHighScoresSelected() {
    return (PlayerPrefs.GetInt("GlobalHighScoresSelected") != 0);
  }

  public void setMScore(float scoreVal){
		mScore = scoreVal;
	}

  public void incrementMScore(float incScore){
    mScore = getMScore() + (incScore * comboMultiplier);
	}
  public float getMScore(){
		return mScore;
	}

  public float getScore() {
		return (mDistance / 2) + mScore - (mOrcHits * 2);
  }
  
  public GameObject getPlayer()
  {
    if (mPlayer == null)
    {
      mPlayer = GameObject.Find("HeroCop(Clone)");
    }

    return mPlayer;
  }

  public void setGameSession(int session){
		gameSession = session;
	}

  public int getGameSession(){
		return gameSession;
	}

  public void incrementGameSession(int incSess){
    setGameSession(getGameSession() + incSess);
	}
}
