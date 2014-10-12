using UnityEngine;
using System.Collections;

public class FlyingEnemyPatrol : MonoBehaviour {
	
	
	public float startPos;
	public float endPos;
	public int moveDistance = 5;
	public int XmoveSpeed = 2;
	public int YmoveSpeed = 1;
	
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
			rigidbody2D.velocity = new Vector2(XmoveSpeed,YmoveSpeed);
		}
		if (rigidbody2D.position.x >= endPos) 
		{
			moveRight = false;
		}
		if (!moveRight) 
		{
			rigidbody2D.velocity = new Vector2 (-XmoveSpeed,-YmoveSpeed);
		}
		if (rigidbody2D.position.x <= startPos) 
		{
			moveRight = true;
		}
		
	}
}