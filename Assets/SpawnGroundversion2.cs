using UnityEngine;
using System.Collections;

public class SpawnGroundversion2 : MonoBehaviour {
	public GameObject[] platform;

  private float nextLower = 0;
  private float nextMiddl = 0;
  private float nextUpper = 0;

  private float timeMIN = 1f;
  private float timeMAX = 3f;

	// Use this for initialization
	void Start () {
    nextLower = Time.timeSinceLevelLoad;
    nextMiddl = Time.timeSinceLevelLoad;
    nextUpper = Time.timeSinceLevelLoad;
	}

  void Update()
  {
    if (!GameVars.getInstance().getUserHasStarted()) {
      return;
    }
		if (Application.loadedLevel != 1) {
			nextLower = 0;
			nextMiddl = 0;
			nextUpper = 0;
		}

    if (nextLower < Time.timeSinceLevelLoad)
    {
      Spawn (-5);
      nextLower += nextTime();
    }

    if (nextMiddl < Time.timeSinceLevelLoad)
    {
      Spawn (0);
      nextMiddl += nextTime();
    }

    if (nextUpper < Time.timeSinceLevelLoad)
    {
      Spawn (5);
      nextUpper += nextTime();
    }
		if (Application.loadedLevel != 1)
			GetComponent<SpawnGroundversion2> ().enabled = false;
  }

  float nextTime()
  {
    return Random.Range (timeMIN, timeMAX);
  }
	
	// Update is called once per frame
	void Spawn(int yOffset) {
    GameObject go = (GameObject)
      Instantiate (
        platform [Random.Range (platform.GetLowerBound(0), platform.GetUpperBound(0) + 1)], 
        new Vector3 (this.transform.position.x + 23, yOffset, 10), Quaternion.identity);

    if (go.gameObject.tag == "Passable") {
      go.gameObject.AddComponent <Onewayplat>();
    }
	}
}
