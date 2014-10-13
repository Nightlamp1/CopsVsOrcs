using UnityEngine;
using System.Collections;

public class Groundable : MonoBehaviour {
  public bool grounded = false; //Used as a groundcheck to verify player jump ability is valid
  public Transform groundedEnd; //the TRANSFORM object used in ground checking function

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
  void Update () {
    Raycasting ();
  }
  
  void Raycasting()//Raycasting controls ground check for JUMP ability
  {
    Debug.DrawLine (this.transform.position, groundedEnd.position, Color.green);
    
    grounded = Physics2D.Linecast (this.transform.position, groundedEnd.position, 1 << LayerMask.NameToLayer ("Ground"));
  }
}
