#pragma strict

var Cop : GameObject;

function Start () 
{
	rigidbody2D.AddForce(Vector2.right*1000f,0);
	rigidbody2D.velocity.y = 0;
}
function OnCollisionEnter2D(other: Collision2D)
{
if(other.transform.tag == "Enemy")
{ 
    Destroy (this);
    Destroy (gameObject);
    Debug.Log("Destroy");
}
/*else if(other.transform.tag == "Ground")
{
	Destroy (this);
	Destroy (gameObject);
}*/
}