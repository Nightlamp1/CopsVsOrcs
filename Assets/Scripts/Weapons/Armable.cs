using UnityEngine;
using System.Collections;

public class Armable : MonoBehaviour {
  int m_handsRequired;
  GameObject m_arm;

	// Use this for initialization
	void Start () {
	  
	}
	
	// Update is called once per frame
	void Update () {
	  
	}

  public bool equip (string p_arm)
  {
    try
    {
      if (m_arm != null)
      {
        Destroy (m_arm);
      }

      if (p_arm == "")
      {
        // Equip nothing

        return true;
      }

      m_arm = (GameObject) Instantiate(Resources.Load("Prefabs/" + p_arm + "Prefab"), 
                                       gameObject.transform.position, 
                                       gameObject.transform.rotation);

      m_arm.transform.Translate(Vector3.right * 0.5f);

      m_arm.transform.parent = gameObject.transform;
    } 
    catch (UnityException ex)
    {
      Debug.Log ("Failed to equip " + p_arm + "\n" + ex.Message);

      return false;
    }

    return true;
  }

  public void activate()
  {
    if (m_arm == null) return;

    getActivateable().activate();
  }

  public void activateAlternate()
  {
    if (m_arm == null) return;
    
    getActivateable().activateAlternate();
  }

  public Activateable getActivateable()
  {
    return m_arm.GetComponent<Activateable>();
  }
}
