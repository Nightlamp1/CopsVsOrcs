using UnityEngine;
using System.Collections.Generic;

public class Orc : MonoBehaviour {
  private bool triedGrowl = false;

  void Update() {
    if (!gameObject.GetComponent<SpriteRenderer>().isVisible) return;
    if (triedGrowl) return;
    Debug.LogDebug("Time to try!");

    triedGrowl = true;

    AudioManager.getInstance().playGrowlSound();
  }
}
