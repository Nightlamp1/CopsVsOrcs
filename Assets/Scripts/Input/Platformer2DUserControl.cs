using UnityEngine;

[RequireComponent(typeof(PlatformerCharacter2D))]
public class Platformer2DUserControl : MonoBehaviour 
{
	private PlatformerCharacter2D character;
  private bool jump;
  private bool shoot;
	private int isMove = 1; //1 is moving. 0 is no movement

  private bool jumpOver = true;
  private bool shootOver = true;

  private int counter1 = 0;
  private int counter2 = 0;
  private int counter3 = 0;

  void Start()
  {
    gameObject.GetComponent<Armable> ().equip ("Pistol");
		Time.timeScale = 0;
    GameVars.getInstance().setUserHasStarted(false);
  }

	void Awake()
	{
    jump = false;
    shoot = false;
		character = GetComponent<PlatformerCharacter2D>();
	}

  void Update ()
  {
    // Don't do anything if we're not on the Endless Run Level
    //  or if the player is dead.
    if (Application.loadedLevel != GameVars.ENDLESS_RUN_SCENE || 
        !PlayerManager.getInstance().isPlayerAlive()) return;

    // Read the jump input in Update so button presses aren't missed.
    if (Input.touchCount > 0)
    {
      for (int i = 0; i < Input.touchCount; i++)
      {
        var touch = Input.GetTouch(i);
        if (touch.position.x < Screen.width/2 && jumpOver)
        {
          jump = true;
          jumpOver = false;
          
          counter1 += 1;
        }
        else if (touch.position.x > Screen.width/2 && shootOver)
        {
          shoot = true;
          shootOver = false;
        }
      }
    }
    else
    {
      jumpOver = true;
      shootOver = true;
      counter3 += 1;
    }

    if (CrossPlatformInput.GetButtonDown ("Jump")) {
			jump = true;
		}

    if (CrossPlatformInput.GetButtonDown("Shoot")) shoot = true;

    // If the user hasn't started yet and they tried to jump or shoot, then start movement.
    //   This is essentially a ghetto 'press any key' implementation.
    if (!GameVars.getInstance().getUserHasStarted() && (jump || shoot)) {
      jump = false;
      shoot = false;

      Time.timeScale = 1;
      GameVars.getInstance().setUserHasStarted(true);
      GameObject.Find ("HeroCop(Clone)").transform.position = new Vector3 (-6f, -3.4f, 0f);
    }

		if (Application.loadedLevel == GameVars.ENDLESS_RUN_SCENE)
			GetComponent<SpawnGroundversion2> ().enabled = true;
  }

	void FixedUpdate()
	{
		// Pass all parameters to the character control script.
		character.Move(isMove, false, jump);

    if (jump)
    {
      counter2 += 1;
    }

    if (shoot)
    {
      gameObject.GetComponent<Armable> ().activate ();
      shoot = false;
    }

    // Reset the jump input once it has been used.
	  jump = false;
	}
}
