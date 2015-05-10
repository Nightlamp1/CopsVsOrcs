using UnityEngine;
using System.Collections;
using GoogleMobileAds.Api;

public class PrefetchAd : MonoBehaviour {
  protected BannerView banner;
  protected InterstitialAd interstitial;

  protected bool preloadedBanner;
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
    preloadedBanner = false;
    preloadedInterstitial = false;

#if BANNER
    getBanner();
#elif ! NOADS
    getInterstitial();
#endif
  }

  void Update() {
#if BANNER
    preloadBanner();
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
      banner = new BannerView(
        adUnitId, AdSize.Banner, AdPosition.Bottom);
      preloadedBanner = false;
    }

    return banner;
  }

  void preloadBanner() {
    if (preloadedBanner) return;

    preloadedBanner = true;
    //ca-app-pub-5012360525975215/8810625686
    // Create an empty ad request.
    AdRequest request = new AdRequest.Builder().AddTestDevice("0EDE6C15F6AD443908050688F06D494F").Build();
    
    // Load the banner with the request.
    getBanner().LoadAd(request);
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
      preloadedBanner = false;
    }
  }

  public bool showInterstitial() {
    if (lastAdTime.AddSeconds(minSecondsBetweenAds) > System.DateTime.Now) {
      print ("========== Refusing to display another ad within " + minSecondsBetweenAds + " ==========");
      return false;
    }

    lastAdTime = System.DateTime.Now;

    getInterstitial().Show();
    print ("========== Interstitial should be visible ==========");
    return true;
  }

  public void resetInterstitial() {
    preloadedInterstitial = false;
  }
}
