using UnityEngine;
using System.Collections;

public class Passthrough : MonoBehaviour {
	public GameObject platform;

	void Update()
	{
		if (platform == null)
      return;

		if (this.transform.position.y < platform.transform.position.y) 
    {
			platform.GetComponent<Collider2D>().isTrigger = true;
		}
	}
	
	
	void OnTriggerEnter2D (Collider2D other)
	{
		platform = other.gameObject;
		if (other.gameObject.tag == "Passable" && GetComponent<Rigidbody2D>().velocity.y > 0) 
		{
          platform = other.gameObject;
      Debug.Log ("Trigger entered");
      Debug.Log (GetComponent<Rigidbody2D>().velocity.y);
      GetComponent<Rigidbody2D>().AddForce (new Vector2 (0f, 20f));
		  Physics2D.IgnoreCollision(platform.GetComponent<Collider2D>(), GetComponent<Collider2D>(), true);
		} 
		else if(other.gameObject.tag == "Passable")
		{
			platform = other.gameObject;
		    other.GetComponent<Collider2D>().isTrigger = false;
		}
	}
	
	void OnTriggerExit2D (Collider2D other)
	{
    if (other.gameObject.tag == "Passable" && GetComponent<Rigidbody2D>().velocity.y > 0) {
      Debug.Log ("Trigger exit " + platform.GetComponent<Collider2D>());
      Physics2D.IgnoreCollision (platform.GetComponent<Collider2D>(), GetComponent<Collider2D>(), false);
      //rigidbody2D.AddForce (new Vector2 (0f, -5f));
      other.GetComponent<Collider2D>().isTrigger = false;
    } 
    else if (platform.transform.position.y > this.transform.position.y) 
    {
      platform.GetComponent<Collider2D>().isTrigger = true;
    }

	}

}