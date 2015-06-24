﻿using UnityEngine;
using System.Collections;

public class MusicScript : MonoBehaviour {
  private static  MusicScript singleton = null;

  private         int         lastLoadedLevel;
  public          AudioSource musicSource;
  public          AudioSource sfxSource;
  public          AudioClip   mainMenuJingle;
  public          AudioClip   endlessRunJingle;
  public          AudioClip   deathJingle;
  public          AudioClip   gameOverJingle;
  public          AudioClip   creditsJingle;

	void Awake(){
    if (singleton != null && singleton != this) {
      Destroy(gameObject);
      return;
    }

    singleton = this;
    lastLoadedLevel = -1;

		DontDestroyOnLoad (gameObject);
	}

  void Update() {
    if (Application.loadedLevel == lastLoadedLevel) return;

    lastLoadedLevel = Application.loadedLevel;

    switch (Application.loadedLevel) {
      case GameVars.MAIN_MENU_SCENE:
        play(mainMenuJingle);
        break;
      case GameVars.ENDLESS_RUN_SCENE:
        play(endlessRunJingle);
        break;
      case GameVars.GAME_OVER_SCENE:
        play(gameOverJingle);
        break;
      case GameVars.CREDITS_SCENE:
        play(creditsJingle);
        break;
      default:
        break;
    }
  }

  void play(AudioClip jingle) {
    if (jingle == null) {
      return;
    }

    musicSource.clip = jingle;
    musicSource.Play();
  }
}
