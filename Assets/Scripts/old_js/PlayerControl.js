#pragma strict


var moveRight : KeyCode;
var moveLeft : KeyCode;
var jump : KeyCode;
var canJump : boolean = true;
var facingRight : boolean = true;

var speed : float = 10;

function Update () 
{
  if (Input.GetKey(moveRight))
  {
    facingRight = true;
    GetComponent.<Rigidbody2D>().velocity.x = speed;
  }
  else if (Input.GetKey(moveLeft))
  {
    facingRight = false;
    GetComponent.<Rigidbody2D>().velocity.x = speed * -1;
  }
  else
  {
    GetComponent.<Rigidbody2D>().velocity.x = 0;
  }
		
  if (Input.GetKey(jump) && canJump)
  {
    GetComponent.<Rigidbody2D>().velocity.y = 11;
    canJump = false;
  }
}


function OnCollisionEnter2D(other: Collision2D)
{
  if(other.transform.tag == "Ground")
  { //If the player lands on ground
    canJump = true; //allow him to jump again
  }
}