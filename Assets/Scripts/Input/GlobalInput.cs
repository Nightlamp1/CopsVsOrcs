using UnityEngine;
using System.Collections;

public class GlobalInput : MonoBehaviour {
  private const int BACK_BUTTON_DOUBLE_TAP_THRESHOLD = 3; // seconds

  private int timeSinceLastBackButtonPress = 0;

  void Start() {

  }

  void FixedUpdate() {
    if (timeSinceLastBackButtonPress >= 0) {
      timeSinceLastBackButtonPress += 1;
    }

    if (timeSinceLastBackButtonPress > BACK_BUTTON_DOUBLE_TAP_THRESHOLD) {
      timeSinceLastBackButtonPress = -1;
    }

    // This should respond to back button presses and escape keyboard presses.
    //   I don't think we can do anything like this on the iOS devices.
    if (Input.GetKeyUp(KeyCode.Escape)) {
      timeSinceLastBackButtonPress = 0;

      if (timeSinceLastBackButtonPress >= 0) {
        Application.Quit();
        return;
      } else {
        return;
      }
    }
  }
}
