using UnityEngine;
using System.Collections;

public class MainMenuGuis : MonoBehaviour {
  private const float WIDTH  = 185;
  private const float HEIGHT = 50;

  private float buttonWidth  = 0;
  private float buttonHeight = 0;

	public GUISkin customSkin;
	public Texture2D startGame;
	public Texture2D optionButton;
	public Texture2D creditsButton;

	// Use this for initialization
	void Awake () 
	{

	}

  void Start() {
    buttonWidth = Screen.width * .3f;
    buttonHeight = Screen.height * .15f;
  }

  void flexibleSpaces(int num) {
    for (int i = 0; i < num; ++i) {
      GUILayout.FlexibleSpace();
    }
  }
	
	// Update is called once per frame
	void OnGUI()
	{
    int vSpaceBetweenButtons = 1;
    int hSpaceBeforeButton = 27;
    int hSpaceAfterButton  = 2;

    GUILayoutOption[] buttonOptions = {GUILayout.Width(buttonWidth), GUILayout.Height(buttonHeight)};
    GUIStyle textStyle   = GUI.skin.GetStyle("TextField");
    //GUIStyle buttonStyle = GUI.skin.GetStyle("Button");
    GUIStyle buttonStyle = new GUIStyle();

    //buttonStyle.fixedHeight = buttonHeight;
    //buttonStyle.fixedWidth = buttonWidth;
    textStyle.fontSize = 30;

    GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height));

    flexibleSpaces(600);

    GUILayout.BeginHorizontal();
    flexibleSpaces(hSpaceBeforeButton);

    if (GUILayout.Button(startGame, buttonStyle, buttonOptions)) {
      Application.LoadLevel(GameVars.ENDLESS_RUN_SCENE);
    }

    flexibleSpaces(hSpaceAfterButton);
    GUILayout.EndHorizontal();
		    
    flexibleSpaces(vSpaceBetweenButtons);

    GUILayout.BeginHorizontal();
    flexibleSpaces(hSpaceBeforeButton);
    
    if (GUILayout.Button(creditsButton, buttonStyle, buttonOptions)) {
      Application.LoadLevel(GameVars.CREDITS_SCENE);
    }

    flexibleSpaces(hSpaceAfterButton);
    GUILayout.EndHorizontal();

	GUILayout.BeginHorizontal();
	flexibleSpaces(2);
		
	GameVars.getInstance().setPlayerName(
	GUILayout.TextField(GameVars.getInstance().getPlayerName(), textStyle, buttonOptions));
		
	flexibleSpaces(hSpaceAfterButton);
	GUILayout.EndHorizontal();
		
	flexibleSpaces(vSpaceBetweenButtons);
    
	flexibleSpaces(0);

    GUILayout.Label("v" + GameVars.VERSION_NUMBER);

    GUILayout.EndArea();
	}
}
