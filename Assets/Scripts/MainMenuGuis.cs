using UnityEngine;
using System.Collections;

public class MainMenuGuis : MonoBehaviour {
  private       float XPOS   = 0; // Set in OnGUI once.  Should be fixed...
  private       float YPOS   = 0; // Set in OnGUI once.  Should be fixed...

  private const float WIDTH  = 185;
  private const float HEIGHT = 50;

	public GUISkin customSkin;
	public Texture2D startGame;
	public Texture2D optionButton;
	public Texture2D creditsButton;

	// Use this for initialization
	void Awake () 
	{

	}

  void Start() {
  }
	
	// Update is called once per frame
	void OnGUI()
	{
    XPOS = Screen.width * 0.665f;
    YPOS = Screen.height * 0.52f;

    GameVars.getInstance().setPlayerName(GUI.TextField(new Rect(XPOS, YPOS-73, WIDTH, HEIGHT),
          GameVars.getInstance().getPlayerName()));

	  if (GUI.Button (new Rect (XPOS, YPOS, WIDTH, HEIGHT), startGame, "")) 
    {
      print("========== {1} " + System.DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff") + " ==========");

      if (GameVars.getInstance().getPlayerName() == "")
      {
        GameVars.getInstance().setPlayerName("not entered");
      }

      print("========== {2} " + System.DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff") + " ==========");

			Application.LoadLevel(1);
		}

		GUI.Button (new Rect (XPOS, YPOS + 73, WIDTH, HEIGHT), optionButton ,"");
		if (GUI.Button (new Rect (XPOS, YPOS + 145, WIDTH, HEIGHT), creditsButton, "")) {
			Application.LoadLevel(4);
		}

	}
}
