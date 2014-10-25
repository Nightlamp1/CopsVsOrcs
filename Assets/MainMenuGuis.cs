using UnityEngine;
using System.Collections;

public class MainMenuGuis : MonoBehaviour {

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
		GUI.skin = customSkin;

		GUI.skin.button.normal.background = (Texture2D)start.image;
		GUI.skin.button.hover.background = (Texture2D)start.image;
		GUI.skin.button.active.background = (Texture2D)start.image;
		GUI.skin.button.alignment = TextAnchor.MiddleCenter;

    GUI.Label (new Rect(Screen.width * 0.34f, Screen.height * 0.5f - 126, 300, 100), "Name");
    GameVars.getInstance().player_name = GUI.TextField (
      new Rect(Screen.width * 0.34f, Screen.height * 0.5f - 63, 300, 100), GameVars.getInstance().player_name);

		if (GUI.Button (new Rect (Screen.width * 0.34f, Screen.height * 0.5f, 300, 100), "Start")) 
    {
      if (GameVars.getInstance().player_name == "")
      {
        GameVars.getInstance().player_name = "not entered";
      }

			Application.LoadLevel(1);
		}

		GUI.Button (new Rect (Screen.width * 0.34f, (Screen.height * 0.5f) + 63, 300, 100), "Options");
		GUI.Button (new Rect (Screen.width * 0.34f, (Screen.height * 0.5f) + 125, 300, 100), "Credits");

	}
}
