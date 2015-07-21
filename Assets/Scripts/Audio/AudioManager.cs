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

  public          int           mainMenuJingleScaling   = 255;
  public          int           endlessRunJingleScaling = 255;
  public          int           gameOverJingleScaling   = 255;
  public          int           creditsJingleScaling    = 255;
  public          int           firingSoundsScaling     = 255;
  public          int           deathJingleScaling      = 255;

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

		DontDestroyOnLoad (gameObject);

    if (PlayerPrefs.HasKey("muteState")) {
      musicSource.mute = (PlayerPrefs.GetInt("muteState") != 0);
      sfxSource.mute   = (PlayerPrefs.GetInt("muteState") != 0);
    }
	}

  void Update() {
    if (Application.loadedLevel == lastLoadedLevel) return;

    lastLoadedLevel = Application.loadedLevel;

    switch ((SceneManager.Scene) Application.loadedLevel) {
      case SceneManager.Scene.MAIN_MENU:
        StartCoroutine(playMusic(mainMenuJingle, mainMenuJingleScaling));
        break;
      case SceneManager.Scene.ENDLESS_RUN:
        StartCoroutine(playMusic(endlessRunJingle, endlessRunJingleScaling));
        break;
      case SceneManager.Scene.GAME_OVER:
        StartCoroutine(playMusic(gameOverJingle, gameOverJingleScaling));
        break;
      case SceneManager.Scene.CREDITS:
        StartCoroutine(playMusic(creditsJingle, creditsJingleScaling));
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

    PlayerPrefs.SetInt("muteState", (state ? 1 : 0));
  }

  // Use this if you just need to toggle the mute state from what it is now (true/false) to
  //  what you want it to be (false/true)
  public void toggleMute() {
    musicSource.mute = (!musicSource.mute);
    sfxSource.mute   = (!sfxSource.mute);

    PlayerPrefs.SetInt("muteState", (sfxSource.mute ? 1 : 0));
  }

  public IEnumerator playMusic(AudioClip jingle, int scaling = 255, bool blocking = false, bool events = false) {
    if (jingle == null || (musicSourceBlocking && musicSource.isPlaying)) {

    } else {
      musicSourceBlocking = blocking;

      if (MusicStarted != null && events) MusicStarted(jingle.length, blocking);

      musicSource.clip = jingle;
      musicSource.volume = (scaling / 255.0f);
      musicSource.Play();

      yield return new WaitForSeconds(jingle.length);

      if (MusicEnded != null && events) MusicEnded(jingle.length, blocking);

      musicSourceBlocking = false;
    }
  }

  public IEnumerator playSFX(AudioClip jingle, int scaling = 255, bool blocking = false, bool events = false) {
    if (jingle == null || (sfxSourceBlocking && sfxSource.isPlaying)) {

    } else {
      sfxSourceBlocking = blocking;
    
      if (SFXStarted != null && events) SFXStarted(jingle.length, blocking);

      sfxSource.clip = jingle;
      sfxSource.volume = (scaling / 255.0f);
      sfxSource.Play();

      yield return new WaitForSeconds(jingle.length);

      if (SFXEnded != null && events) SFXEnded(jingle.length, blocking);

      sfxSourceBlocking = false;
    }
  }

  // Call this method to trigger a firing sound
  public void playFiringSound() {
    if (!firingEnabled) return;

    StartCoroutine(playSFX(firingSounds[Random.Range(0, firingSounds.Length - 1)], firingSoundsScaling));
  }

  public void playDeathJingle() {
    musicSource.Stop();

    if (sfxSource.mute) {
      // We don't want the muted players to have to wait for the death jingle to finish.
      SFXEnded(deathJingle.length, true);
    } else {
      StartCoroutine(playSFX(deathJingle, deathJingleScaling, true, true));
    }
  }

  public void setMusicBlocking(bool blocking) {
    musicSourceBlocking = blocking;
  }

  public void setSFXBlocking(bool blocking) {
    sfxSourceBlocking = blocking;
  }

  public void setFiring(bool pFiringEnabled) {
    firingEnabled = pFiringEnabled;
  }

  public void disableFiring() {
    firingEnabled = false;
  }

  public void enableFiring() {
    firingEnabled = true;
  }
}
