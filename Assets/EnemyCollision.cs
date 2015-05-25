﻿using UnityEngine;
using System.Collections;

public class EnemyCollision : MonoBehaviour {
	HudScript hud;
	Animator anim;
	RaycastHit2D MyRay;
	
	void Start()
	{
		hud = GameObject.Find("Main Camera").GetComponent<HudScript>();
		anim = gameObject.GetComponent<Animator> ();

	}

	void FixedUpdate()
	{
		MyRay = Physics2D.Linecast(new Vector2(this.transform.position.x-1f,this.transform.position.y),new Vector2(this.transform.position.x - 3f,this.transform.position.y));
		Debug.DrawLine(new Vector3(this.transform.position.x-1f,this.transform.position.y),new Vector3(this.transform.position.x - 3f,this.transform.position.y));
		if (MyRay.collider.tag == "Player") {
			anim.SetTrigger ("Attack1");
			//Debug.Log ("HITHIHTIHTIHTI");
			//Debug.Log (MyRay.collider.tag);
		} 

	}

	
	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.tag == "Player") 
		{
			//anim.SetTrigger("Attack1");
			hud.IncreaseScore (-2);
			Physics2D.IgnoreCollision(other.collider, this.GetComponent<Collider2D>());
			//Destroy (this.gameObject,0.5f);
		} 
		else if (other.gameObject.tag == "Bullet") 
		{
			anim.SetTrigger("Death");
			hud.IncreaseScore (1);
			GameVars.getInstance().orcKills += 1;
			this.GetComponent<Collider2D>().enabled = false;
			Destroy (this.gameObject, 0.3f);
			Destroy (other.gameObject);
		}
	}
}
