/*This code is used to control the playable character movement and weapons
 * Code is used to move character and shoot weapon as dictated by certain key presses
*/
using UnityEngine;
using System.Collections;

public class CatMovement : MonoBehaviour {
  public const float DEFAULT_SPEED = 6.0f;
  public const float JUMP_FORCE = 350f;
  private const string BLANK_EQUIP_REQUEST = "BLANK_EQUIP_REQUEST";

  public Vector2 directionxy;
  public Transform spawnPosition;//this variable is used to spawn the bullet in front of the player
  public bool facingRight;
  public float Speed = 6.0f;
  public Bullet bullet;
  private bool m_cheats = false;

  Animator anim;
  
  void Start()
  {
    anim = GetComponent <Animator>();
    facingRight = true;
#if CHEATS
    m_cheats = true;
#else
    m_cheats = false;
#endif
  }
  
  // Update is called once per frame
  void Update () 
  {
    Movement ();
    Weapon ();
  }

  void Movement ()
  {
    anim.SetFloat ("Speed", Mathf.Abs (Input.GetAxis ("Horizontal")));

    // Check sprint
    if (Input.GetKey (KeyCode.LeftShift) && getGrounded () == true)
    {
      Speed = DEFAULT_SPEED * 1.5f;
    }
    else if (getGrounded () == true)
    {
      Speed = DEFAULT_SPEED;
    }

    // Handle directional input
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

    string l_equipRequest = BLANK_EQUIP_REQUEST;

    if (Input.GetKeyDown(KeyCode.Alpha1))
    {
      l_equipRequest = "";
    }
    else if (Input.GetKeyDown(KeyCode.Alpha4))
    {
      l_equipRequest = "Rifle";
    }

    if (l_equipRequest != BLANK_EQUIP_REQUEST)
    {
      // TODO: Add cheats / not cheats version.
      Debug.Log ("Requesting to equip " + l_equipRequest);

      Armable l_armable = (Armable) gameObject.GetComponent (typeof(Armable));

      l_armable.equip (l_equipRequest);
    }

    if (m_cheats)
    {
      // Handle health cheats input
      if (Input.GetKeyDown (KeyCode.PageUp))
      {
        HankGUI.getInstance().decPlayerHP(-0.10f);
      }
      else if (Input.GetKeyDown (KeyCode.PageDown))
      {
        HankGUI.getInstance().decPlayerHP(0.10f);
      }

      if (Input.GetKeyDown (KeyCode.W)) // Jetpack
      {
        GetComponent<Rigidbody2D>().AddForce (Vector2.up * JUMP_FORCE);
      }
      else if (Input.GetKeyDown (KeyCode.S)) // Reverse Jetpack
      {
        GetComponent<Rigidbody2D>().AddForce (Vector2.up * (- JUMP_FORCE));
      }
    }
    else
     {
    }

    if (Input.GetKeyDown (KeyCode.Space) && getGrounded() == true) //This if statement controls player JUMP controls
    {
      GetComponent<Rigidbody2D>().AddForce (Vector2.up * JUMP_FORCE);
    }

  }

  private bool getGrounded()
  {
    return ((Groundable) FindObjectOfType(typeof(Groundable))).grounded;
  }

  void Weapon ()
  {
    if (Input.GetKeyDown (KeyCode.R))
    {
      Armable a = (Armable) gameObject.GetComponent (typeof(Armable));

      if (a != null)
      {
        a.activate ();
      }
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
