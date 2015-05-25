using UnityEngine;
using System.Collections;

public class HudScript : MonoBehaviour {
	public Font CVOFont;
	private int ScoreTen = 0;
  void Start ()
  {
  }

	// Update is called once per frame
	void Update () 
	{
    //GameVars.getInstance().score += Time.deltaTime;
	  //GameVars.getInstance ().distance += Time.deltaTime;
		if (Time.timeScale == 0)return;
		ScoreTen += 1;
		if (ScoreTen >= 100) {
			GameVars.getInstance().score += 0.1f;
			GameVars.getInstance ().distance += 1;
			ScoreTen = 0;
		}
	}

	public void IncreaseScore (int amount)
	{
    GameVars.getInstance().score += amount;
	}

	void OnGUI()
	{
		GUI.color = Color.white;
		GUI.skin.font = CVOFont;
    GUI.Label (new Rect (Screen.width * 0.5f, Screen.height * 0.05f, 100, 30), 
               "Score: " + (int)(GameVars.getInstance().score));
	
		GUI.Label (new Rect (Screen.width * 0.01f, Screen.height * 0.01f, 200, 50), "Distance: " + (int)(GameVars.getInstance().distance) + "m");

		GUI.Label (new Rect (Screen.width * 0.76f, Screen.height * 0.01f, 200, 50), "Orcs Destroyed: " + (int)(GameVars.getInstance().orcKills));
	}
}
