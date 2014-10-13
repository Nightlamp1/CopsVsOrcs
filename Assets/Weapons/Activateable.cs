using UnityEngine;
using System.Collections;

public abstract class Activateable : MonoBehaviour {
  protected bool m_hasActivate;
  protected bool m_hasActivateAlternate;

  protected int m_cooldownActivate;
  protected int m_cooldownActivateAlternate;

  public Transform spawnPosition;

	// Use this for initialization
	void Start () {
    m_hasActivate = false;
    m_hasActivateAlternate = false;

    m_cooldownActivate = 0;
    m_cooldownActivateAlternate = 0;
	}
	
	// Update is called once per frame
	void Update () {
    m_cooldownActivate -= 1;
    m_cooldownActivateAlternate -= 1;
  }
  
  public bool canActivate()
  {
    return (m_hasActivate && m_cooldownActivate <= 0);
  }
  
  public bool canActivateAlternate()
  {
    return (m_hasActivateAlternate && m_cooldownActivateAlternate <= 0);
  }

  public abstract void activate();

  public abstract void activateAlternate();
}
