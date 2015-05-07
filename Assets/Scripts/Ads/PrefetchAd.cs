#define BANNER

using UnityEngine;
using System.Collections;
using GoogleMobileAds.Api;

public class PrefetchAd : MonoBehaviour {
  protected BannerView banner;
  protected InterstitialAd interstitial;

  protected bool preloadedInterstitial;

  private static PrefetchAd singleton;
  protected System.DateTime lastAdTime;

  [SerializeField] public int minSecondsBetweenAds;

  // Initialize an InterstitialAd.
#if UNITY_ANDROID
  public const string adUnitId = "ca-app-pub-5012360525975215/8810625686";
#elif UNITY_IPHONE
  public const string adUnitId = "INSERT_IOS_INTERSTITIAL_AD_UNIT_ID_HERE";
#else
  public const string adUnitId = "unexpected_platform";
#endif

  void Start() {
    if (singleton != null) {
      print ("========== Maybe we're wasting our time and destroying the old singleton a lot? ==========");
      singleton.destroyAds();
    }

    lastAdTime = new System.DateTime(1970, 1, 1);

    singleton = this;
    preloadedInterstitial = false;

#if BANNER
#elif ! NOADS
    getInterstitial();
#endif
  }

  void Update() {
#if BANNER
#elif ! NOADS
    preloadInterstitial();
#endif
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

  public InterstitialAd getInterstitial() {
    if (interstitial == null) {
      interstitial = new InterstitialAd(adUnitId);
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
      adUnitId, AdSize.Banner, AdPosition.Bottom);

    return banner;
  }

  // Do nothing
  void preloadBanner() {
    return;
    /*
    if (preloadedBanner) return;

    preloadedBanner = true;
    */
  }

  void preloadInterstitial() {
    if (preloadedInterstitial) return;

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

  public void showInterstitial() {
    if (lastAdTime.AddSeconds(minSecondsBetweenAds) > System.DateTime.Now) {
      print ("========== Refusing to display another ad within " + minSecondsBetweenAds + " ==========");
      return;
    }

    lastAdTime = System.DateTime.Now;

    getInterstitial().Show();
    print ("========== Interstitial should be visible ==========");
  }

  public void showBanner() {
    if (banner != null) {
      print ("========== Destroying old banner ==========");
      getNewBanner();
    } else {
      print ("========== Showing new banner ==========");
    }
    
    // Load the banner with the request.
    getBanner().LoadAd(new AdRequest.Builder().AddTestDevice("0EDE6C15F6AD443908050688F06D494F").Build());
    getBanner ().Show ();
  }

  public void resetInterstitial() {
    preloadedInterstitial = false;
  }

  public void resetBanner() {
    getBanner ().Hide();
    getBanner ().Destroy ();
  }
}
