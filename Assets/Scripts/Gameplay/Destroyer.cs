using UnityEngine;
using System.Collections;

public class Destroyer : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other)
	{
    if (other.tag == "Indestructible") return;

		if (other.tag == "Player") 
		{
      PlayerManager.getInstance().killPlayer();
      GameVars.getInstance().incrementGameSession(1);
			return;
		}

		if (other.gameObject.transform.parent) 
		{
			Destroy (other.gameObject.transform.parent.gameObject);	
		} 
		else 
		{
			Destroy (other.gameObject);
		}
	}
}
