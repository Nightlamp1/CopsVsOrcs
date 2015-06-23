using UnityEngine;
using System.Collections;

public class TutorialToggle : MonoBehaviour {

	// Use this for initialization
	void Start () {
    if (!GameVars.getInstance().getUserHasStarted()) {
			this.gameObject.GetComponent<SpriteRenderer> ().enabled = true;
    }
	}
	
	// Update is called once per frame
	void Update () {
    if (GameVars.getInstance().getUserHasStarted()) {
			this.gameObject.GetComponent<SpriteRenderer> ().enabled = false;
    }
	}
}
