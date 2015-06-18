using UnityEngine;
using System.Collections;

public class HudScript : MonoBehaviour {
	public Font CVOFont;
	private int ScoreTen = 0;

	public Texture2D tutorialOverlay;
  
  void Start ()
  {
  }

	// Update is called once per frame
	void Update () 
	{
		if (!GameVars.getInstance().getUserHasStarted()) {
      return;
    }

		ScoreTen += 1;
		if (ScoreTen >= 100) {
			GameVars.getInstance().incrementScore(0.1f);
			ScoreTen = 0;
		}
	}

	public void IncreaseScore (int amount)
	{
    GameVars.getInstance().incrementScore((float)amount);
	}

	void OnGUI()
	{
    if (GameVars.getInstance().getUserHasStarted()) {
  		GUI.color = Color.white;
	  	GUI.skin.font = CVOFont;
      GUI.Label (new Rect (Screen.width * 0.5f, Screen.height * 0.05f, 100, 30), 
                  "Score: " + (int) GameVars.getInstance().getScore());
	
  		GUI.Label (new Rect (Screen.width * 0.01f, Screen.height * 0.01f, 200, 50), "Distance: " + (int)(GameVars.getInstance().distance) + "m");

	  	GUI.Label (new Rect (Screen.width * 0.76f, Screen.height * 0.01f, 200, 50), "Orcs Destroyed: " + (int)(GameVars.getInstance().orcKills));

      if (!GameVars.getInstance().getUserHasStarted()) {
        GUI.Label (new Rect (Screen.width * 0.35f , Screen.height * 0.83f, 300, 50), "Please tap the screen to start.");
      }
    } else {
      GUI.Box(new Rect(0, 0, Screen.width, Screen.height), tutorialOverlay);
    }
	}
}
