using UnityEngine;
using System.Collections;

public class Pistol : Activateable {
  float DEFAULT_ACTIVATE_COOLDOWN = 0.05f;
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
      m_cooldownActivate -= Time.deltaTime;
    }

    if (m_cooldownActivateAlternate > 0)
    {
      m_cooldownActivate -= Time.deltaTime;
    }
  }

  public static GameObject getPrefab()
  {
    if (staticPrefab == null)
    {
      new Pistol();
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
                                   gameObject.transform.position + (gameObject.transform.right) - gameObject.transform.up * 0.4f,
                                   gameObject.transform.rotation);

      b.transform.name = "bullet(Clone)";

      b.GetComponent<Bullet>().fire();
      AudioManager.getInstance().playFiringSound();
    }
  }

  public override void activateAlternate()
  {

  }
}
