#pragma warning disable 0168 // variable declared but not used.

using UnityEngine;
using System.Collections;

public class GameOverAdvancedGui: MonoBehaviour {
	private const int INSERT_VERSION = 1;
	const string TwitterShare = "http://twitter.com/intent/tweet";
	const string FacebookID = "648150285304327";
	const string FacebookShare = "http://www.facebook.com/dialog/feed";
	
	private float buttonWidth  = 0f;
	private float buttonHeight = 0f;
	private float rateWidth  = 0f;
	private float rateHeight = 0f;
	private float ScoreButtonWidth  = 0f;
	private float ScoreButtonHeight = 0f;
	private float ShareButtonWidth  = 0f;
	private float ShareButtonHeight = 0f;
	private float goLabelWidth  = 0f;
	private float goLabelHeight = 0f;
	private float backGroundWidth = 0f;
	private float backGroundHeight = 0f;
	private float labelWidth = 0f;
	private float labelHeight = 0f;
	public GUISkin gameOverSkin;
	public GUISkin labelSkin;
	public GUISkin labelSkinOverlay;

  private HighScoreboard highScoreboard;
	
	private float topSize      = 0.35f;
	
	float score = 0;
	string scores;
	
	WWW up_query;
	
	bool up_handled;
	
	public Texture2D MainMenu;
	public Texture2D Retry;
	public Texture2D RateUs;
	public Texture2D HighScoreBG;
	public Texture2D DonutMasters;
	public Texture2D Twitter;
	public Texture2D Facebook;
	public Texture2D GameOverLabel;
	public Texture2D GlobalButtonOn;
	public Texture2D LocalButtonOn;
	public Texture2D GlobalButtonOff;
	public Texture2D LocalButtonOff;
	private Texture2D GlobalButtonToggle;
	private Texture2D LocalButtonToggle;
	
	void flexibleSpaces(int num) {
		for (int i = 0; i < num; ++i) {
			GUILayout.FlexibleSpace();
		}
	}
	
	void Start () 
	{
	    buttonWidth = Screen.width * .2f;
		buttonHeight = Screen.height * .1f;
		rateWidth = Screen.width * .2f;
		rateHeight = Screen.height * .1f;
		ScoreButtonWidth = Screen.width * 0.15f;
		ScoreButtonHeight = Screen.height * 0.15f;
		ShareButtonWidth = Screen.width * 0.06f;
		ShareButtonHeight = Screen.height * 0.1f;
		goLabelWidth = Screen.width * 0.25f;
		goLabelHeight = Screen.height * 0.15f;
		labelWidth = Screen.width * 0.5f;
		labelHeight = 94;
		backGroundWidth = Screen.width * 0.4f;
		backGroundHeight = Screen.height * 0.5f;
		up_handled = false;
		
		scores = "";
		
		score = Mathf.Round(GameVars.getInstance().getScore());

    HighScoreEntry highScoreEntry = new HighScoreEntry();

    highScoreEntry.playerName   = GameVars.getInstance().getPlayerName();
    highScoreEntry.scoreUuid    = System.Guid.NewGuid();
    highScoreEntry.score        = (int) score;
    highScoreEntry.scoreVersion = 1;
    highScoreEntry.appVersion   = GameVars.VERSION_NUMBER;

    HighScores.getInstance().localScores.addHighScoreEntry(highScoreEntry);

    if (GameVars.getInstance().getGlobalHighScoresSelected()) {
      highScoreboard = HighScores.getInstance().globalScores;
      GlobalButtonToggle = GlobalButtonOn;
      LocalButtonToggle = LocalButtonOff;
    } else {
      highScoreboard = HighScores.getInstance().localScores;
      GlobalButtonToggle = GlobalButtonOff;
      LocalButtonToggle = LocalButtonOn;
    }
		
    if (GameVars.getInstance().getValidUsername()) {
  		up_query = new WWW("https://www.copsvsorcs.com/insert_high_score.php" +
  		                   "?id=" + WWW.EscapeURL(GameVars.getInstance().getPlayerName()) + 
  		                   "&score=" + WWW.EscapeURL(score.ToString()) +
  		                   "&version=" + WWW.EscapeURL(INSERT_VERSION.ToString()));
    }
		
		GameObject.Find ("HeroCop(Clone)").transform.position = new Vector3 (-6f, -3.4f, 0f);
		GameVars.getInstance().setUserHasStarted(false);
	}
	
	void OnGUI()
	{
		if (!up_handled && up_query != null && up_query.isDone)
		{
			up_handled = true;
			HighScores.getInstance().LoadGlobalScoresResponseString(up_query.text);
		}
		
		GUIStyle buttonStyle = new GUIStyle();
		GUI.skin = gameOverSkin;
		GUI.skin.label.fontSize = (int) (Screen.height*0.038f);
		GUILayoutOption[] buttonOptions = {GUILayout.Width(buttonWidth), GUILayout.Height(buttonHeight)};
		GUILayoutOption[] rateOptions = {GUILayout.Width(rateWidth), GUILayout.Height(rateHeight)};
		GUILayoutOption[] BackgroundOptions = {GUILayout.Width(backGroundWidth), GUILayout.Height(backGroundHeight)};
		GUILayoutOption[] LabelOptions = {GUILayout.Width(labelWidth), GUILayout.Height(labelHeight)};
		GUILayoutOption[] goLabelOptions = {GUILayout.Width(goLabelWidth), GUILayout.Height(goLabelHeight)};
		GUILayoutOption[] scoreButtonOptions = {GUILayout.Width(ScoreButtonWidth), GUILayout.Height(ScoreButtonHeight)};
		GUILayoutOption[] shareButtonOptions = {GUILayout.Width(ShareButtonWidth), GUILayout.Height(ShareButtonHeight)};
		
		GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height));

		GUILayout.BeginVertical ();
		flexibleSpaces (1);//vert distance from top of screen to buttons and gameoverlabel

		GUILayout.BeginHorizontal ();
		flexibleSpaces (1);//space from left side to retry button

		if (GUILayout.Button(Retry, buttonOptions))
		{
			GameVars.getInstance().setOrcKills(0);
			GameVars.getInstance().setDistance(0);
			GameVars.getInstance().setOrcHits(0);
			SceneManager.LoadLevel(GameVars.ENDLESS_RUN_SCENE);
		}

		flexibleSpaces (1);//spacefrom retry button to game over label
		GUILayout.Box (GameOverLabel, goLabelOptions);
		flexibleSpaces (1);//space from gameover label to main menu button
		
		if (GUILayout.Button(MainMenu, buttonOptions))
		{
			GameVars.getInstance().setOrcKills(0);
			GameVars.getInstance().setDistance(0);
			GameVars.getInstance().setOrcHits(0);
			SceneManager.LoadLevel(GameVars.MAIN_MENU_SCENE);
		}

		flexibleSpaces (1);//space from main menu to right edge
		GUILayout.EndHorizontal ();
		flexibleSpaces (1);//vert distance from gameover to score text prompt

		GUILayout.BeginHorizontal ();
		flexibleSpaces (1);
		GUI.skin = labelSkin;
		GUI.skin.label.fontSize = (int) (Screen.height*0.04f);
		GUILayout.Label("Your score this round was $" + score + "!", LabelOptions);
		GUI.skin = gameOverSkin;
		flexibleSpaces (1);
		GUILayout.EndHorizontal ();

		flexibleSpaces (20);//vert distance between score prompt bottom of screen


		GUILayout.BeginArea (new Rect (0, 0, Screen.width, Screen.height));
		GUILayout.BeginVertical ();
		flexibleSpaces (7);
		GUILayout.BeginHorizontal ();
		flexibleSpaces (1);
		GUILayout.Label (highScoreboard.getNames(), BackgroundOptions);
        flexibleSpaces (1);
		GUILayout.EndHorizontal ();
		flexibleSpaces (3);
		GUILayout.EndVertical ();
		GUILayout.EndArea ();
		GUILayout.BeginArea (new Rect (0, 0, Screen.width, Screen.height));
		GUILayout.BeginVertical ();
		flexibleSpaces (7);
		GUILayout.BeginHorizontal ();
		flexibleSpaces (1);
		GUI.skin = labelSkinOverlay;
    GUI.skin.label.fontSize = (int) (Screen.height*0.038f);
		GUILayout.Label (highScoreboard.getScores(), BackgroundOptions);
		GUI.skin = gameOverSkin;
		flexibleSpaces (1);
		GUILayout.EndHorizontal ();
		flexibleSpaces (3);
		GUILayout.EndVertical ();
		GUILayout.EndArea ();

		GUILayout.BeginArea (new Rect (0, 0, Screen.width, Screen.height));
		GUILayout.BeginVertical ();
		flexibleSpaces (2);
		GUILayout.BeginHorizontal ();
		flexibleSpaces (1);

		if (GUILayout.Button (GlobalButtonToggle, scoreButtonOptions)) {
			GlobalButtonToggle = GlobalButtonOn;
			LocalButtonToggle = LocalButtonOff;
      GameVars.getInstance().setGlobalHighScoresSelected(true);
      highScoreboard = HighScores.getInstance().globalScores;
    }
    
		GUILayout.Box (DonutMasters, goLabelOptions);

		if (GUILayout.Button (LocalButtonToggle, scoreButtonOptions)) {
			GlobalButtonToggle = GlobalButtonOff;
			LocalButtonToggle = LocalButtonOn;
      GameVars.getInstance().setGlobalHighScoresSelected(false);
      highScoreboard = HighScores.getInstance().localScores;
    }

		flexibleSpaces (1);
		GUILayout.EndHorizontal ();
		flexibleSpaces (3);
		GUILayout.EndVertical ();
		GUILayout.EndArea ();
	
		GUILayout.BeginArea (new Rect (0, 0, Screen.width, Screen.height));
		GUILayout.BeginVertical ();
		flexibleSpaces (20);
		GUILayout.BeginHorizontal ();
		flexibleSpaces (4);
		if (GUILayout.Button (Twitter, GUIStyle.none, shareButtonOptions)) {
			Application.OpenURL(TwitterShare + "?text=" + WWW.EscapeURL("@Delirium_GW, I scored " + score + " on #CopsVsOrcs!") 
			                    + "&url=" + WWW.EscapeURL("https://www.facebook.com/deliriumgameworks?fref=ts") 
			                    + "&related=" + WWW.EscapeURL("https://www.twitter.com/Delirium_GW") 
			                    + "&lang=" + WWW.EscapeURL("en"));
		}
		if (GUILayout.Button (Facebook, GUIStyle.none, shareButtonOptions)) {
			Application.OpenURL(FacebookShare + 
			                    "?app_id=" + FacebookID +
			                    "&link=" + WWW.EscapeURL("https://www.facebook.com/deliriumgameworks?fref=ts")+
			                    "&picture=" + WWW.EscapeURL("https://pbs.twimg.com/profile_images/602945529169883136/j21GJ7G4.jpg")+
			                    "&name=" + WWW.EscapeURL("Cops vs Orcs!!!") +
			                    "&caption=" + WWW.EscapeURL("Check out my new HighScore on CopsVsOrcs") +
			                    "&description=" + WWW.EscapeURL("@deliriumgameworks, My new Cops vs Orcs score is " + score) +
			                    "&redirect_uri=" + WWW.EscapeURL("https://www.deliriumgameworks.com/"));
		}
		GUI.skin = gameOverSkin;
		flexibleSpaces (2);
		GUILayout.EndHorizontal ();
		flexibleSpaces (1);
		GUILayout.EndVertical ();
		GUILayout.EndArea ();

		flexibleSpaces (1);
		if (GUILayout.Button (RateUs, rateOptions)) {
#if UNITY_ANDROID
			Application.OpenURL("market://details?id=com.deliriumgw.copsvsorcs");
#elif UNITY_IPHONE
			Application.OpenURL("itms-apps://itunes.apple.com/app/id1004888424");
#endif
		}
		flexibleSpaces (1);
		GUILayout.EndVertical ();
		GUILayout.EndArea ();
		
	}
	
}
