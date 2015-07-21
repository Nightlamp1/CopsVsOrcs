using UnityEngine;
using System.Collections;

public class EnemyCollision : MonoBehaviour {
	Animator anim;
	RaycastHit2D MyRay;
	
	void Start()
	{
		anim = gameObject.GetComponent<Animator> ();
	}

	void FixedUpdate()
	{
		MyRay = Physics2D.Linecast(new Vector2(this.transform.position.x-1f,this.transform.position.y),new Vector2(this.transform.position.x - 3f,this.transform.position.y));
		Debug.DrawLine(new Vector3(this.transform.position.x-1f,this.transform.position.y),new Vector3(this.transform.position.x - 3f,this.transform.position.y));

		if (MyRay.collider == null)
		{
			return;
		}

		if (MyRay.collider.tag == "Player") 
		{
			anim.SetTrigger("Attack1");
		} 

	}

	
	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.tag == "Player") 
		{
			GameVars.getInstance().incrementOrcHits(1);
			GameVars.getInstance().setComboOrcKills(0);
			Physics2D.IgnoreCollision(other.collider, this.GetComponent<Collider2D>());
		} 
		else if (other.gameObject.tag == "Bullet") 
		{
			anim.SetTrigger("Death");
      gameObject.GetComponent<Orc>().hit();
			GameVars.getInstance().incrementOrcKills(1);
			GameVars.getInstance().incrementMScore(2);
			GameVars.getInstance().incrementcomboOrcKills(1);
			this.GetComponent<Collider2D>().enabled = false;
			Destroy (this.gameObject, 0.4f);
			Destroy (other.gameObject);
		}
	}
}
