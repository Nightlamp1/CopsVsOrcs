using UnityEngine;
using System.Collections;

public class Destroyer : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player") 
		{
#if NOADS || BANNERADS
      Application.LoadLevel(3);
#else
			Application.LoadLevel(2);
#endif
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
