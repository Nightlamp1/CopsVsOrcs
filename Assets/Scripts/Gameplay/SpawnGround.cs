using UnityEngine;
using System.Collections;

public class SpawnGround : MonoBehaviour {

	public GameObject[] LowerObj;
	public GameObject[] MidObj;
	public GameObject[] HighObj;
	public float spawnMin = 6f;
	public float spawnMax = 6f;
  public bool startSpawn = true;

	// Use this for initialization
	void Start () {
    if (startSpawn)
    {
		  InitialSpawn ();
    }
    else
    {
      Invoke ("Spawn", Random.Range (spawnMin, spawnMax));
    }
	}

	void InitialSpawn()
   {
	  startSpawn = false;

    Invoke ("Spawn", Random.Range (spawnMin, spawnMax));
	}


	void Spawn()
	{
		Invoke ("SpawnLow", Random.Range(3f,4f));
		Invoke ("SpawnMid", Random.Range(2f,3f));
		Invoke ("SpawnHigh", Random.Range(4f,5f));
		Invoke ("Spawn", Random.Range(spawnMin,spawnMax));
	}

	void SpawnLow()//Spawn objects in the lower tier
	{
	  Instantiate(LowerObj[Random.Range (0, LowerObj.GetLength(0))], new Vector3(transform.position.x + 23 + Random.Range(-1,1), -4, 10), Quaternion.identity);
	}

	void SpawnMid()//Spawn objects in the middle tier
	{
	  Instantiate (MidObj [Random.Range (0, MidObj.GetLength (0))], new Vector3 (transform.position.x + 23 + Random.Range(-1,0), 0, 10), Quaternion.identity);
	}

	void SpawnHigh()//Spawn objects in the highest tier
	{
		Instantiate (HighObj [Random.Range (0, HighObj.GetLength (0))], new Vector3 (transform.position.x + 23 + Random.Range(-1,1), 4, 10), Quaternion.identity);
	}
}
