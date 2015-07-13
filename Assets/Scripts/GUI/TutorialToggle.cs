using UnityEngine;
using System.Collections;

public class TutorialToggle : MonoBehaviour {


	public Sprite Overlay1; //Basic Instruction
	public Sprite Overlay2; //Double Jump Introduction
	public Sprite Overlay3; //Scoring and Multiplier Breakdown
	// Use this for initialization
	void Start () {
    if (!GameVars.getInstance().getUserHasStarted()) {
			if(GameVars.getInstance().getgameSession() == 2) {
				this.gameObject.GetComponent<SpriteRenderer>().sprite = Overlay2;
			}else if (GameVars.getInstance().getgameSession() == 3){
				this.gameObject.GetComponent<SpriteRenderer>().sprite = Overlay3;
			}else this.gameObject.GetComponent<SpriteRenderer>().sprite = Overlay1;

			this.gameObject.GetComponent<SpriteRenderer> ().enabled = true;
    }
	}
	
	// Update is called once per frame
	void Update () {
    if (GameVars.getInstance().getUserHasStarted()) {
			this.gameObject.GetComponent<SpriteRenderer> ().enabled = false;
			Debug.Log ("Game Session is:" + GameVars.getInstance().getgameSession());
    }
	}
}
