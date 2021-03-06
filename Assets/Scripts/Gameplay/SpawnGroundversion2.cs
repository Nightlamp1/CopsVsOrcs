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
	void OnLevelWasLoaded (int level) {
    if ((SceneManager.Scene) level == SceneManager.Scene.ENDLESS_RUN) {
			nextLower = 0;
			nextMiddl = 0;
			nextUpper = 0;
			Time.timeScale = 0;
			GameVars.getInstance().setUserHasStarted(false);

		}
	}

  void Update()
  {
    if (!GameVars.getInstance().getUserHasStarted()) {
      return;
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
		if ((SceneManager.Scene) Application.loadedLevel != SceneManager.Scene.ENDLESS_RUN)
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
