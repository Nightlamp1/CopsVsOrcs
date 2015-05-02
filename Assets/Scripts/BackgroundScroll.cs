using UnityEngine;
using System.Collections;

public class BackgroundScroll : MonoBehaviour {

  public float speed = 0;
  
  
  // Update is called once per frame
  void Update () {
    try {
      GetComponent<Renderer>().material.mainTextureOffset = new Vector2 (Time.time * speed, 0f);
    } catch (System.Exception ex) {
      // Ignore it
    }
  }
}
