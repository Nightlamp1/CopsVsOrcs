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
    nextLower = Time.time;
    nextMiddl = Time.time;
    nextUpper = Time.time;
	}

  void Update()
  {
    if (nextLower < Time.time)
    {
      Spawn (-5);
      nextLower += nextTime();
    }

    if (nextMiddl < Time.time)
    {
      Spawn (0);
      nextMiddl += nextTime();
    }

    if (nextUpper < Time.time)
    {
      Spawn (5);
      nextUpper += nextTime();
    }
  }

  float nextTime()
  {
    return Random.Range (timeMIN, timeMAX);
  }
	
	// Update is called once per frame
	void Spawn(int yOffset) {
    float l_yShift = 0;
    float l_x;
    float l_y;
    float l_z;

    GameObject go = (GameObject)
      Instantiate (
        platform [Random.Range (platform.GetLowerBound(0), platform.GetUpperBound(0) + 1)], 
        new Vector3 (this.transform.position.x + 23, yOffset, 10), Quaternion.identity);
    if (go.gameObject.tag == "Passable") {
      go.gameObject.AddComponent <Onewayplat>();
    }

    /*
    for (int i = 0; i < go.transform.childCount; ++i)
    {
      l_x = go.transform.GetChild(i).transform.localPosition.x;
      l_z = go.transform.GetChild(i).transform.localPosition.z;
      
      go.transform.GetChild(i).transform.localPosition = new Vector3(l_x, 0, l_z);

      if (i == 0)
      {
        l_yShift = go.transform.GetChild(i).transform.localPosition.y;
      }

      l_x = go.transform.GetChild(i).transform.localPosition.x;
      l_y = go.transform.GetChild(i).transform.localPosition.y;
      l_z = go.transform.GetChild(i).transform.localPosition.z;
      
      go.transform.GetChild(i).transform.localPosition = new Vector3(l_x, l_y - l_yShift, l_z);
    }
    */
	}
}
