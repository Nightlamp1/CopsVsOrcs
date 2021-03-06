using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
  public GameObject prefab;
  public static GameObject staticPrefab;
  public float m_range;
  int travelDistance = 0;
  public Bullet()
  {
    if (staticPrefab == null)
    {
      staticPrefab = this.prefab;
    }
  }

  void OnCollisionEnter2D(Collision2D other)
  {
    Destroy (gameObject, 0.05f);
  }

	// Use this for initialization
	void Start () 
  {
    StatsManager.incLong(STATS.TOTAL_DONUTS_SHOT, StatsManager.StatSig.CUMULATIVE);
    StatsManager.incLong(STATS.PEAK_DONUTS_SHOT, StatsManager.StatSig.MAX);
    Destroy(gameObject, m_range);
	}
	
	// Update is called once per frame
  void FixedUpdate()
  {
    travelDistance += 1;
    if (travelDistance > 75) {
			Destroy (gameObject);
			GameVars.getInstance().setComboOrcKills(0);
			GameVars.getInstance().setComboMultiplier(1);
		}
  }

  public static GameObject getPrefab()
  {
    if (staticPrefab == null) 
    {
      new Bullet();
    }

    return staticPrefab;
  }
  
  public void fire()
  {
    this.GetComponent<Rigidbody2D>().AddForce(gameObject.transform.right * 1000f, 0);
  }
}
