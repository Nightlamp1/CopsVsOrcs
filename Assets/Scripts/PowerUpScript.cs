using UnityEngine;
using System.Collections;

public class PowerUpScript : MonoBehaviour {

	HudScript hud;

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player")
		{
			hud = GameObject.Find("Main Camera").GetComponent<HudScript>();
			hud.IncreaseScore(10);
			Destroy (this.gameObject);
		}
	}
}
