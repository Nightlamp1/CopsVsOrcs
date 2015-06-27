using UnityEngine;
using System.Collections;

public class MainMenuGuis : MonoBehaviour {
  private const float WIDTH  = 185;
  private const float HEIGHT = 50;

  private float buttonWidth  = 0;
  private float buttonHeight = 0;
  private float muteWidth  = 0;
  private float muteHeight = 0;
  private float textWidth  = 0;
  private float textHeight = 0;

	public GUISkin customSkin;
	public Texture2D startGame;
	public Texture2D optionButton;
	public Texture2D creditsButton;
	public Texture2D muteFalse;
	public Texture2D muteTrue;
	private Texture2D toggleTextureMute;

	// Use this for initialization
	void Awake () 
	{

	}

  void Start() {
    buttonWidth = Screen.width * .3f;
    buttonHeight = Screen.height * .15f;
	muteWidth = Screen.width * .3f;
	muteHeight = Screen.height * .15f;
	textWidth = Screen.width * .3f;
	textHeight = Screen.height * .10f;
  }

  void flexibleSpaces(int num) {
    for (int i = 0; i < num; ++i) {
      GUILayout.FlexibleSpace();
    }
  }
	
	// Update is called once per frame
	void OnGUI()
	{
		if (AudioManager.getInstance ().musicSource.mute) {
			toggleTextureMute = muteTrue;
		} else if (!AudioManager.getInstance ().musicSource.mute) {
			toggleTextureMute = muteFalse;
		}
    int vSpaceBetweenButtons = 1;
    int hSpaceBeforeButton = 27;
    int hSpaceAfterButton  = 2;

    GUILayoutOption[] buttonOptions = {GUILayout.Width(buttonWidth), GUILayout.Height(buttonHeight)};
	GUILayoutOption[] textOptions = {GUILayout.Width(textWidth), GUILayout.Height(textHeight)};
	GUILayoutOption[] muteOptions = {GUILayout.Width(muteWidth), GUILayout.Height(muteHeight)};
    GUIStyle buttonStyle = new GUIStyle();
	GUI.skin = customSkin;
	GUI.skin.textField.fontSize = (int)(Screen.height * 0.05f);
	GUI.skin.label.fontSize = (int)(Screen.height * 0.05f);

    GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height));

    flexibleSpaces(600);

    GUILayout.BeginHorizontal();
    flexibleSpaces(hSpaceBeforeButton);

    if (GUILayout.Button(startGame, buttonStyle, buttonOptions)) {
      SceneManager.LoadLevel(GameVars.ENDLESS_RUN_SCENE);
    }

    flexibleSpaces(hSpaceAfterButton);
    GUILayout.EndHorizontal();
		    
    flexibleSpaces(vSpaceBetweenButtons);

    GUILayout.BeginHorizontal();
    flexibleSpaces(hSpaceBeforeButton);
    
    if (GUILayout.Button(creditsButton, buttonStyle, buttonOptions)) {
      SceneManager.LoadLevel(GameVars.CREDITS_SCENE);
    }

    flexibleSpaces(hSpaceAfterButton);
    GUILayout.EndHorizontal();

	GUILayout.BeginHorizontal();
	flexibleSpaces(10);

	GUILayout.BeginVertical ();
	flexibleSpaces (2);
	GUILayout.Label ("Enter Name Here:");

	GameVars.getInstance().setPlayerName(
	GUILayout.TextField(GameVars.getInstance().getPlayerName(), textOptions));
	GUILayout.EndVertical ();
		
	flexibleSpaces(1);
	if (GUILayout.Button (toggleTextureMute, muteOptions)) {
			AudioManager.getInstance().toggleMute();
		}
	GUILayout.EndHorizontal();
		
	flexibleSpaces(vSpaceBetweenButtons);
    
	flexibleSpaces(0);
	

    GUILayout.Label("v" + GameVars.VERSION_NUMBER);


    GUILayout.EndArea();
	}
}
