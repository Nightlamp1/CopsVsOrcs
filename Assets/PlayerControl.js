#pragma strict


var moveRight : KeyCode;
var moveLeft : KeyCode;
var jump : KeyCode;
var canJump : boolean = true;

var speed : float = 10;


function Update () 
{
	if (Input.GetKey(moveRight))
	{
		rigidbody2D.velocity.x = speed;
	}
	else if (Input.GetKey(moveLeft))
	{
		rigidbody2D.velocity.x = speed * -1;
	}
	else
	{
		rigidbody2D.velocity.x = 0;
	}
		
		
	if (Input.GetKey(jump) && canJump)
	{
		rigidbody2D.velocity.y = 11;
		canJump = false;
	}
	//else
	//{
		
	//}
	
}


function OnCollisionEnter2D(other: Collision2D)
{
if(other.transform.tag == "Ground")
{ //If the player lands on ground
    canJump = true; //allow him to jump again
}
}