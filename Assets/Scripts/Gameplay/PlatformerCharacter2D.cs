using UnityEngine;

public class PlatformerCharacter2D : MonoBehaviour 
{
  bool facingRight = true;							// For determining which way the player is currently facing.

  [SerializeField] float maxSpeed = 10f;				// The fastest the player can travel in the x axis.
  [SerializeField] float jumpForce = 400f;			// Amount of force added when the player jumps.	

  [Range(0, 1)]
  [SerializeField] float crouchSpeed = .36f;		    // Amount of maxSpeed applied to crouching movement. 1 = 100%

  [SerializeField] LayerMask whatIsGround;			// A mask determining what is ground to the character

  Transform groundCheck;								// A position marking where to check if the player is grounded.
  float groundedRadius = .2f;							// Radius of the overlap circle to determine if grounded
  bool grounded = false;								// Whether or not the player is grounded.
  Animator anim;	// Reference to the player's animator component.
	float FlashPeriod = 0.1f;

  bool justJumped = false;
  private int jumpCheck = 0;  // This is going to allow for ungrounded double jumps
	SpriteRenderer Rend;


  void Awake()
	{
		// Setting up references.
		groundCheck = transform.Find("GroundCheck");
		anim = gameObject.GetComponent<Animator>();
		Rend = gameObject.GetComponent<SpriteRenderer> ();
		DontDestroyOnLoad (gameObject);
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

		//Code for distance calculation
		GameVars.getInstance().setDistance((this.transform.position.x + 5.8f) / 10f);
	}

  // This function serves as an API to reset the jump counter from outside
  //  of this class.
  public void resetJumpCheck() {
    jumpCheck = 0;
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
    if (!GameVars.getInstance().getUserHasStarted() || 
       (SceneManager.Scene) Application.loadedLevel != SceneManager.Scene.ENDLESS_RUN) {
      GetComponent<Rigidbody2D>().velocity = new Vector2();
      return;
    }

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

      PlayerManager.getInstance().jump();
      jumpCheck += 1;

      GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, 0);
        
      float localJumpForce = jumpForce;
      string animString = "Jump";

      if (jumpCheck == 2) {
        localJumpForce -= 100f;
        animString = "Jump2";
      }

      GetComponent<Rigidbody2D>().AddForce(new Vector2(1f, localJumpForce));
      anim.SetBool(animString, true);

      justJumped = true;
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

void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.tag == "Enemy") {

			for(var n = 0; n < 2; n++)
			{
				Invoke("Flashoff", (n*FlashPeriod) + 0.01f);
				Invoke ("FlashOn", (n*FlashPeriod) + FlashPeriod);
			}
			Rend.enabled = true;
		}
	}
	void FlashOn()
	{
		Rend.enabled = true;
		return;
	}
	void Flashoff()
	{
		Rend.enabled = false;
		return;
	}
}
