using UnityEngine;
using System.Collections;

public class Destroyer : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player") 
		{
      Debug.Log("Loading Level 2");
			Application.LoadLevel(2);
			return;
		}

		if (other.gameObject.transform.parent) 
		{
      Debug.Log("Destroying a game object.");
			Destroy (other.gameObject.transform.parent.gameObject);	
		} 
		else 
		{
      Debug.Log("Destroying an other game object.");
			Destroy (other.gameObject);
		}
	}
}
