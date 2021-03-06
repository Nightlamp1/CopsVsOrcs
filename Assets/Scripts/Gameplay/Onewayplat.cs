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
    PlatformerCharacter2D platform = this.GetComponent<PlatformerCharacter2D>();
    Collider2D collider = this.GetComponent<Collider2D>();

    if (Player == null) {
      Debug.Log("Player was null unexpectedly");
    }

    if (platform == null) {
      //Debug.Log("Platform was null unexpectedly");
      return;
    }

    if (collider == null) {
      Debug.Log("Collider was null unexpectedly");
    }

    if (Player.transform.position.y > this.transform.position.y + 1.1) {
      Physics2D.IgnoreLayerCollision (0, 2, false);
      platform.resetJumpCheck();
      collider.isTrigger = false;
    }
  }
}
