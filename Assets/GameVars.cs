using UnityEngine;
using System.Collections;

public class GameVars : MonoBehaviour 
{
  private static GameVars singleton;
  
  public Transform somePrefab;
  
  public float score;
  public float distance;
  public int orcKills = 0;
  public string player_name = "";
  public string debugMessage = "";

  void Awake()
  {
#if UNITY_WEBPLAYER
#elif UNITY_STANDALONE || UNITY_EDITOR_WIN
    player_name = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
#elif UNITY_ANDROID
    player_name = "ANDROID";
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
}