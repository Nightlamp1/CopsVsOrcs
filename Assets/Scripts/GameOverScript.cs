using UnityEngine;
using System.Collections;

public class GameOverScript : MonoBehaviour {

	float score = 0;
	//int convertedScore = 0;
	// Use this for initialization
	void Start () 
	{
    score = GameVars.getInstance().score * 10;
	}

	void OnGUI()
	{
		GUI.Label (new Rect (Screen.width * 0.5f - 40, 50, 80, 30), "GAME OVER");

		GUI.Label (new Rect (Screen.width * 0.5f - 30, 300, 80, 30), "Score: " + Mathf.Round(score));

		if (GUI.Button (new Rect (Screen.width * 0.5f - 30, 350, 60, 30), "Retry?"))
		{
      GameVars.getInstance().score = 0;
			Application.LoadLevel(1);
		}
	}

}
