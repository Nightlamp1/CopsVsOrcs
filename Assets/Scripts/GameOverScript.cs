#pragma warning disable 0168 // variable declared but not used.

using UnityEngine;
using System.Collections;

public class GameOverScript : MonoBehaviour {
  private const int INSERT_VERSION = 1;

  private float buttonWidth  = 0f;
  private float buttonHeight = 0f;
	private float backGroundWidth = 0f;
	private float backGroundHeight = 0f;
	private float labelWidth = 0f;
	private float labelHeight = 0f;
	public GUISkin gameOverSkin;

  private float topSize      = 0.35f;

	float score = 0;
  string scores;

  WWW up_query;

  bool up_handled;

  public Texture2D MainMenu;
  public Texture2D Retry;
  public Texture2D HighScoreBG;
  public Texture2D DonutMasters;
	public Texture2D Twitter;
	public Texture2D Facebook;
  
  void flexibleSpaces(int num) {
    for (int i = 0; i < num; ++i) {
      GUILayout.FlexibleSpace();
    }
  }

	void Start () 
	{
    buttonWidth = Screen.width * .2f;
    buttonHeight = Screen.height * .1f;
	labelWidth = 436 ;
	labelHeight = 94;
	backGroundWidth = Screen.width * 0.9f;
	backGroundHeight = Screen.height * 0.9f;
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
		GUI.skin = gameOverSkin;
	GUI.skin.label.fontSize = 32;
    GUILayoutOption[] buttonOptions = {GUILayout.Width(buttonWidth), GUILayout.Height(buttonHeight)};
	GUILayoutOption[] BackgroundOptions = {GUILayout.Width(backGroundWidth), GUILayout.Height(backGroundHeight)};
	GUILayoutOption[] LabelOptions = {GUILayout.Width(labelWidth), GUILayout.Height(labelHeight)};

    GUILayout.BeginArea(new Rect(0, 0, Screen.width, 0.3f * Screen.height));

    GUILayout.BeginVertical();
    flexibleSpaces(1);
    GUILayout.BeginHorizontal();
    flexibleSpaces(1);

    GUILayout.Label("Your score this round was " + score + "!", GUI.skin.label);

    flexibleSpaces(1);
    GUILayout.EndHorizontal();
	GUILayout.EndArea();

	GUILayout.BeginArea(new Rect(0, Screen.height * 0.05f, Screen.width, topSize * Screen.height));
    GUILayout.BeginHorizontal();
    flexibleSpaces(1);

		if (GUILayout.Button(Retry, buttonOptions))
		{
      GameVars.getInstance().setScore(0);
			GameVars.getInstance().setOrcKills(0);
			GameVars.getInstance().setDistance(0);
			Application.LoadLevel(1);
		}

    flexibleSpaces(10);

		if (GUILayout.Button(MainMenu, buttonOptions))
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

    flexibleSpaces(1);

    GUILayout.EndHorizontal();

		flexibleSpaces (1);

    GUILayout.EndVertical();
    
    GUILayout.EndArea();

	GUILayout.BeginArea (new Rect (Screen.width*0.1f, topSize * Screen.height, Screen.width,Screen.height));
		GUILayout.BeginHorizontal ();
		//flexibleSpaces (1);
		GUILayout.Box (HighScoreBG,BackgroundOptions);
		//flexibleSpaces (1);
		GUILayout.EndHorizontal();
	GUILayout.EndArea ();

		GUILayout.BeginArea (new Rect (Screen.width/2-(218), 0.3f * Screen.height, Screen.width,Screen.height));
		GUILayout.BeginHorizontal ();
		GUILayout.Label (DonutMasters,LabelOptions);
		GUILayout.EndHorizontal();
		GUILayout.EndArea ();

    GUILayout.BeginArea(new Rect(0, (topSize+0.09f) * Screen.height, Screen.width, (1 - topSize) * Screen.height));

    GUILayout.BeginHorizontal();
    flexibleSpaces(1);

    GUILayout.Label(scores);

    flexibleSpaces(1);
    GUILayout.EndHorizontal();

    GUILayout.EndArea(); 

	GUILayout.BeginArea(new Rect(Screen.width * 0.7f, Screen.height * 0.85f, 200, 100));
		GUILayout.BeginHorizontal ();
		GUILayout.Button (Twitter,GUIStyle.none);
		flexibleSpaces (1);
		GUILayout.Button(Facebook,GUIStyle.none);
		GUILayout.EndHorizontal ();

	GUILayout.EndArea ();

	}

}
