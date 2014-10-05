#pragma strict

var bulletKey : KeyCode;
var spawnPosition : Transform;
var bullet : GameObject;
var cooldown : int;

function Update () 
{
	if(cooldown >0)
	{
	cooldown--;
	}
	else
	{
	cooldown = 30;
	if (Input.GetKey(bulletKey))
			{
				Debug.Log("Bullet Fire");
				spawnPosition = transform;
				Instantiate(bullet,spawnPosition.position,spawnPosition.rotation);
			}
	}
	
}

