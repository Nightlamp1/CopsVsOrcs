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
    GameVars.getInstance ().setGameSession (0);
  }

  void flexibleSpaces(int num) {
    for (int i = 0; i < num; ++i) {
      GUILayout.FlexibleSpace();
    }
  }
	
	// Update is called once per frame
	void OnGUI()
	{
#if UNITY_EDITOR
    if (GUI.Button(new Rect(0.25f * Screen.width, 0, 0.5f * Screen.width, 0.05f * Screen.height), "This big hideous button is for the debug menu.")) {
      SceneManager.LoadLevel(SceneManager.Scene.DEBUG);
    }
#endif

		if (AudioManager.getInstance ().musicSource.mute) {
			toggleTextureMute = muteTrue;
		} else if (!AudioManager.getInstance ().musicSource.mute) {
			toggleTextureMute = muteFalse;
		}
    int vSpaceBetweenButtons = 1;
    int hSpaceBeforeButton = 25;
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
      SceneManager.LoadLevel(SceneManager.Scene.ENDLESS_RUN);
    }

    flexibleSpaces(hSpaceAfterButton);
    GUILayout.EndHorizontal(); 
    flexibleSpaces(vSpaceBetweenButtons);

    GUILayout.BeginHorizontal();
    flexibleSpaces(hSpaceBeforeButton);
    
    if (GUILayout.Button(creditsButton, buttonStyle, buttonOptions)) {
      SceneManager.LoadLevel(SceneManager.Scene.CREDITS);
    }

    flexibleSpaces(hSpaceAfterButton);
    GUILayout.EndHorizontal();

	GUILayout.BeginHorizontal();
	flexibleSpaces(10);

	GUILayout.BeginVertical ();
	flexibleSpaces (2);
	/*
	 * The following code will set the correct label string/color based on the bool from getValidUsername()
	 * if nameCheck is false we assume the name to be valid (no curse words) and the GUI shows no change
	 * if nameCheck is true we assume an invalid name. At this point the text string "Enter Name here:"
	 * will change to "NAME IS INVALID" and font color/label color will turn to red.
	 * All that is left is your logic to toggle the nameCheck variable!!!
	 */
  if (GameVars.getInstance().getPlayerName() == "") {
    GUILayout.Label ("Enter a Name to Compete Globally:");
  } else if (GameVars.getInstance().getValidUsername()) {
		GUI.color = Color.white;
		GUILayout.Label ("Enter Name Here:");
	} else {
		GUI.color = Color.red;
		GUILayout.Label ("NAME IS INVALID");
	}

	GameVars.getInstance().setPlayerName(
	GUILayout.TextField(GameVars.getInstance().getPlayerName(), textOptions));
	GUILayout.EndVertical ();
		
	flexibleSpaces(1);
		GUI.color = Color.white;//used to reset the color to white (clear) after our name input gui element.
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
