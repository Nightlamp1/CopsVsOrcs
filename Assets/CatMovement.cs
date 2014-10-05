using UnityEngine;
using System.Collections;

public class CatMovement : MonoBehaviour {
  public bool grounded = false;
  public Transform groundedEnd;
  public Transform spawnPosition;
  public Vector2 directionxy;
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

    if (Input.GetKey (KeyCode.D)) 
    {
      facingRight = true;
      transform.Translate (Vector2.right * Speed * Time.deltaTime);
      transform.eulerAngles = new Vector2(0, 0);
    } 
    else if (Input.GetKey (KeyCode.A)) 
    {
      facingRight = false;
      transform.Translate (Vector2.right * Speed * Time.deltaTime);
      transform.eulerAngles = new Vector2(0, 180);
    }

    if (Input.GetKeyDown (KeyCode.Space) && grounded == true) 
    {
      rigidbody2D.AddForce (Vector2.up * 300f);
    }
  }

  void Raycasting()
  {
    Debug.DrawLine (this.transform.position, groundedEnd.position, Color.green);

    grounded = Physics2D.Linecast (this.transform.position, groundedEnd.position, 1 << LayerMask.NameToLayer ("Ground"));
  }

  void Weapon ()
  {
    if (Input.GetKeyDown (KeyCode.R))
    {
      Bullet b;

      b = Instantiate(bullet, this.transform.position, this.transform.rotation) as Bullet;

      b.transform.Translate(new Vector2(0.5f, 0));

      b.rigidbody2D.velocity.Set(0, 0);

      b.setFacingRight(facingRight);

      b.fire();
    }
  }
}
