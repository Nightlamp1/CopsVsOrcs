#pragma warning disable 0168 // variable declared but not used.

using UnityEngine;
using System.Collections;

public class BackgroundScroll : MonoBehaviour {

  public float speed = 0;
  
  // Update is called once per frame
  void Update () {
    try {
      GetComponent<Renderer>().material.mainTextureOffset = new Vector2 (Time.time * speed, 0f);
    } catch (System.Exception ex) {
      // I'm not certain what's generating this Exception, we'll continue to ignore it.
      //  But we have to do something with it in order for it to not complain.

    }
  }
}
