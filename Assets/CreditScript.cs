using UnityEngine;
using System.Collections;

public class CreditScript : MonoBehaviour {
	private       float XPOS   = Screen.width *0.9f; // Set in OnGUI once.  Should be fixed...
	private       float YPOS   = Screen.height*0.8f; // Set in OnGUI once.  Should be fixed...
	
	private const float WIDTH  = 50;
	private const float HEIGHT = 50;
	public Texture2D MainReturn;

	void OnGUI(){


		if (GUI.Button (new Rect (XPOS, YPOS, WIDTH, HEIGHT), MainReturn, "")) {
			Application.LoadLevel (0);
		}
	}
}
