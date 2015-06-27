using UnityEngine;
using System.Collections;

public class CreditScript : MonoBehaviour {
	public Texture2D MainReturn;

  void flexibleSpaces(int num) {
    for (int i = 0; i < num; ++i) {
      GUILayout.FlexibleSpace();
    }
  }

	void OnGUI(){
    GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height));
    GUIStyle buttonStyle = new GUIStyle();
    GUILayoutOption[] buttonOptions = {
        GUILayout.Width(0.15f * Screen.width),
        GUILayout.Height(0.15f * Screen.height)
      };

    flexibleSpaces(9);

    GUILayout.BeginHorizontal();

    flexibleSpaces(39);

		if (GUILayout.Button (MainReturn, buttonStyle, buttonOptions)) {
			SceneManager.LoadLevel (GameVars.MAIN_MENU_SCENE);
		}

    flexibleSpaces(1);

    GUILayout.EndHorizontal();

    flexibleSpaces(1);

    GUILayout.EndArea();
	}
}
