using UnityEngine;
using System.Collections;

public class DebugSceneClick : MonoBehaviour {
#if UNITY_EDITOR
  void OnMouseDown() {
    SceneManager.LoadLevel(SceneManager.Scene.DEBUG);
  }
#endif
}
