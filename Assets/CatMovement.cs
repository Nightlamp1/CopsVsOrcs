using UnityEngine;
using System.Collections;

public class CatMovement : MonoBehaviour {

	public bool grounded = false;
	public Transform groundedEnd;
	public Transform spawnPosition;
	public GameObject bullet;
	public Vector2 directionxy;
	//public Transform Shoot;

	public float Speed = 6.0f;

	Animator anim;

	void Start()
	{
		anim = GetComponent <Animator>();
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
			transform.Translate (Vector2.right * Speed * Time.deltaTime);
			transform.eulerAngles = new Vector2(0, 0);			
		}
		if (Input.GetKey (KeyCode.A)) 
		{
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
			directionxy = new Vector2(transform.forward.x,0);
			Instantiate(bullet,spawnPosition.position, spawnPosition.rotation);
			//bullet.rigidbody2D.AddForce(directionxy*10f);
		}
	}
}
