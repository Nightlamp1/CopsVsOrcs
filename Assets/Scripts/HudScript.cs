using UnityEngine;
using System.Collections;

public class HudScript : MonoBehaviour {
	public Font CVOFont;

	// Update is called once per frame
	void Update () 
	{
    GameVars.getInstance().score += Time.deltaTime;
	  GameVars.getInstance ().distance += Time.deltaTime;
	}

	public void IncreaseScore (int amount)
	{
    GameVars.getInstance().score += amount;
	}

	void OnGUI()
	{
		GUI.color = Color.white;
		GUI.skin.font = CVOFont;
    GUI.Label (new Rect (Screen.width * 0.45f, Screen.height * 0.01f, 200, 50), 
               "Score: " + (int)(GameVars.getInstance().score * 10));
	
		GUI.Label (new Rect (Screen.width * 0.01f, Screen.height * 0.01f, 200, 50), "Distance: " + (int)(GameVars.getInstance().distance) + "m");

		GUI.Label (new Rect (Screen.width * 0.76f, Screen.height * 0.01f, 200, 50), "Orcs Destroyed: " + (int)(GameVars.getInstance().orcKills));
	
    /*if (GameVars.getInstance().debugMessage != "")
    {
      GUI.Label (new Rect(Screen.width * 0.5f, Screen.height * 0.13f, 500, 200), GameVars.getInstance().debugMessage);
    }*/
	}
}