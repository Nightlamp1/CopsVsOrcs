#pragma warning disable 0168 // variable declared but not used.

using UnityEngine;
using System.Collections;

public class GameOverScript : MonoBehaviour {
	private       float XPOS   = 0; // Set in OnGUI once.  Should be fixed...
	private       float YPOS   = 0; // Set in OnGUI once.  Should be fixed...
	
	private const float WIDTH  = 200;
	private const float HEIGHT = 50;
	float score = 0;
  string scores;
  WWW down_query;
  WWW up_query;

  bool up_requested;
  bool down_requested;

  bool up_handled;
  bool down_handled;

  public Texture2D MainMenu;
  public Texture2D Retry;
  

	//int convertedScore = 0;
	// Use this for initialization
	void Start () 
	{
    up_requested = false;
    down_requested = false;

    up_handled = false;
    down_handled = false;

    scores = "";

    score = Mathf.Round(GameVars.getInstance().score);
  }

	void OnGUI()
	{
		XPOS = Screen.width * 0.5f;
		YPOS = Screen.height * 0.5f;

		GUI.Label (new Rect (Screen.width * 0.5f - 30, Screen.height * 0.5f - 50, 80, 30), "Score: " + score);

		if (GUI.Button (new Rect (XPOS + 53, YPOS - 105, WIDTH, HEIGHT), Retry, ""))
		{
      GameVars.getInstance().score = 0;
			GameVars.getInstance().orcKills = 0;
			GameVars.getInstance().distance = 0;
			Application.LoadLevel(1);
		}
		if (GUI.Button (new Rect (XPOS - 230, YPOS - 105, WIDTH, HEIGHT), MainMenu, ""))
		{
			GameVars.getInstance().score = 0;
			GameVars.getInstance().orcKills = 0;
			GameVars.getInstance().distance = 0;
			Application.LoadLevel(0);

      try {
        gameObject.GetComponent<GameOverInterstitialAd>().hideBannerAd();
      } catch (System.Exception ex) {
        print ("Exception trying to hide banner ad.");
      }
		}

    if (!up_handled && !up_requested)
    {
      up_query = new WWW("https://www.copsvsorcs.com/insert_high_score.php" +
                         "?id=" + WWW.EscapeURL(GameVars.getInstance().getPlayerName()) + 
                         "&score=" + WWW.EscapeURL(score.ToString()));

      Debug.Log ("Inserted score " + score + " for " + GameVars.getInstance().getPlayerName());

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
