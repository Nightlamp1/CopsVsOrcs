using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EasterEggsHiddenGUI : MonoBehaviour {
  private BoxCollider2D orcCollider;

  void Start() {
    orcCollider = gameObject.AddComponent<BoxCollider2D>();
    orcCollider.size = new Vector2(Screen.width * 0.05f, Screen.height * 0.05f);
    orcCollider.offset = new Vector2(Screen.width * 0.01f, Screen.height * 0.4f);
  }

  void Update() {
    if (Input.GetMouseButtonDown(0)) {
      Debug.Log("Clicked");
    }
  }

  void OnMouseDown() {
    Debug.LogDebug("Testing");
  }

  void OnDestroy() {
    Destroy(orcCollider);
  }
}
