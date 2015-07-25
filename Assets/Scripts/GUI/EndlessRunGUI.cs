using UnityEngine;

public class EndlessRunGUI : MonoBehaviour {
  private Texture2D resume;
  private Texture2D pause;

  public bool paused { get; private set; }
  public bool newPauseState;

  private Rect resumePauseRect;
  private GUIStyle resumePauseButtonStyle;

  public enum EndlessRunGuiTexturesEnum {
    RESUME,
    PAUSE,
  }

  void Start() {
    paused = false;

    resume  = getERTexture(EndlessRunGuiTexturesEnum.RESUME);
    pause = getERTexture(EndlessRunGuiTexturesEnum.PAUSE);

    resumePauseRect = new Rect(
        Screen.width - (resume.width * 1.25f),
        0 + (resume.height * 0.25f),
        resume.width,
        resume.height);

    resumePauseButtonStyle  = new GUIStyle();
  }

  void OnGUI() {
    newPauseState = paused;

    if (paused) {
      if (GUI.Button(resumePauseRect, resume, resumePauseButtonStyle)) {
        newPauseState = false;
      }
    } else {
      if (GUI.Button(resumePauseRect, pause, resumePauseButtonStyle)) {
        newPauseState = true;
      }
    }

    if (paused != newPauseState) {
      paused = newPauseState;
      EndlessRunManager.setPauseState(paused);
    }
  }

  private Texture2D getERTexture(EndlessRunGuiTexturesEnum ert) {
    GUIManager gm = gameObject.GetComponent<GUIManager>();

    for (int i = 0; i < gm.endlessRunGuiTexturesEnum.Length; ++i) {
      if (gm.endlessRunGuiTexturesEnum[i] == ert) {
        return gm.endlessRunGuiTextures[i];
      }
    }

    return null;
  }
}
