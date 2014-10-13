using UnityEngine;
using System.Collections;

public class Rifle : Activateable {
  int DEFAULT_ACTIVATE_COOLDOWN = 600;
  public GameObject prefab;
  public static GameObject staticPrefab;

  public Rifle()
  {
    if (staticPrefab == null)
    {
      staticPrefab = this.prefab;
    }
  }

	// Use this for initialization
	void Start () {
    m_hasActivate = true;
    m_hasActivateAlternate = false;
	}
	
	// Update is called once per frame
  void Update () {
    m_cooldownActivate -= 1;
    m_cooldownActivateAlternate -= 1;
	}

  public static GameObject getPrefab()
  {
    if (staticPrefab == null) 
    {
      Debug.Log ("New Rifle");
      new Rifle();
    }

    return staticPrefab;
  }
  /*
  public static GameObject getPrefab()
  {
    if (prefab == null)
    {
      SpriteRenderer l_spriteRenderer;

      prefab = new GameObject();

      l_spriteRenderer = prefab.AddComponent<SpriteRenderer>();

      l_spriteRenderer.sprite = Sprite.Create((Texture2D) Resources.Load("Weapons/Rifle"), 
                                              new Rect(Prefabs.DEF_X, Prefabs.DEF_Y, 25f, 325f), 
                                              Vector2.right);

      prefab.AddComponent<Rifle>();
    }

    return prefab;
  }
  */

  public override void activate()
  {
    if (canActivate())
    {
      m_cooldownActivate = DEFAULT_ACTIVATE_COOLDOWN;

      GameObject b;

      b = (GameObject) Instantiate(Resources.Load("Prefabs/BulletPrefab"), 
                                   gameObject.transform.position + (gameObject.transform.right * gameObject.transform.localScale.x), 
                                   gameObject.transform.rotation);

      b.transform.name = "bullet(Clone)";

      b.GetComponent<Bullet>().fire();
    }
  }

  public override void activateAlternate()
  {

  }
}
