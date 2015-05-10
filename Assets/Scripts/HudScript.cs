using UnityEngine;
using System.Collections;

public class HudScript : MonoBehaviour {
	private int ScoreTen = 0;
  void Start ()
  {
  }

	// Update is called once per frame
	void Update () 
	{
		ScoreTen += 1;
		if (ScoreTen >= 100) {
			GameVars.getInstance().score += 10;
			GameVars.getInstance ().distance += 1;
			ScoreTen = 0;
		}

    //GameVars.getInstance().score += Time.deltaTime;
	//GameVars.getInstance ().distance += Time.deltaTime;
	}

	public void IncreaseScore (int amount)
	{
    GameVars.getInstance().score += amount;
	}

	void OnGUI()
	{
    GUI.Label (new Rect (Screen.width * 0.5f, Screen.height * 0.05f, 100, 30), 
               "Score: " + (int)(GameVars.getInstance().score));
	
		GUI.Label (new Rect (Screen.width * 0.5f, Screen.height * 0.07f, 100, 30), "Distance: " + (int)(GameVars.getInstance().distance) + "m");

		GUI.Label (new Rect (Screen.width * 0.5f, Screen.height * 0.09f, 150, 30), "Orcs Destroyed: " + (int)(GameVars.getInstance().orcKills));
	
    if (GameVars.getInstance().debugMessage != "")
    {
      GUI.Label (new Rect(Screen.width * 0.5f, Screen.height * 0.13f, 500, 200), GameVars.getInstance().debugMessage);
    }
	}
}