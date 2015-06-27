﻿using UnityEngine;
using System.Collections;

public class Restarter : MonoBehaviour {

	void OnTriggerEnter2D (Collider2D other)
	{
		if(other.tag == "Player")
      SceneManager.LoadLevel(Application.loadedLevel);
	}
}
