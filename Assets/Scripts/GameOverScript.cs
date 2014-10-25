using UnityEngine;
using System.Collections;

public class GameOverScript : MonoBehaviour {

	float score = 0;
  string scores;
  WWW down_query;
  WWW up_query;

  bool up_handled;
  bool down_handled;

	//int convertedScore = 0;
	// Use this for initialization
	void Start () 
	{
    up_handled = false;
    scores = "";
    score = GameVars.getInstance().score * 10;

    up_query = new WWW("https://www.copsvsorcs.com/insert_high_score.php?id=test&score=" + score);
	}

	void OnGUI()
	{
		GUI.Label (new Rect (Screen.width * 0.5f - 40, 50, 80, 30), "GAME OVER");

		GUI.Label (new Rect (Screen.width * 0.5f - 30, 300, 80, 30), "Score: " + Mathf.Round(score));

		if (GUI.Button (new Rect (Screen.width * 0.5f - 300, 350, 600, 300), "Retry?"))
		{
      GameVars.getInstance().score = 0;
			GameVars.getInstance().orcKills = 0;
			GameVars.getInstance().distance = 0;
			Application.LoadLevel(1);
		}

    if (up_query.isDone && !up_handled)
    {
      down_query = new WWW("https://www.copsvsorcs.com/select_high_score.php");
      up_handled = true;
    }

    if (!(down_query == null) && down_query.isDone && !down_handled)
    {
      scores = down_query.text;
      down_handled = true;
    }

    if (down_handled)
    {
      GUI.Label (new Rect (Screen.width * 0.5f - 150, 450, 300, 150), scores);
    }
	}

}
