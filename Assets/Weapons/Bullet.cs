using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
  bool facingRight = true;
  public GameObject prefab;
  public static GameObject staticPrefab;
  public float m_range;

  public Bullet()
  {
    if (staticPrefab == null)
    {
      staticPrefab = this.prefab;
    }
  }

  void OnCollisionEnter2D(Collision2D other)
  {
    Destroy (gameObject);
  }

	// Use this for initialization
	void Start () 
  {
    Destroy(gameObject, m_range);
	}
	
	// Update is called once per frame
	void Update () 
  {
  }
  
  public void setFacingRight(bool p_facingRight)
  {
    this.facingRight = p_facingRight;
  }

  public static GameObject getPrefab()
  {
    return staticPrefab;
    //if (prefab == null)
    {
      /*
      SpriteRenderer l_spriteRenderer;
      Rigidbody2D l_rigidBody2D;
      
      prefab = new GameObject();
      
      l_spriteRenderer = prefab.AddComponent<SpriteRenderer>();
      
      l_spriteRenderer.sprite = Sprite.Create((Texture2D) Resources.Load("Weapons/Bullet"), 
                                              new Rect(Prefabs.DEF_X, Prefabs.DEF_Y, 5f, 25f), 
                                              Vector2.right);

      l_rigidBody2D = prefab.AddComponent<Rigidbody2D>();

      l_rigidBody2D.mass = 1;
      l_rigidBody2D.drag = 0;
      l_rigidBody2D.angularDrag = 0.05f;
      l_rigidBody2D.gravityScale = 0;
      l_rigidBody2D.fixedAngle = true;
      l_rigidBody2D.isKinematic = false;

      prefab.AddComponent<Bullet>();
      */

      //prefab = 
    }
    
    //return prefab;
  }
  
  public void fire()
  {
    this.rigidbody2D.AddForce(gameObject.transform.right * 1000f, 0);
  }
}
