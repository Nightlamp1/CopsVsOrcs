using UnityEngine;
using System.Collections;

public delegate void MusicStartedEventHandler (float musicLength, bool blocking);
public delegate void MusicEndedEventHandler   (float musicLength, bool blocking);
public delegate void SFXStartedEventHandler   (float sfxLength,   bool blocking);
public delegate void SFXEndedEventHandler     (float sfxLength,   bool blocking);


public class AudioManager : MonoBehaviour {
  private         int           lastLoadedLevel;
  public          AudioSource   musicSource;
  public          AudioSource   sfxSource;
  public          AudioClip     mainMenuJingle;
  public          AudioClip     endlessRunJingle;
  public          AudioClip     deathJingle;
  public          AudioClip     gameOverJingle;
  public          AudioClip     creditsJingle;
  public          AudioClip[]   firingSounds;

  private static  bool          initialized = false;
  private static  AudioManager  singleton;

  private         bool          musicSourceBlocking;
  private         bool          sfxSourceBlocking;
  private         bool          firingEnabled;

  public event MusicStartedEventHandler MusicStarted;
  public event MusicEndedEventHandler   MusicEnded;
  public event SFXStartedEventHandler   SFXStarted;
  public event SFXEndedEventHandler     SFXEnded;

	void Awake(){
    if (initialized) {
      Destroy(gameObject);
      return;
    }

    initialized = true;
    singleton = this;

    lastLoadedLevel = -1;

    musicSourceBlocking = false;
    sfxSourceBlocking = false;
    firingEnabled = false;

    setMute(false);

		DontDestroyOnLoad (gameObject);
	}

  void Update() {
    if (Application.loadedLevel == lastLoadedLevel) return;

    lastLoadedLevel = Application.loadedLevel;

    switch (Application.loadedLevel) {
      case GameVars.MAIN_MENU_SCENE:
        StartCoroutine(playMusic(mainMenuJingle));
        break;
      case GameVars.ENDLESS_RUN_SCENE:
        StartCoroutine(playMusic(endlessRunJingle));
        break;
      case GameVars.GAME_OVER_SCENE:
        StartCoroutine(playMusic(gameOverJingle));
        break;
      case GameVars.CREDITS_SCENE:
        StartCoroutine(playMusic(creditsJingle));
        break;
      default:
        break;
    }
  }

  public static AudioManager getInstance() {
    return singleton;
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

  public IEnumerator playMusic(AudioClip jingle, bool blocking = false) {
    if (jingle == null || (musicSourceBlocking && musicSource.isPlaying)) {

    } else {
      musicSourceBlocking = blocking;

      if (MusicStarted != null) MusicStarted(jingle.length, blocking);

      musicSource.clip = jingle;
      musicSource.Play();

      yield return new WaitForSeconds(jingle.length);

      if (MusicEnded != null) MusicEnded(jingle.length, blocking);

      musicSourceBlocking = false;
    }
  }

  public IEnumerator playSFX(AudioClip jingle, bool blocking = false) {
    if (jingle == null || (sfxSourceBlocking && sfxSource.isPlaying)) {

    } else {
      sfxSourceBlocking = blocking;
    
      if (SFXStarted != null) SFXStarted(jingle.length, blocking);

      sfxSource.clip = jingle;
      sfxSource.Play();

      yield return new WaitForSeconds(jingle.length);

      if (SFXEnded != null) SFXEnded(jingle.length, blocking);

      sfxSourceBlocking = false;
    }
  }

  // Call this method to trigger a firing sound
  public void playFiringSound() {
    if (!firingEnabled) return;

    StartCoroutine(playSFX(firingSounds[Random.Range(0, firingSounds.Length - 1)]));
  }

  public void playDeathJingle() {
    musicSource.Stop();

    StartCoroutine(playSFX(deathJingle, true));
  }

  public void setMusicBlocking(bool blocking) {
    musicSourceBlocking = blocking;
  }

  public void setSFXBlocking(bool blocking) {
    sfxSourceBlocking = blocking;
  }

  public void disableFiring() {
    firingEnabled = false;
  }

  public void enableFiring() {
    firingEnabled = true;
  }
}
