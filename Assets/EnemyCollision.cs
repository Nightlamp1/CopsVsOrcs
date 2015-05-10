using UnityEngine;
using System.Collections;

public class EnemyCollision : MonoBehaviour {
	HudScript hud;
	
	void Start()
	{
		hud = GameObject.Find("Main Camera").GetComponent<HudScript>();
	}
	
	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.tag == "Player") 
		{
			hud.IncreaseScore (-100);
			Destroy (this.gameObject);
		} 
		else if (other.gameObject.tag == "Bullet") 
		{
			hud.IncreaseScore (100);
			GameVars.getInstance().orcKills += 1;
			Destroy (this.gameObject);
			Destroy (other.gameObject);
		}
	}
}
