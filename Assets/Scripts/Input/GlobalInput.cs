using UnityEngine;
using System.Collections;

public class GlobalInput : MonoBehaviour {
  private const float BACK_BUTTON_DOUBLE_TAP_THRESHOLD = 2.5f; // seconds

  private float tSinceBack  = -1f;
  private float startX      = 0f;
  private float startY      = 0f;
  private float width       = 0f;
  private float height      = 0f;

  void Start() {
    GUI.color = new Color(0, 0, 0, 0);

    width  = Screen.width  * 0.25f;
    height = Screen.height * 0.05f;
    startX = (Screen.width - width) / 2f;
    startY = Screen.height * 0.85f;
  }

  void FixedUpdate() {
    if (tSinceBack >= 0) {
      tSinceBack += Time.deltaTime;
    }

    if (tSinceBack > BACK_BUTTON_DOUBLE_TAP_THRESHOLD) {
      tSinceBack = -1;
    }

    // This should respond to back button presses and escape keyboard presses.
    //   I don't think we can do anything like this on the iOS devices.
    if (Input.GetKeyDown(KeyCode.Escape)) {
      if (tSinceBack >= 0) {
        Application.Quit();

        return;
      } else {
        tSinceBack = 0f;

        return;
      }
    }
  }

  void OnGUI() {
    GUIStyle boxStyle = GUI.skin.GetStyle("Box");
    boxStyle.fontSize = 30;
    boxStyle.alignment = TextAnchor.MiddleCenter;

    if (tSinceBack >= 0) {
      GUI.Box(new Rect(startX, startY, width, height), "Press back again to quit", boxStyle);
    }
  }
}
