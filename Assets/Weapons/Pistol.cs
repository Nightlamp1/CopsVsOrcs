using UnityEngine;
using System.Collections;

public class Pistol : Activateable {
  int DEFAULT_ACTIVATE_COOLDOWN = 150;
  public GameObject prefab;
  public static GameObject staticPrefab;
  
  public Pistol()
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
    if (m_cooldownActivate > 0)
    {
      m_cooldownActivate -= 1;
    }

    if (m_cooldownActivateAlternate > 0)
    {
      m_cooldownActivateAlternate -= 1;
    }
  }
  
  public static GameObject getPrefab()
  {
    if (staticPrefab == null) 
    {
      Debug.Log ("New Pistol");
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
