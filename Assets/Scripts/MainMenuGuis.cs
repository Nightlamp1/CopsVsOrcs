using UnityEngine;
using System.Collections;

public class MainMenuGuis : MonoBehaviour {
  private       float XPOS   = 0; // Set in OnGUI once.  Should be fixed...
  private       float YPOS   = 0; // Set in OnGUI once.  Should be fixed...

  private const float WIDTH  = 200;
  private const float HEIGHT = 50;

	public GUISkin customSkin;
	public Texture2D startGame;
	public Texture2D optionButton;
	public Texture2D creditsButton;

	// Use this for initialization
	void Awake () 
	{

	}
	
	// Update is called once per frame
	void OnGUI()
	{
    XPOS = Screen.width * 0.72f;
    YPOS = Screen.height * 0.55f;

	  if (GUI.Button (new Rect (XPOS, YPOS, WIDTH, HEIGHT), startGame, "")) 
    {
      if (GameVars.getInstance().getPlayerName() == "")
      {
        GameVars.getInstance().setPlayerName("not entered");
      }

			Application.LoadLevel(1);
		}

		GUI.Button (new Rect (XPOS, YPOS + 63, WIDTH, HEIGHT), optionButton ,"");
		GUI.Button (new Rect (XPOS, YPOS + 125, WIDTH, HEIGHT), creditsButton, "");

	}
}
