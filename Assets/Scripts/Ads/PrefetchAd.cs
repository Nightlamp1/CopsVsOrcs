using UnityEngine;
using System.Collections;
using GoogleMobileAds.Api;

public class PrefetchAd : MonoBehaviour {
  public const int DEFAULT_MIN_SECONDS_BETWEEN_ADS = 120;

  protected InterstitialAd interstitial;

  protected bool preloadedInterstitial;

  private static PrefetchAd singleton;
  protected System.DateTime lastAdTime;
  private bool interstitialAdsEnabled;

  protected int minSecondsBetweenAds;

  // Initialize an InterstitialAd.
#if UNITY_ANDROID
	public const string interstitialAdUnitId = "ca-app-pub-5012360525975215/8810625686";
#elif UNITY_IPHONE
  public const string interstitialAdUnitId = "ca-app-pub-5012360525975215/7853108485";
#else
  public const string interstitialAdUnitId = "ca-app-pub-5012360525975215/8810625686";
#endif

  void Start() {
    if (singleton != null) {
      print ("========== Maybe we're wasting our time and destroying the old singleton a lot? ==========");
      singleton.destroyAds();
    }

    minSecondsBetweenAds = DEFAULT_MIN_SECONDS_BETWEEN_ADS;

    interstitialAdsEnabled = true;

    lastAdTime = new System.DateTime(1970, 1, 1);

    singleton = this;
    preloadedInterstitial = false;

    if (interstitialAdsEnabled) {
      getInterstitial();
    }
  }

  void Update() {
    if (interstitialAdsEnabled) {
      preloadInterstitial();
    }
  }

  // Returns a singleton instance of this class.
  public static PrefetchAd get() {
    if (singleton == null) {
      singleton = new PrefetchAd();
    }

    return singleton;
  }

  void Awake() {
    DontDestroyOnLoad(transform.gameObject);
  }

  public int getMinSecondsBetweenAds() {
    return minSecondsBetweenAds;
  }

  public void setMinSecondsBetweenAds(int seconds) {
    minSecondsBetweenAds = seconds;
  }

  public void setInterstitialAdsEnabled(bool pEnable) {
    interstitialAdsEnabled = pEnable;
  }

  public bool getInterstitialAdsEnabled() {
    return interstitialAdsEnabled;
  }

  public InterstitialAd getInterstitial() {
    if (interstitial == null) {
      interstitial = new InterstitialAd(interstitialAdUnitId);
      preloadedInterstitial = false;
    }

    return interstitial;
  }

  void preloadInterstitial() {
    string[] testDevices = {
      "0EDE6C15F6AD443908050688F06D494F",
      "228DB787B1BA32D99786A227FF0256CC",
      "A7F4BFA8EFD8F7E09E23709A1D509B73",
      "E1AB6EF5FB621FA3D7EC54239C1814C1",
    };

    if (!interstitialAdsEnabled || preloadedInterstitial) return;

    preloadedInterstitial = true;

    // Create an ad request.
    AdRequest.Builder adRequestBuilder = new AdRequest.Builder();

    for (int i = 0; i < testDevices.Length; ++i) {
      adRequestBuilder.AddTestDevice(testDevices[i]);
    }

    AdRequest request = adRequestBuilder.Build();

    // Load the interstitial with the request.
    getInterstitial().LoadAd(request);
    print ("========== Preloaded interstitial ==========");
  }

  void destroyAds() {
    if (interstitial != null) {
      getInterstitial().Destroy();
      preloadedInterstitial = false;
    }
  }

  public bool showInterstitial() {
    if (!interstitialAdsEnabled) return false;
    if (lastAdTime.AddSeconds(minSecondsBetweenAds) > System.DateTime.Now) {
      print ("========== Refusing to display another ad within " + minSecondsBetweenAds + " ==========");
      return false;
    }

    // Prevent this function from running multiple times.
    interstitialAdsEnabled = false;

    print("========== Ads - Displaying a new interstitial ad because " +
          lastAdTime.AddSeconds(minSecondsBetweenAds) + " < " +
          System.DateTime.Now + " ==========");

    lastAdTime = System.DateTime.Now;

    getInterstitial().Show();
    print ("========== Interstitial should be visible ==========");

    interstitialAdsEnabled = true;

    return true;
  }

  public void resetInterstitial() {
    preloadedInterstitial = false;
  }
}
