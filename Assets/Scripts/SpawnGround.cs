using UnityEngine;
using System.Collections;

public class SpawnGround : MonoBehaviour {

	public GameObject[] LowerObj;
	public GameObject[] MidObj;
	public GameObject[] HighObj;
	public float spawnMin = 1f;
	public float spawnMax = 2f;
    public bool startSpawn = true;
	private float spawnControlMax = 7f;
	private float spawnControlMin = 0f;

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
    GameObject new_obj;
    
    new_obj = (GameObject) Instantiate(LowerObj[Random.Range (0, LowerObj.GetLength(0))], new Vector3(transform.position.x + 25, -4, 10), Quaternion.identity);
	new_obj = (GameObject) Instantiate(MidObj[Random.Range (0, MidObj.GetLength(0))], new Vector3(transform.position.x + 30, 0, 10), Quaternion.identity);
	startSpawn = false;

    Invoke ("Spawn", Random.Range (spawnMin, spawnMax));
	}


	void Spawn()
	{
		/*float spawnControl = Random.Range (spawnControlMin, spawnControlMax);
		if (spawnControl < 0.3f) 
		{
			SpawnHigh ();
		} 
		else if (spawnControl < 0.6f) 
		{
			SpawnLow ();
		}
		else if (spawnControl < 1f) 
		{
			SpawnMid ();
		}
		else if (spawnControl < 2.5f) 
		{
			SpawnHigh();
			SpawnMid ();
		}
		else if (spawnControl < 4f) 
		{
			SpawnLow ();
			SpawnMid ();
		}
		else if (spawnControl < 5.5f) 
		{
			SpawnLow ();
			SpawnHigh ();
		}
		else if (spawnControl < 7f) 
		{
			SpawnLow ();
			SpawnMid ();
			SpawnHigh ();
		}*/

		Invoke ("SpawnLow", Random.Range(1f,2f));
		Invoke ("SpawnMid", Random.Range(2f,3f));
		Invoke ("SpawnHigh", Random.Range(2f,5f));
		Invoke ("Spawn", Random.Range(spawnMin,spawnMax));


	}



	/*the following block of code is used to actually instantiate the objects based on y height*/


	void SpawnLow()//Spawn objects in the lower tier
	{
	  GameObject new_low;
	  new_low = (GameObject) Instantiate(LowerObj[Random.Range (0, LowerObj.GetLength(0))], new Vector3(transform.position.x + 19 + Random.Range(-1,3), -4, 10), Quaternion.identity);
	}

	void SpawnMid()//Spawn objects in the middle tier
	{
	  GameObject new_mid;
	  new_mid = (GameObject)Instantiate (MidObj [Random.Range (0, MidObj.GetLength (0))], new Vector3 (transform.position.x + 19 + Random.Range(-1,3), 0, 10), Quaternion.identity);
	}

	void SpawnHigh()//Spawn objects in the highest tier
	{
		GameObject new_high;
		new_high = (GameObject)Instantiate (HighObj [Random.Range (0, HighObj.GetLength (0))], new Vector3 (transform.position.x + 19 + Random.Range(-1,3), 4, 10), Quaternion.identity);
	}
}
