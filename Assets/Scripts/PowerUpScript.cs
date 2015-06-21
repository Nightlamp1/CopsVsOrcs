using UnityEngine;
using System.Collections;

public class PowerUpScript : MonoBehaviour {
	HudScript hud;

  void Start()
  {
    hud = GameObject.Find("Main Camera").GetComponent<HudScript>();
  }

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player")
		{
			//hud.IncreaseScore(10);
			Destroy (this.gameObject);
		}
	}
}
