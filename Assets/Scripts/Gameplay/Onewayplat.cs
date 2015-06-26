using UnityEngine;
using System.Collections;

public class Onewayplat : MonoBehaviour {
	
  void Update(){
    GameObject Player = GameVars.getInstance().getPlayer();
    if (Player.transform.position.y - 0.8 > this.transform.position.y) {
      this.GetComponent<Collider2D>().isTrigger = false;
    } else {
      this.GetComponent<Collider2D>().isTrigger = true;
    }
  }
	// Update is called once per frame
	void OnTriggerEnter2D (Collider2D other) {
    GameObject Player = other.gameObject;

    if (Player.transform.position.y < this.transform.position.y) {
      Physics2D.IgnoreLayerCollision (0, 2, true);
    }
	  
	}


  void OnTriggerExit2D (Collider2D other){
    GameObject Player = other.gameObject;

    if (Player.transform.position.y > this.transform.position.y +1.1) {
      Physics2D.IgnoreLayerCollision (0, 2, false);
			this.GetComponent<PlatformerCharacter2D>().resetJumpCheck();
      this.GetComponent<Collider2D>().isTrigger = false;
    }

  }
}
