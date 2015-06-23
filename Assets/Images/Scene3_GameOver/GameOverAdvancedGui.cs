#pragma warning disable 0168 // variable declared but not used.

using UnityEngine;
using System.Collections;

public class GameOverAdvancedGui: MonoBehaviour {
	private const int INSERT_VERSION = 1;
	
	private float buttonWidth  = 0f;
	private float buttonHeight = 0f;
	private float ScoreButtonWidth  = 0f;
	private float ScoreButtonHeight = 0f;
	private float goLabelWidth  = 0f;
	private float goLabelHeight = 0f;
	private float backGroundWidth = 0f;
	private float backGroundHeight = 0f;
	private float labelWidth = 0f;
	private float labelHeight = 0f;
	public GUISkin gameOverSkin;
	public GUISkin labelSkin;
	
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
	public Texture2D GameOverLabel;
	public Texture2D GlobalButtonOn;//need off asset
	public Texture2D LocalButtonOn;//need off asset
	
	void flexibleSpaces(int num) {
		for (int i = 0; i < num; ++i) {
			GUILayout.FlexibleSpace();
		}
	}
	
	void Start () 
	{
	    buttonWidth = Screen.width * .2f;
		buttonHeight = Screen.height * .1f;
		ScoreButtonWidth = Screen.width * 0.1f;
		ScoreButtonHeight = Screen.height * 0.15f;
		goLabelWidth = Screen.width * 0.4f;
		goLabelHeight = Screen.height * 0.15f;
		labelWidth = 436 ;
		labelHeight = 94;
		backGroundWidth = Screen.width * 0.5f;
		backGroundHeight = Screen.height * 0.5f;
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
		GUI.skin.label.fontSize = 24;
		GUILayoutOption[] buttonOptions = {GUILayout.Width(buttonWidth), GUILayout.Height(buttonHeight)};
		GUILayoutOption[] BackgroundOptions = {GUILayout.Width(backGroundWidth), GUILayout.Height(backGroundHeight)};
		GUILayoutOption[] LabelOptions = {GUILayout.Width(labelWidth), GUILayout.Height(labelHeight)};
		GUILayoutOption[] goLabelOptions = {GUILayout.Width(goLabelWidth), GUILayout.Height(goLabelHeight)};
		GUILayoutOption[] scoreButtonOptions = {GUILayout.Width(ScoreButtonWidth), GUILayout.Height(ScoreButtonHeight)};
		
		GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height));

		GUILayout.BeginVertical ();
		flexibleSpaces (1);//vert distance from top of screen to buttons and gameoverlabel

		GUILayout.BeginHorizontal ();
		flexibleSpaces (1);//space from left side to retry button

		if (GUILayout.Button(Retry, buttonOptions))
		{
			GameVars.getInstance().setScore(0);
			GameVars.getInstance().setOrcKills(0);
			GameVars.getInstance().setDistance(0);
			Application.LoadLevel(1);
		}

		flexibleSpaces (1);//spacefrom retry button to game over label
		GUILayout.Box (GameOverLabel, goLabelOptions);
		flexibleSpaces (1);//space from gameover label to main menu button
		
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

		flexibleSpaces (1);//space from main menu to right edge
		GUILayout.EndHorizontal ();
		flexibleSpaces (2);//vert distance from gameover to score text prompt

		GUILayout.BeginHorizontal ();
		flexibleSpaces (1);
		GUI.skin = labelSkin;
		GUILayout.Label("Your score this round was " + score + "!",LabelOptions);
		GUI.skin = gameOverSkin;
		flexibleSpaces (1);
		GUILayout.EndHorizontal ();

		flexibleSpaces (2);//vert distance between score prompt and highscore board

		GUILayout.BeginHorizontal ();
		flexibleSpaces (1);
		GUILayout.Button (GlobalButtonOn, scoreButtonOptions);
		GUILayout.Label (scores,BackgroundOptions);
		GUILayout.Button (LocalButtonOn, scoreButtonOptions);
		flexibleSpaces (1);
		GUILayout.EndHorizontal ();

		flexibleSpaces (1);
		GUILayout.BeginHorizontal ();
		flexibleSpaces (2);
		GUILayout.Button (Twitter,GUIStyle.none);
		GUILayout.Button (Facebook,GUIStyle.none);
		flexibleSpaces (1);
		GUILayout.EndHorizontal ();

		GUILayout.EndVertical ();
		GUILayout.EndArea ();
		
	}
	
}
