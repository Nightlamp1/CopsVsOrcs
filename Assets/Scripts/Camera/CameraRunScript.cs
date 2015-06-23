using UnityEngine;
using System.Collections;

public class CameraRunScript : MonoBehaviour {

	public GameObject player;
	public GameObject Cop;

	void Start ()
	{
		if (!GameObject.Find ("HeroCop(Clone)")) {
			player = Instantiate (Cop, new Vector3 (-6f, -3.4f, 0f), Quaternion.identity) as GameObject;
		} else {
			player = GameObject.Find ("HeroCop(Clone)");
			GameObject.Find ("HeroCop(Clone)").transform.position = new Vector3 (-6f, -3.4f, 0f);
		}
	}


	// Update is called once per frame
	void Update () 
	{
		transform.position = new Vector3(player.transform.position.x +6,0,-10);
	}
}
