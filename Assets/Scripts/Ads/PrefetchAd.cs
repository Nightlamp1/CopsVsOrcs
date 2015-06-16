using UnityEngine;
using System.Collections;
using GoogleMobileAds.Api;

public class PrefetchAd : MonoBehaviour {
  public const int DEFAULT_MIN_SECONDS_BETWEEN_ADS = 120;

  protected BannerView banner;
  protected InterstitialAd interstitial;

  protected bool preloadedInterstitial;

  private static PrefetchAd singleton;
  protected System.DateTime lastAdTime;
  private bool interstitialAdsEnabled;
  private bool bannerAdsEnabled;

  private bool bannerAdHidden;
  protected int minSecondsBetweenAds;

  // Initialize an InterstitialAd.
#if UNITY_ANDROID
	public const string bannerAdUnitId       = "ca-app-pub-5012360525975215/4791908484";
	public const string interstitialAdUnitId = "ca-app-pub-5012360525975215/4791908484";
#elif UNITY_IPHONE
  public const string bannerAdUnitId       = "";
  public const string interstitialAdUnitId = "";
#else
  public const string bannerAdUnitId       = "ca-app-pub-5012360525975215/4791908484";
  public const string interstitialAdUnitId = "ca-app-pub-5012360525975215/8810625686";
#endif

  void Start() {
    if (singleton != null) {
      print ("========== Maybe we're wasting our time and destroying the old singleton a lot? ==========");
      singleton.destroyAds();
    }

    minSecondsBetweenAds = DEFAULT_MIN_SECONDS_BETWEEN_ADS;

    bannerAdHidden = false;
    interstitialAdsEnabled = true;
    bannerAdsEnabled = false;

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

  public void setBannerAdsEnabled(bool pEnable) {
    bannerAdsEnabled = pEnable;
  }

  public bool getInterstitialAdsEnabled() {
    return interstitialAdsEnabled;
  }

  public bool getBannerAdsEnabled() {
    return bannerAdsEnabled;
  }

  public InterstitialAd getInterstitial() {
    if (interstitial == null) {
      interstitial = new InterstitialAd(interstitialAdUnitId);
      preloadedInterstitial = false;
    }
    
    return interstitial;
  }

  public BannerView getBanner() {
    if (banner == null) {
      return getNewBanner ();
    }

    return banner;
  }

  public BannerView getNewBanner() {
    if (banner != null) {
      banner.Destroy();
    }

    banner = new BannerView(
      bannerAdUnitId, AdSize.Banner, AdPosition.Bottom);

    return banner;
  }

  // Do nothing
  void preloadBanner() {
    return;
  }

  void preloadInterstitial() {
    if (!interstitialAdsEnabled || preloadedInterstitial) return;

    preloadedInterstitial = true;

    // Create an empty ad request.
    AdRequest request = new AdRequest.Builder().AddTestDevice("0EDE6C15F6AD443908050688F06D494F").Build();

    // Load the interstitial with the request.
    getInterstitial().LoadAd(request);
    print ("========== Preloaded interstitial ==========");
  }

  void destroyAds() {
    if (interstitial != null) {
      getInterstitial().Destroy();
      preloadedInterstitial = false;
    }
    
    if (banner != null) {
      getBanner().Destroy();
    }
  }

  public bool showInterstitial() {
    if (!interstitialAdsEnabled) return false;
    if (lastAdTime.AddSeconds(minSecondsBetweenAds) > System.DateTime.Now) {
      print ("========== Refusing to display another ad within " + minSecondsBetweenAds + " ==========");
      return false;
    }

    lastAdTime = System.DateTime.Now;

    getInterstitial().Show();
    print ("========== Interstitial should be visible ==========");
    return true;
  }

  public void showBanner() {
    if (!bannerAdsEnabled) return;

    if (banner != null) {
      print ("========== Destroying old banner ==========");
      getNewBanner();
    } else {
      print ("========== Showing new banner ==========");
    }

    bannerAdHidden = false;
    
    // Load the banner with the request.
    getBanner().LoadAd(new AdRequest.Builder().AddTestDevice("0EDE6C15F6AD443908050688F06D494F").Build());
    getBanner ().Show ();
  }

  public void resetInterstitial() {
    preloadedInterstitial = false;
  }

  public void resetBanner() {
    if (!bannerAdsEnabled) return;
    getBanner ().Hide();
    getBanner ().Destroy ();
  }

  public void hideBannerAd() {
    if (bannerAdHidden) return;
    bannerAdHidden = true;
    banner.Hide ();
  }
}
