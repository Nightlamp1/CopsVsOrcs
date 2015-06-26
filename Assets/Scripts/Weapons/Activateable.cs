using UnityEngine;
using System.Collections.Generic;

public abstract class Activateable : MonoBehaviour {
  protected bool m_hasActivate;
  protected bool m_hasActivateAlternate;

  protected float m_cooldownActivate;
  protected float m_cooldownActivateAlternate;

  public Transform spawnPosition;

	// Use this for initialization
	void Start () {
    m_hasActivate = false;
    m_hasActivateAlternate = false;

    m_cooldownActivate = 0;
    m_cooldownActivateAlternate = 0;
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
