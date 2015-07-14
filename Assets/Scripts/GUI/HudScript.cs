using UnityEngine;
using System.Collections;

public class HudScript : MonoBehaviour {
	public Font CVOFont;
	private int ScoreTen = 0;
	public Texture2D roadIcon;
	public Texture2D moneyIcon;
	public Texture2D scoreMultiplier;
	public Texture2D multiplierOne;
	public Texture2D multiplierTwo;
	public Texture2D multiplierThree;
	public Texture2D multiplierFour;
	public Texture2D multiplierFive;
	private float boxWidth = 0;
	private float boxHeight = 0;
	public GUISkin mainMenuSkin;

  void flexibleSpaces(int num) {
    for (int i = 0; i < num; ++i) {
      GUILayout.FlexibleSpace();
    }
  }

  void Start ()
  {
		boxWidth = Screen.width * 0.07f;
		boxHeight = Screen.height * 0.07f;

  }

	// Update is called once per frame
	void Update () 
	{
		if (!GameVars.getInstance().getUserHasStarted()) {
      return;
    }

    if (GameVars.getInstance ().getComboOrcKills () <= 1) {
      scoreMultiplier = multiplierOne;
      GameVars.getInstance().setComboMultiplier(1);
    } else if (GameVars.getInstance ().getComboOrcKills () == 5) {
      scoreMultiplier = multiplierTwo;
      GameVars.getInstance().setComboMultiplier(2);
    } else if (GameVars.getInstance ().getComboOrcKills () == 10) {
      scoreMultiplier = multiplierThree;
      GameVars.getInstance().setComboMultiplier(3);
    } else if (GameVars.getInstance ().getComboOrcKills () == 15) {
      scoreMultiplier = multiplierFour;
      GameVars.getInstance().setComboMultiplier(4);
    } else if (GameVars.getInstance ().getComboOrcKills () == 20) {
      scoreMultiplier = multiplierFive;
      GameVars.getInstance().setComboMultiplier(5);
    }
  }


	void OnGUI()
	{
		GUI.skin = mainMenuSkin;
		GUI.color = Color.white;
		GUI.skin.font = CVOFont;
    GUI.skin.label.wordWrap = false;
    GUI.skin.label.stretchWidth = true;
    GUI.skin.label.fontSize = (int)(Screen.height * 0.05f);

	GUILayoutOption[] boxOptions = {GUILayout.Width(boxWidth), GUILayout.Height(boxHeight)};
    GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height));

    GUILayout.BeginHorizontal();
    GUILayout.FlexibleSpace();

	GUILayout.Box (roadIcon, boxOptions);
    GUILayout.Label(": " + (int) GameVars.getInstance().getDistance() + "m  ");
	GUILayout.Box (moneyIcon, boxOptions);
	GUILayout.Label(": $" + (int) GameVars.getInstance().getScore(), GUI.skin.label);
	GUILayout.Box (scoreMultiplier, boxOptions);
    
	GUILayout.FlexibleSpace();
    GUILayout.EndHorizontal();

    GUILayout.EndArea();
	}
}
