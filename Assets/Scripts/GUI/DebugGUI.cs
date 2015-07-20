using UnityEngine;
using System.Collections;

public class DebugGUI : MonoBehaviour {
  private Vector2 scrollView;

  void Start() {
  }

  void Update() {
  }

  void OnGUI() {
    GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height));

    scrollView = GUILayout.BeginScrollView(scrollView);//new Rect(0.05f*Screen.width, 0.05f*Screen.height, 0.9f*Screen.width, 0.9f*Screen.height));

    for (int i = 0; i < 50; ++i) {
      GUILayout.Button("This is a test button " + i + " is my number.");
    }

    GUILayout.EndScrollView();

    GUILayout.EndArea();
  }
}
