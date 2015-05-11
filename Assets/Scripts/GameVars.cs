using UnityEngine;
using System.Collections.Generic;

public class GameVars : MonoBehaviour 
{
  private static GameVars singleton;
  
  public Transform somePrefab;
  
  private GameObject mPlayer;

  public float score;
  public float distance;
  public List<AudioClip> sounds;
  public int orcKills = 0;
  private string mPlayerName = "";
  public string debugMessage = "";

  void Awake()
  {
#if UNITY_WEBPLAYER
#elif UNITY_STANDALONE || UNITY_EDITOR_WIN
    mPlayerName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
#elif UNITY_ANDROID
    mPlayerName = "ANDROID";
#endif
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
  }

  public static GameVars getInstance()
  {
    return singleton;
  }

  public void setPlayerName(string playerName)
  {
    mPlayerName = playerName;
  }

  public string getPlayerName()
  {
    return mPlayerName;
  }
  
  public GameObject getPlayer()
  {
    if (mPlayer == null)
    {
      mPlayer = GameObject.Find("Cop_Run01");
    }

    return mPlayer;
  }
}
