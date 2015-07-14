using UnityEngine;
using System.Collections;

public class HudScript : MonoBehaviour {
	public Font CVOFont;
	private int ScoreTen = 0;
	public Texture2D roadIcon;
	public Texture2D moneyIcon;
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

	}


	void OnGUI()
	{
    if (!GameVars.getInstance().getUserHasStarted()) {
      return;
    }
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
    
	GUILayout.FlexibleSpace();
    GUILayout.EndHorizontal();

    GUILayout.EndArea();
	}
}
