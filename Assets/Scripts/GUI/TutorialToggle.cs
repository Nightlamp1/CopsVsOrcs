using UnityEngine;
using System.Collections;

public class TutorialToggle : MonoBehaviour {
  public  Texture2D[] Overlays;
  private Texture2D   activeOverlay;
  private GUIStyle     tutorialStyle;

	// Use this for initialization
	void Start () {
    if (!GameVars.getInstance().getUserHasStarted()) {
      if (GameVars.getInstance().getGameSession() < Overlays.Length) {
        activeOverlay = Overlays[GameVars.getInstance().getGameSession()];
      } else {
        activeOverlay = Overlays[Random.Range(0, Overlays.Length)];
      }
    }

    tutorialStyle                = new GUIStyle();
    tutorialStyle.fixedHeight    = Screen.height;
    tutorialStyle.fixedWidth     = Screen.width;
    tutorialStyle.stretchHeight  = true;
    tutorialStyle.stretchWidth   = true;
	}

  void OnGUI() {
    if (!GameVars.getInstance().getUserHasStarted()) {
      GUI.DrawTexture(new Rect(0f, 0f, Screen.width, Screen.height), activeOverlay);
    }
  }
}
