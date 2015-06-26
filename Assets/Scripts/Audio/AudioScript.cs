﻿using UnityEngine;
using System.Collections;

public class AudioScript : MonoBehaviour {
  private         int         lastLoadedLevel;
  public          AudioSource musicSource;
  public          AudioSource sfxSource;
  public          AudioClip   mainMenuJingle;
  public          AudioClip   endlessRunJingle;
  public          AudioClip   deathJingle;
  public          AudioClip   gameOverJingle;
  public          AudioClip   creditsJingle;
  public          AudioClip[] firingSounds;

  private static  bool        initialized = false;
  private static  AudioScript singleton;

	void Awake(){
    if (initialized) {
      Destroy(gameObject);
      return;
    }

    initialized = true;
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

  public static AudioScript getInstance() {
    return singleton;
  }

  void play(AudioClip jingle) {
    if (jingle == null) {
      return;
    }

    musicSource.clip = jingle;
    musicSource.Play();
  }

  // Use this if you need to be able to set the mute state to an absolute value (true/false)
  public void setMute(bool state) {
    musicSource.mute = state;
    sfxSource.mute   = state;
  }

  // Use this if you just need to toggle the mute state from what it is now (true/false) to
  //  what you want it to be (false/true)
  public void toggleMute() {
    musicSource.mute = (!musicSource.mute);
    sfxSource.mute   = (!sfxSource.mute);
  }

  // Call this method to trigger a firing sound
  public void playFiringSound() {
    sfxSource.clip = firingSounds[Random.Range(0, firingSounds.Length - 1)];
    sfxSource.Play();
  }
}
