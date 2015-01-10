﻿using UnityEngine;
using System.Collections;

public class Passthrough : MonoBehaviour {
	public GameObject platform;

	void Update()
	{
		if (platform == null)
      return;

		if (this.transform.position.y < platform.transform.position.y) 
    {
			platform.collider2D.isTrigger = true;
		}
	}
	
	
	void OnTriggerEnter2D (Collider2D other)
	{
		platform = other.gameObject;
		if (other.gameObject.tag == "Passable" && rigidbody2D.velocity.y > -5) 
		{
          platform = other.gameObject;
      Debug.Log ("Trigger entered");
      Debug.Log (rigidbody2D.velocity.y);
      rigidbody2D.AddForce (new Vector2 (0f, 5f));
		  Physics2D.IgnoreCollision(platform.collider2D, collider2D, rigidbody2D.velocity.y > 0);
		} 
		else if(other.gameObject.tag == "Passable")
		{
			platform = other.gameObject;
		    other.collider2D.isTrigger = false;
		}
	}
	
	void OnTriggerExit2D (Collider2D other)
	{
    if (other.gameObject.tag == "Passable" && rigidbody2D.velocity.y > 0) {
      Debug.Log ("Trigger exit " + platform.collider2D);
      Physics2D.IgnoreCollision (platform.collider2D, collider2D, false);
      rigidbody2D.AddForce (new Vector2 (0f, -10f));
      other.collider2D.isTrigger = false;
    } 
    else if (other.gameObject.tag == "Passable" && rigidbody2D.velocity.y < 0) 
    {
      other.collider2D.isTrigger = true;
    }

	}

}