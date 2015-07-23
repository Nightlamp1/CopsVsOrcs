using UnityEngine;
using System;
using System.Collections;
using GoogleMobileAds.Api;

public class AdManager : MonoBehaviour {
  public const string INTERSTITIAL_ADS        = "InterstitialAds";
  public const string AD_FAILED_TO_LOAD       = "AdFailedToLoad";
  public const string INTERSTITIAL_AD_SHOWN   = "InterstitialAdShown";
  public const string ADS_SHOWN_THIS_SESSION  = "AdsShownThisSession";

  public const int DEFAULT_MIN_SECONDS_BETWEEN_ADS = 120;

  private static AdManager singleton;
  private static bool initialized = false;

  private static  bool            frequencyTimerExpired;

  private         InterstitialAd  interstitial;
  private         bool            preloaded;

  private         int             adsShown;
  private         int             adsFailedToLoad;

  // Initialize an InterstitialAd.
#if UNITY_ANDROID
	public const string interstitialAdUnitId = "ca-app-pub-5012360525975215/8810625686";
#elif UNITY_IPHONE
  public const string interstitialAdUnitId = "ca-app-pub-5012360525975215/1092637281";
#else
  public const string interstitialAdUnitId = "ca-app-pub-5012360525975215/8810625686";
#endif

  void Awake() {
    if (initialized) {
      Destroy(gameObject);
      return;
    }

    initialized = true;
    singleton = this;
    frequencyTimerExpired = true;

    preloaded = false;

    DontDestroyOnLoad(gameObject);
  }

  void OnDestroy() {
    AdClicks.serialize();
  }

  void Start() {
    SceneManager.getInstance().SceneChanged += new SceneChangedEventHandler(SceneChange);
  }

  private void SceneChange(SceneManager.Scene oldScene, SceneManager.Scene newScene) {
    switch ((SceneManager.Scene) newScene) {
      case SceneManager.Scene.AD:
        if (!frequencyTimerExpired) {
          Debug.LogDebug("Frequency timer not yet expired.");
          SceneManager.LoadLevel(SceneManager.Scene.GAME_OVER);
          return;
        }

        if (!preloaded) {
          Debug.LogDebug("Not preloaded.");
          SceneManager.LoadLevel(SceneManager.Scene.GAME_OVER);
          return;
        }

        if (AdClicks.tooManyClicks()) {
          Debug.LogDebug("Too many clicks.");
          SceneManager.LoadLevel(SceneManager.Scene.GAME_OVER);
          return;
        }

        if (!getInterstitial().IsLoaded()) {
          Debug.LogDebug("Interstitial not yet loaded.");
          SceneManager.LoadLevel(SceneManager.Scene.GAME_OVER);
          return;
        }

        if (Application.isEditor) {
          Debug.LogDebug("Is editor.");
          SceneManager.LoadLevel(SceneManager.Scene.GAME_OVER);
          return;
        }

        frequencyTimerExpired = false;
        preloaded = false;

        getInterstitial().Show();

        ++adsShown;
        ReportingManager.LogEvent(
          INTERSTITIAL_ADS, INTERSTITIAL_AD_SHOWN, ADS_SHOWN_THIS_SESSION, adsShown);

        StartCoroutine(resetFrequencyTimer());

        StartCoroutine(nextScene());

        break;
      default:
        preloadInterstitial();

        break;
    }
  }

  private IEnumerator nextScene() {
    yield return new WaitForSeconds(5);

    if (SceneManager.getScene() == SceneManager.Scene.AD) {
      SceneManager.LoadLevel(SceneManager.Scene.GAME_OVER);
    }
  }

  private IEnumerator resetFrequencyTimer() {
    yield return new WaitForSeconds(DEFAULT_MIN_SECONDS_BETWEEN_ADS);

    frequencyTimerExpired = true;
  }

  // Returns a singleton instance of this class.
  public static AdManager getInstance() {
    if (singleton == null) {
      singleton = new AdManager();
    }

    return singleton;
  }

  public InterstitialAd getInterstitial() {
    if (interstitial == null) {
      interstitial = new InterstitialAd(interstitialAdUnitId);

      interstitial.AdLeftApplication += delegate(object sender, EventArgs args) {
        AdClicks.addClick();

        // If someone is just hitting the same ad over and over again, this can happen, and in order to
        //  protect us, I'll just quit the app at this point.  They can relaunch, but won't see anymore
        //  ads until they drop below the tooManyClicks() threshold.
        if (AdClicks.wayTooManyClicks()) {
          Application.Quit();
        }
      };

      interstitial.AdFailedToLoad += delegate(object sender, AdFailedToLoadEventArgs args) {
        preloaded = false;
        frequencyTimerExpired = false;

        ++adsFailedToLoad;
        ReportingManager.LogEvent(
          INTERSTITIAL_ADS, AD_FAILED_TO_LOAD, args.Message, adsFailedToLoad);
      };

      interstitial.AdClosed += delegate(object sender, EventArgs args) {
        SceneManager.LoadLevel(SceneManager.Scene.GAME_OVER);
      };
    }

    return interstitial;
  }

  void preloadInterstitial() {
    if (preloaded) return;

    preloaded = true;

    string[] testDevices = {
      "0EDE6C15F6AD443908050688F06D494F",
      "228DB787B1BA32D99786A227FF0256CC",
      "A7F4BFA8EFD8F7E09E23709A1D509B73",
      "E1AB6EF5FB621FA3D7EC54239C1814C1",
    };

    // Create an ad request.
    AdRequest.Builder adRequestBuilder = new AdRequest.Builder();

    for (int i = 0; i < testDevices.Length; ++i) {
      adRequestBuilder.AddTestDevice(testDevices[i]);
    }

    AdRequest request = adRequestBuilder.Build();
  
    if (interstitial != null) {
      interstitial.Destroy();
    }

    interstitial = null;

    // Load the interstitial with the request.
    getInterstitial().LoadAd(request);
  }
}
