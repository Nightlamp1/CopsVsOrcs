#pragma strict

var VELOCITY : int;
var threshold : int;
var curr_iter : int;
var direction : int;

function Start () {
  rigidbody2D.velocity.x = VELOCITY;
}

function Update () {
  curr_iter++;
  
  if (curr_iter > threshold) {
    curr_iter = 0;
  
  	/*
	var rnd = Random.Range(-10, 10);
	Debug.Log("Am I doing stuff?" + rnd);
	rigidbody2D.velocity.x = rnd;
	*/
	
	if (direction == 1) {
	  direction = -1;
	} else if(direction == -1) {
	  direction = 1;
	}
	
	rigidbody2D.velocity.x = direction * VELOCITY;
  }
}