using UnityEngine;
using System;
using System.Collections;
using GoogleMobileAds.Api;

public class AdManager : MonoBehaviour {
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
  public const string interstitialAdUnitId = "ca-app-pub-5012360525975215/7853108485";
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

  private void SceneChange(int oldScene, int newScene) {
    switch (newScene) {
      case GameVars.GAME_OVER_SCENE:
        if (!frequencyTimerExpired || !preloaded || AdClicks.tooManyClicks()) {
          return;
        }

        frequencyTimerExpired = false;
        preloaded = false;

        getInterstitial().Show();

        ++adsShown;
        SceneManager.getInstance().googleAnalytics.LogEvent(
          SystemInfo.operatingSystem, "InterstitialAdShown", "Shown", adsShown);

        StartCoroutine(resetFrequencyTimer());

        break;
      default:
        preloadInterstitial();

        break;
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
        SceneManager.getInstance().googleAnalytics.LogEvent(
          SystemInfo.operatingSystem, "AdFailedToLoad", args.Message, adsFailedToLoad);
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
    print ("========== Preloaded interstitial ==========");
  }
}
