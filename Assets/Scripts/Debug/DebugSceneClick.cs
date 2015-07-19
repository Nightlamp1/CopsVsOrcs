using UnityEngine;
using System.Collections;

public class DebugSceneClick : MonoBehaviour {
#if UNITY_EDITOR
  void OnMouseDown() {
    Application.LoadLevel(4);

    Debug.Log("Click detected.");
  }
#endif
}
