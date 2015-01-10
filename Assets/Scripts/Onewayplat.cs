using UnityEngine;
using System.Collections;

public class Onewayplat : MonoBehaviour {

	
  void Update(){
    GameObject Player = GameVars.getInstance ().getPlayer ();
    if (Player.transform.position.y < this.transform.position.y) {
      this.collider2D.isTrigger = false;
    } else {
      this.collider2D.isTrigger = true;
    }
  }
	// Update is called once per frame
	void OnTriggerEnter2D (Collider2D other) {

    Debug.Log ("Trigger is entered");
    GameObject Player = other.gameObject;
    if (Player.transform.position.y < this.transform.position.y) {
      Physics2D.IgnoreLayerCollision (0, 2, true);
    }
	  
	}


  void OnTriggerExit2D (Collider2D other){
    Debug.Log ("Trigger is exited");
    GameObject Player = other.gameObject;
    if (Player.transform.position.y > this.transform.position.y) {
      Physics2D.IgnoreLayerCollision (0, 2, false);
      this.collider2D.isTrigger = false;
    }

  }
}
