using UnityEngine;
using System.Collections;

public class HudScript : MonoBehaviour {
	public Font CVOFont;
	private int ScoreTen = 0;
  void Start ()
  {
  }

	// Update is called once per frame
	void Update () 
	{
		if (!GameVars.getInstance().getUserHasStarted()) {
      return;
    }

		ScoreTen += 1;
		if (ScoreTen >= 100) {
			GameVars.getInstance().incrementScore(0.1f);
			ScoreTen = 0;
		}
	}

	public void IncreaseScore (int amount)
	{
    GameVars.getInstance().incrementScore((float)amount);
	}

	void OnGUI()
	{
		GUI.color = Color.white;
		GUI.skin.font = CVOFont;
    GUI.skin.label.wordWrap = false;
    GUI.skin.label.stretchWidth = true;
    GUI.skin.label.fontSize = 24;

    GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height));

    GUILayout.BeginHorizontal();
    GUILayout.FlexibleSpace();

    GUILayout.Label((int) GameVars.getInstance().getDistance() + "m  " + (int) GameVars.getInstance().getScore() + "pts", GUI.skin.label);

    GUILayout.FlexibleSpace();
    GUILayout.EndHorizontal();

    GUILayout.EndArea();
	}
}
