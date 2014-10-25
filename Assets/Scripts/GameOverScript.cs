﻿using UnityEngine;
using System.Collections;

public class GameOverScript : MonoBehaviour {

	float score = 0;
  string scores;
  WWW down_query;
  WWW up_query;

  bool up_requested;
  bool down_requested;

  bool up_handled;
  bool down_handled;

	//int convertedScore = 0;
	// Use this for initialization
	void Start () 
	{
    up_requested = false;
    down_requested = false;

    up_handled = false;
    down_handled = false;

    scores = "";
    score = Mathf.Round(GameVars.getInstance().score * 10);
	}

	void OnGUI()
	{
		GUI.Label (new Rect (Screen.width * 0.5f - 40, 50, 80, 30), "GAME OVER");

		GUI.Label (new Rect (Screen.width * 0.5f - 30, 300, 80, 30), "Score: " + score);

		if (GUI.Button (new Rect (Screen.width * 0.5f - 300, 350, 600, 300), "Retry?"))
		{
      GameVars.getInstance().score = 0;
			GameVars.getInstance().orcKills = 0;
			GameVars.getInstance().distance = 0;
			Application.LoadLevel(1);
		}

    if (!up_handled && !up_requested)
    {
      up_query = new WWW("https://www.copsvsorcs.com/insert_high_score.php" +
                         "?id=" + WWW.EscapeURL(GameVars.getInstance().player_name) + 
                         "&score=" + WWW.EscapeURL(score.ToString()));

      Debug.Log ("Inserted score " + score + " for " + GameVars.getInstance().player_name);

      up_requested = true;
    }

    if (up_requested && !up_handled && !(up_query == null) && up_query.isDone)
    {
      up_handled = true;
      down_handled = false;

      down_query = new WWW("https://www.copsvsorcs.com/select_high_score.php");

      down_requested = true;
    }

    if (down_requested && !down_handled && !(down_query == null) && down_query.isDone)
    {
      down_handled = true;
      scores = down_query.text;
    }

    if (down_handled)
    {
      GUI.Label (new Rect (Screen.width * 0.5f - 150, 450, 300, 150), scores);
    }
	}

}
