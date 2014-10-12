/* The following script controls a generic ground based enemy patrol pattern
 * Player starting and ending position are stored as floats
 * ending position is equal to starting position + a movement distance interger
 * an integer movement speed is used to control all velocity settings
 * a boolean is used to determine the movement direction (moveRight)
*/

using UnityEngine;
using System.Collections;

public class GroundEnemyPatrol : MonoBehaviour {


	public float startPos;
	public float endPos;
	public int moveDistance = 5;
	public int moveSpeed = 2;

	bool moveRight = true;

	void Start()
	{
		startPos = transform.position.x;
		endPos = startPos + moveDistance;
	}




	// Update is called once per frame
	void Update () {
		if (moveRight) 
		{
			rigidbody2D.velocity = new Vector2(moveSpeed,0);
		}
		if (rigidbody2D.position.x >= endPos) 
		{
			moveRight = false;
		}
		if (!moveRight) 
		{
			rigidbody2D.velocity = new Vector2 (-moveSpeed, 0);
		}
		if (rigidbody2D.position.x <= startPos) 
		{
			moveRight = true;
		}

	}
}
