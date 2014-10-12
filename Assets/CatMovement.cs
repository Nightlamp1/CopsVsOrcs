/*This code is used to control the playable character movement and weapons
 * Code is used to move character and shoot weapon as dictated by certain key presses
*/
using UnityEngine;
using System.Collections;

public class CatMovement : MonoBehaviour {
  public bool grounded = false; //Used as a groundcheck to verify player jump ability is valid
  public Transform groundedEnd; //the TRANSFORM object used in ground checking function
  public Transform spawnPosition;//this variable is used to spawn the bullet in front of the player
  public bool facingRight;
  public float Speed = 6.0f;

  public Bullet bullet;

  Animator anim;
  
  void Start()
  {
    anim = GetComponent <Animator>();
    facingRight = true;
  }
  
  // Update is called once per frame
  void Update () 
  {
    Movement ();
    Raycasting ();
    Weapon ();
  }

  void Movement ()
  {
    anim.SetFloat ("Speed", Mathf.Abs (Input.GetAxis ("Horizontal")));

    if (Input.GetKey (KeyCode.D)) //This if statement controls walking right
    {
      facingRight = true;
      transform.Translate (Vector2.right * Speed * Time.deltaTime);
      transform.eulerAngles = new Vector2(0, 0);
    } 
    else if (Input.GetKey (KeyCode.A)) //This if statement controls walking left
    {
      facingRight = false;
      transform.Translate (Vector2.right * Speed * Time.deltaTime);
      transform.eulerAngles = new Vector2(0, 180);
    }
	if (Input.GetKey (KeyCode.D) && Input.GetKey (KeyCode.LeftShift)) //This if statement controls RUNNING right
		{
			facingRight = true;
			transform.Translate (Vector2.right * Speed * Time.deltaTime * 2);
			transform.eulerAngles = new Vector2(0, 0);
		} 
	else if (Input.GetKey (KeyCode.A)&& Input.GetKey (KeyCode.LeftShift)) //This if statement controls RUNNING left
		{
			facingRight = false;
			transform.Translate (Vector2.right * Speed * Time.deltaTime * 2);
			transform.eulerAngles = new Vector2(0, 180);
		}

    if (Input.GetKeyDown (KeyCode.Space) && grounded == true) //This if statement controls player JUMP controls
    {
      rigidbody2D.AddForce (Vector2.up * 300f);
    }
  }

  void Raycasting()//Raycasting controls ground check for JUMP ability
  {
    Debug.DrawLine (this.transform.position, groundedEnd.position, Color.green);

    grounded = Physics2D.Linecast (this.transform.position, groundedEnd.position, 1 << LayerMask.NameToLayer ("Ground"));
  }

  void Weapon ()
  {
    if (Input.GetKeyDown (KeyCode.R))
    {
      Bullet b;

      b = Instantiate(bullet, spawnPosition.position, spawnPosition.rotation) as Bullet;

      b.transform.Translate(new Vector2(0.5f, 0));

      b.rigidbody2D.velocity.Set(0, 0);

      b.setFacingRight(facingRight);

      b.fire();
    }
  }

	/*void OnTriggerStay2D (other : Collider2D)
	{
		if (other.transform.tag == "Ladder" && Input.GetKeyDown (KeyCode.W)) 
		{
			transform.Translate (Vector2.up * Speed * Time.deltaTime);
			Debug.Log ("Ladder HIT OMG");
		}
	}*/
}
