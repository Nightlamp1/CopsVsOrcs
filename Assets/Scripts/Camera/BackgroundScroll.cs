#pragma warning disable 0168 // variable declared but not used.

using UnityEngine;
using System.Collections;

public class BackgroundScroll : MonoBehaviour {

  public float speed = 0;
	public Texture2D DayTime;
	public Texture2D NightTime;
	private Material currentBackground;
	private float offset;
  
 void Start(){
		int RandScreen = Random.Range (0,2);

    print("========== {3} " + System.DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff") + " ==========");

		if (RandScreen >= 1) {
			GetComponent<MeshRenderer> ().material.mainTexture = DayTime;
		} else {
			GetComponent<MeshRenderer>().material.mainTexture = NightTime;
		}

		currentBackground = GetComponent<MeshRenderer> ().material;
	}


  // Update is called once per frame
  void FixedUpdate () {
		offset += speed * Time.deltaTime;
		offset = offset % 1.0f;
		currentBackground.mainTextureOffset = new Vector2 (offset, 0);
  }
}
