using UnityEngine;
using System.Collections;
/* This is a prototype of an achievement list. It's a simple scroll box with some buttons*/
public class ScrollBoxTest : MonoBehaviour {

	public Vector2 scrollPosition = Vector2.zero;
	void OnGUI() {
		scrollPosition = GUI.BeginScrollView(new Rect(Screen.width * 0.35f, Screen.height * 0.3f, 300, 100), scrollPosition, new Rect(0, 0, 220, 200));
		GUI.Button(new Rect(0, 0, 300, 20), "achievement1"); //top left
		GUI.Button(new Rect(0, 23, 300, 20), "achievement2"); //top right
		GUI.Button(new Rect(0, 43, 300, 20), "achievement3");//bottom left
		GUI.Button(new Rect(0, 63, 300, 20), "achievement4");//bottom right
		GUI.Button(new Rect(0, 83, 300, 20), "achievement5");//bottom right
		GUI.Button(new Rect(0, 103, 300, 20), "achievement6");//bottom right
		GUI.Button(new Rect(0, 123, 300, 20), "achievement7");//bottom right
		GUI.Button(new Rect(0, 143, 300, 20), "achievement8");//bottom right
		GUI.Button(new Rect(0, 163, 300, 20), "achievement9");//bottom right
		GUI.Button(new Rect(0, 183, 300, 20), "achievement10");//bottom right
		GUI.Button(new Rect(0, 203, 300, 20), "achievement11");//bottom right
		GUI.EndScrollView();
	}
}
