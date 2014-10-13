using UnityEngine;
using System.Collections;

public class HudScript : MonoBehaviour {

	float playerScore = 0;
	
	// Update is called once per frame
	void Update () 
	{
		playerScore += Time.deltaTime;
	}

	public void IncreaseScore (int amount)
	{
		playerScore += amount;
	}

	void OnDisable()
	{
		PlayerPrefs.SetFloat("Score", (float)(playerScore * 10));
	}

	void OnGUI()
	{
		GUI.Label (new Rect (Screen.width * 0.5f, Screen.height * 0.05f, 100, 30), "Score: " + (int)(playerScore * 10));
	}
}
