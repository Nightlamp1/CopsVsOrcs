using UnityEngine;

[RequireComponent(typeof(PlatformerCharacter2D))]
public class Platformer2DUserControl : MonoBehaviour 
{
	private PlatformerCharacter2D character;
  private bool jump;
  private bool shoot;

  private bool jumpOver = true;
  private bool shootOver = true;

  private int counter1 = 0;
  private int counter2 = 0;
  private int counter3 = 0;

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
//#if CROSS_PLATFORM_INPUT
#if (!UNITY_EDITOR_WIN && !UNITY_STANDALONE_OSX && !UNITY_WEBPLAYER)
    if (Input.touchCount > 0)
    {
      for (int i = 0; i < Input.touchCount; i++)
      {
        var touch = Input.GetTouch(i);
        if (touch.position.x < Screen.width/2 && jumpOver)
        {
          //Debug.Log ("You Jumped");
          jump = true;
          jumpOver = false;
          
          counter1 += 1;
        }
        else if (touch.position.x > Screen.width/2 && shootOver)
        {
          //Debug.Log("You shoot");
          shoot = true;
          shootOver = false;
        }
      }
      
      update_hud();
    }
    else
    {
      jumpOver = true;
      shootOver = true;
      counter3 += 1;
    }
#else
    if (CrossPlatformInput.GetButtonDown("Jump")) jump = true;
    if (CrossPlatformInput.GetButtonDown("Shoot")) shoot = true;
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

    if (jump)
    {
      counter2 += 1;
    }

    update_hud();

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


  void update_hud()
  {
    GameVars.getInstance().debugMessage = 
      "c1, c2, c3, sO, jO, tC = {" + 
      counter1 + ", " + counter2 + ", " + counter3 + ", " + shootOver + ", " + jumpOver + ", " + Input.touchCount + "}";
  }
}
