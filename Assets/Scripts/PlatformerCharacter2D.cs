using UnityEngine;

public class PlatformerCharacter2D : MonoBehaviour 
{
	bool facingRight = true;							        // For determining which way the player is currently facing.

	[SerializeField] float maxSpeed = 10f;				// The fastest the player can travel in the x axis.
	[SerializeField] float jumpForce = 400f;			// Amount of force added when the player jumps.	

	[Range(0, 1)]
	[SerializeField] float crouchSpeed = .36f;		// Amount of maxSpeed applied to crouching movement. 1 = 100%
	
	[SerializeField] bool airControl = false;			// Whether or not a player can steer while jumping;
	[SerializeField] LayerMask whatIsGround;			// A mask determining what is ground to the character
	
	Transform groundCheck;								        // A position marking where to check if the player is grounded.
	float groundedRadius = .2f;							      // Radius of the overlap circle to determine if grounded
	bool grounded = false;								        // Whether or not the player is grounded.
	Transform ceilingCheck;								        // A position marking where to check for ceilings
	float ceilingRadius = .01f;							      // Radius of the overlap circle to determine if the player can stand up
	Animator anim;										            // Reference to the player's animator component.

  bool justJumped = false;
	int jumpCheck = 0;                            // This is going to allow for ungrounded double jumps

  void Awake()
	{
		// Setting up references.
		groundCheck = transform.Find("GroundCheck");
		ceilingCheck = transform.Find("CeilingCheck");
		anim = gameObject.GetComponent<Animator>();
	}

  bool calcGrounded()
  {
    return Physics2D.OverlapCircle(groundCheck.position, groundedRadius, whatIsGround);
  }

	void FixedUpdate()
	{
    if (!justJumped) {
      // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
      grounded = calcGrounded ();
    } else {
      if (!calcGrounded ()) {
        justJumped = false;
      }
    }
	}

  public void checkGround()
  {
    if (grounded)
    {
      jumpCheck = 0;
      anim.SetBool("Jump",false);
      anim.SetBool("Jump2",false);
      
      GameVars.getInstance().debugMessage = "jumpCheck: " + jumpCheck;
    }
  }

	public void Move(float move, bool crouch, bool jump)
	{
		// Reduce the speed if crouching by the crouchSpeed multiplier
		move = (crouch ? move * crouchSpeed : move);

		// Move the character
		GetComponent<Rigidbody2D>().velocity = new Vector2(move * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);
		
		// If the input is moving the player right and the player is facing left...
		if(move > 0 && !facingRight)
			// ... flip the player.
			Flip();
		// Otherwise if the input is moving the player left and the player is facing right...
		else if(move < 0 && facingRight)
			// ... flip the player.
  		Flip();

    checkGround ();

    // If the player should jump...
		if (jumpCheck < 2 && jump) {
      grounded = false;
      anim.SetBool("Jump",true);
      if (jumpCheck == 0)
      {
        jumpCheck+=1;
        GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x,0);
        
        GetComponent<Rigidbody2D>().AddForce(new Vector2(1f, jumpForce));
        
        justJumped = true;
       
      }
      else if (jumpCheck == 1)
      {
        jumpCheck+=1;
        GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x,0);
        
        GetComponent<Rigidbody2D>().AddForce(new Vector2(1f, jumpForce-100f));
        
        justJumped = true;
        anim.SetBool("Jump2",true);
      }
    }
	}

	
	void Flip ()
	{
		// Switch the way the player is labelled as facing.
		facingRight = !facingRight;
		
		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
