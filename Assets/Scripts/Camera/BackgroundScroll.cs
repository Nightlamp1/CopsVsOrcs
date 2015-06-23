#pragma warning disable 0168 // variable declared but not used.

using UnityEngine;
using System.Collections;

public class BackgroundScroll : MonoBehaviour {

  public float speed = 0;
	public Texture2D DayTime;
	public Texture2D NightTime;
  
 void Start(){
		int RandScreen = Random.Range (0,2);

    print("========== {3} " + System.DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff") + " ==========");

		if (RandScreen >= 1) {
			GetComponent<MeshRenderer> ().material.mainTexture = DayTime;
		} else {
			GetComponent<MeshRenderer>().material.mainTexture = NightTime;
		}
	}


  // Update is called once per frame
  void Update () {
    try {
      if (GameVars.getInstance().getUserHasStarted()) {
        GetComponent<Renderer>().material.mainTextureOffset = new Vector2 (Time.time * speed, 0f);
      }
    } catch (System.Exception ex) {
      // I'm not certain what's generating this Exception, we'll continue to ignore it.
      //  But we have to do something with it in order for it to not complain.

    }
  }
}
