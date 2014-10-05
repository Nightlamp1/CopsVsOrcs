using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
  bool facingRight = true;

	// Use this for initialization
	void Start () 
  {
	
	}
	
	// Update is called once per frame
	void Update () 
  {
	  
  }
  
  public void setFacingRight(bool p_facingRight)
  {
    this.facingRight = p_facingRight;
  }
  
  void OnCollisionEnter2D(Collision2D other)
  {
    Debug.Log (this.transform.tag + " struck " + other.transform.tag + " (" + other.GetType() + ")");

    if(other.transform.tag == "Enemy")
    { 
      Destroy (this);
      other.gameObject.SendMessage("struckWithBullet");
    }
    else if (other.transform.tag == "Cop")
    {
      Destroy (this);
      other.gameObject.SendMessage("struckWithBullet");
    }
    else
    {
      // Always destroy the bullet upon collision.
      Destroy (this);
    }
  }
  
  public void fire()
  {
    this.rigidbody2D.AddForce(Vector2.right * (facingRight ? 1000f : -1000f), 0);
  }
}
