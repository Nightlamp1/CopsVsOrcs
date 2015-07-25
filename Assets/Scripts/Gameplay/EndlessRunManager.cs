using UnityEngine;

class EndlessRunManager {
  private static bool pauseState = false;
  private static float savedTimeScale;

  public static void setPauseState(bool state) {
    if (state == pauseState) return;

    pauseState = state;

    if (state) {
      // pausing game
      savedTimeScale = Time.timeScale;
      Time.timeScale = 0;
    } else {
      // resuming game
      Time.timeScale = savedTimeScale;
    }
  }
}
