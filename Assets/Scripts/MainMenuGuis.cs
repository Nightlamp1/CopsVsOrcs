using UnityEngine;
using System.Collections;

public class MainMenuGuis : MonoBehaviour {
  private       float XPOS   = 0; // Set in OnGUI once.  Should be fixed...
  private       float YPOS   = 0; // Set in OnGUI once.  Should be fixed...

  private const float WIDTH  = 300;
  private const float HEIGHT = 100;

	public GUISkin customSkin;
	public Texture2D startGame;
	GUIContent start = new GUIContent();
	string startText = "Start Game";

	// Use this for initialization
	void Awake () 
	{
		start.image = (Texture2D)startGame;
		start.text = startText;
	}
	
	// Update is called once per frame
	void OnGUI()
	{
    XPOS = Screen.width * 0.34f;
    YPOS = Screen.height * 0.5f;

		GUI.skin = customSkin;

		GUI.skin.button.normal.background = (Texture2D)start.image;
		GUI.skin.button.hover.background = (Texture2D)start.image;
		GUI.skin.button.active.background = (Texture2D)start.image;
		GUI.skin.button.alignment = TextAnchor.MiddleCenter;

    GUI.Label(new Rect(XPOS, YPOS - 126, WIDTH, HEIGHT), "Name");
    GameVars inst = GameVars.getInstance();

    if (inst != null)
    {
      inst.setPlayerName(GUI.TextField(new Rect(XPOS, YPOS - 63, WIDTH, HEIGHT), inst.getPlayerName()));
    }
    else
    {
      GUI.TextField(new Rect(XPOS, YPOS - 63, WIDTH, HEIGHT), "There may be an issue using the GameVars script...");
    }

		if (GUI.Button (new Rect (XPOS, YPOS, WIDTH, HEIGHT), "Start")) 
    {
      if (GameVars.getInstance().getPlayerName() == "")
      {
        GameVars.getInstance().setPlayerName("not entered");
      }

			Application.LoadLevel(1);
		}

		GUI.Button (new Rect (XPOS, YPOS + 63, WIDTH, HEIGHT), "Options");
		GUI.Button (new Rect (XPOS, YPOS + 125, WIDTH, HEIGHT), "Credits");

	}
}
