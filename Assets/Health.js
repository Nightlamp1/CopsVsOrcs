#pragma strict

var health : int = 100;

function OnCollisionEnter2D(other: Collision2D)
{
  if(other.transform.tag == "Enemy")
  { 
    health -= 10;
    Debug.Log("Your health is " + health); 
  }
}

function struckWithBullet()
{
  health -= 10;
  Debug.Log("Your health is " + health); 
}