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
      new Rifle();
    }

    return staticPrefab;
  }

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
