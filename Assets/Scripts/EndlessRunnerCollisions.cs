using UnityEngine;
using System.Collections;

public class EndlessRunnerCollisions : MonoBehaviour {
  HudScript hud_script;
  
  void Start()
  {
    hud_script = GameObject.Find("Main Camera").GetComponent<HudScript>();
  }

  /*void OnCollisionEnter2D(Collision2D other)
  {
    /*if (other.name == "Powerup(Clone)")
    {
      Destroy (other);
      hud_script.IncreaseScore(100);
    }*/
   /* if (other.gameObject.tag == "Enemy")
    {
      if (other.gameObject.tag == "Player")
      {
				Debug.Log ("Player Hit");
        hud_script.IncreaseScore(-100);
      }
      else if(other.gameObject.tag == "Bullet")
      {
				Debug.Log ("enemy hit");
        hud_script.IncreaseScore(100);
      }
      else
      {
        return;
      }
    }
    //else
    //{
     // Debug.Log ("Collided with " + other.gameObject.tag);
    //}
	}*/
}
