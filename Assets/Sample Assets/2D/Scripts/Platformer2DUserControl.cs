using UnityEngine;

[RequireComponent(typeof(PlatformerCharacter2D))]
public class Platformer2DUserControl : MonoBehaviour 
{
	private PlatformerCharacter2D character;
  private bool jump;
  private bool shoot;

  void Start()
  {
    gameObject.GetComponent<Armable> ().equip ("Pistol");
  }

	void Awake()
	{
		character = GetComponent<PlatformerCharacter2D>();
	}

  void Update ()
  {
      // Read the jump input in Update so button presses aren't missed.
#if CROSS_PLATFORM_INPUT
    if (CrossPlatformInput.GetButtonDown("Jump")) jump = true;
    if (CrossPlatformInput.GetButtonDown("Shoot")) shoot = true;
#else
	  if (Input.GetButtonDown("Jump")) jump = true;
#endif
  }

	void FixedUpdate()
	{
		// Read the inputs.
		//bool crouch = Input.GetKey(KeyCode.LeftControl);
		//#if CROSS_PLATFORM_INPUT
		//float h = CrossPlatformInput.GetAxis("Horizontal");
		//#else
		//float h = Input.GetAxis("Horizontal");
		//#endif

		// Pass all parameters to the character control script.
		character.Move(1, false, jump);
    if (shoot)
    {
      gameObject.GetComponent<Armable> ().activate ();
      shoot = false;
    }

    // Reset the jump input once it has been used.
	  jump = false;
	}

	//The following onGUI function allows for touch input controls
	/*void OnGUI()
	{
		if (GUI.Button (new Rect (Screen.width * 0.89f, Screen.height * 0.85f, 150, 150), "Shoot")) 
		{
			gameObject.GetComponent<Armable> ().activate ();
			shoot = false;					
		}

		if (GUI.Button(new Rect (Screen.width * 0.02f, Screen.height * 0.85f, 150, 150), "Jump"))
		{
			jump = true;
		}
	

	}*/


}
