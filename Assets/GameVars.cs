using UnityEngine;
using System.Collections;

public class GameVars : MonoBehaviour 
{
  private static GameVars singleton;
  
  public Transform somePrefab;
  
  public float score;
  public float distance;
  public int orcKills = 0;

  void Awake()
  {
    if(singleton != null && singleton != this)
    {
      Destroy(gameObject);
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