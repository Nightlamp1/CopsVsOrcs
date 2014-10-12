using UnityEngine;
using System.Collections;

public class GroundEnemyHealth : MonoBehaviour {

	public int enemyHealth = 60;

	// Update is called once per frame
	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.transform.tag == "Bullet") 
		{
			enemyHealth -= 10;
			Debug.Log("Enemy Health is " + enemyHealth);
			Destroy(other.gameObject);
		}
		if (enemyHealth <= 0) 
		{
			Destroy(this.gameObject);
		}
	}





}
