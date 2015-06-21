#pragma warning disable 0168 // variable declared but not used.

using UnityEngine;
using System.Collections;

public class GameOverScript : MonoBehaviour {
  private const int INSERT_VERSION = 1;

  private float buttonWidth  = 0f;
  private float buttonHeight = 0f;

  private float topSize      = 0.4f;

	float score = 0;
  string scores;

  WWW up_query;

  bool up_handled;

  public Texture2D MainMenu;
  public Texture2D Retry;
  
  void flexibleSpaces(int num) {
    for (int i = 0; i < num; ++i) {
      GUILayout.FlexibleSpace();
    }
  }

	void Start () 
	{
    buttonWidth = Screen.width * .15f;
    buttonHeight = Screen.height * .075f;

    up_handled = false;

    scores = "";

    score = Mathf.Round(GameVars.getInstance().getScore());

    up_query = new WWW("https://www.copsvsorcs.com/insert_high_score.php" +
                       "?id=" + WWW.EscapeURL(GameVars.getInstance().getPlayerName()) + 
                       "&score=" + WWW.EscapeURL(score.ToString()) +
                       "&version=" + WWW.EscapeURL(INSERT_VERSION.ToString()));

    GameObject.Find ("HeroCop(Clone)").transform.position = new Vector3 (-6f, -3.4f, 0f);
    GameVars.getInstance().setUserHasStarted(false);
  }

	void OnGUI()
	{
    if (!up_handled && up_query != null && up_query.isDone)
    {
      up_handled = true;
      scores = up_query.text;
    }

    GUIStyle buttonStyle = new GUIStyle();

    GUILayoutOption[] buttonOptions = {GUILayout.Width(buttonWidth), GUILayout.Height(buttonHeight)};

    GUILayout.BeginArea(new Rect(0, 0, Screen.width, topSize * Screen.height));

    GUILayout.BeginVertical();
    flexibleSpaces(20);
    GUILayout.BeginHorizontal();
    flexibleSpaces(1);

    GUILayout.Label("Your score this round was " + score + "!", GUI.skin.label);

    flexibleSpaces(1);
    GUILayout.EndHorizontal();

    flexibleSpaces(1);

    GUILayout.BeginHorizontal();
    flexibleSpaces(4);

		if (GUILayout.Button(Retry, buttonStyle, buttonOptions))
		{
      GameVars.getInstance().setScore(0);
			GameVars.getInstance().setOrcKills(0);
			GameVars.getInstance().setDistance(0);
			Application.LoadLevel(1);
		}

    flexibleSpaces(1);

		if (GUILayout.Button(MainMenu, buttonStyle, buttonOptions))
		{
			GameVars.getInstance().setScore(0);
			GameVars.getInstance().setOrcKills(0);
			GameVars.getInstance().setDistance(0);
			Application.LoadLevel(0);

      try {
        gameObject.GetComponent<GameOverInterstitialAd>().hideBannerAd();
      } catch (System.Exception ex) {
        print ("Exception trying to hide banner ad.");
      }
		}

    flexibleSpaces(4);

    GUILayout.EndHorizontal();

    GUILayout.BeginHorizontal();
    flexibleSpaces(4);

    flexibleSpaces(4);
    GUILayout.EndHorizontal();

    GUILayout.EndVertical();
    
    GUILayout.EndArea();

    GUILayout.BeginArea(new Rect(0, topSize * Screen.height, Screen.width, (1 - topSize) * Screen.height));

    flexibleSpaces(1);

    GUILayout.BeginHorizontal();
    flexibleSpaces(1);

    GUILayout.Label(scores);

    flexibleSpaces(1);
    GUILayout.EndHorizontal();

    flexibleSpaces(1);

    GUILayout.EndArea();
	}

}
