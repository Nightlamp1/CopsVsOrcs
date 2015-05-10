#define BANNER

using UnityEngine;
using System.Collections;
using GoogleMobileAds.Api;

public class GameOverInterstitialAd : MonoBehaviour 
{
  System.DateTime startTime;
  double secondsLeft;
#if BANNER
  BannerView banner;

  int loadedLevel = 0;
  bool hidden = false;

  #if UNITY_ANDROID
  public const string adUnitId = "ca-app-pub-5012360525975215/4791908484";
  #elif UNITY_IPHONE
  public const string adUnitId = "INSERT_IOS_INTERSTITIAL_AD_UNIT_ID_HERE";
  #else
  public const string adUnitId = "unexpected_platform";
  #endif
#else
  PrefetchAd pre;
#endif

  public void hideBannerAd() {
#if BANNER
    if (hidden) return;
    hidden = true;
    banner.Hide ();
#endif
  }

  void Awake() {
    DontDestroyOnLoad(transform.gameObject);
  }

	// Use this for initialization
	void Start () {
#if BANNER
    loadedLevel = Application.loadedLevel;
    banner = new BannerView(adUnitId, AdSize.Banner, AdPosition.Bottom);

    banner.AdLoaded += HandleAdLoaded;
#else 
    pre = PrefetchAd.get();

    // Only load the InterstitialAd on Level 2 (The Interstitial Ad Scene)
    if (Application.loadedLevel == 2) {
      if (pre.getInterstitial().IsLoaded()) {
        pre.getInterstitial().AdClosed += HandleAdClosed;

        if (! pre.showInterstitial()) {
          endScene();
        }
      }
      
      startTime = System.DateTime.Now;
    }
#endif
	}

#if BANNER
  void Update() {
    if (Application.loadedLevel == 2) {
      Application.LoadLevel (3);
    } else if (Application.loadedLevel != 3) {
      hideBannerAd();
    } else if (loadedLevel != Application.loadedLevel) {
      banner.LoadAd (new AdRequest.Builder().AddTestDevice("0EDE6C15F6AD443908050688F06D494F").Build());
    }

    loadedLevel = Application.loadedLevel;
  }

  void Destroy() {
    if (banner != null) {
      banner.AdLoaded -= HandleAdLoaded;
      banner.Destroy ();
      banner = null;
    }
  }

  void HandleAdLoaded(object sender, System.EventArgs e) {
    if (Application.loadedLevel == 3) {
      banner.Show();
      hidden = false;
    }
  }
#else
  void HandleAdClosed (object sender, System.EventArgs e)
  {
    endScene();
  }

	// Update is called once per frame
	void Update () {
    secondsLeft = (startTime.AddSeconds(5).Subtract(System.DateTime.Now)).TotalSeconds;

	  if (secondsLeft < 0) {
      endScene();
    }
	}

  void endScene() {
    if (Application.loadedLevel == 2) {
      pre.getInterstitial().AdClosed -= HandleAdClosed;
      pre.resetInterstitial();
      Application.LoadLevel(3);
    }
  }
  
  void OnGUI()
  {
    //GUI.Label (new Rect (Screen.width * 0.5f - 50, 20, 100, 30), "Seconds Left: " + (long) (secondsLeft + 1));
  }
#endif
}
