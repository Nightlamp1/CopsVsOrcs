using UnityEngine;
using System.Collections.Generic;

public abstract class Activateable : MonoBehaviour {
  protected bool m_hasActivate;
  protected bool m_hasActivateAlternate;

  protected float m_cooldownActivate;
  protected float m_cooldownActivateAlternate;

  public Transform spawnPosition;

  public List<AudioClip> activateableSounds;

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

  public void addActivateableSound(AudioClip newSound)
  {
    if (activateableSounds == null) activateableSounds = new List<AudioClip>();

    activateableSounds.Add(newSound);
  }

  public void activateRandomSound()
  {
    if (activateableSounds == null || activateableSounds.Count == 0) return;

    if (activateableSounds.Count > 0) {
      AudioSource.PlayClipAtPoint(
        activateableSounds[Random.Range(0, activateableSounds.Count)],
        new Vector3());
    } else {
      print("No available sounds to randomly select.");
    }
  }

  public abstract void activate();

  public abstract void activateAlternate();
}
