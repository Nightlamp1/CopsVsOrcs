using UnityEngine;
using System.Collections;
using GoogleMobileAds.Api;

public class GameOverInterstitialAd : MonoBehaviour 
{
  double secondsLeft;

  int loadedLevel = 0;
   
  PrefetchAd pre;

  public void hideBannerAd() {
    pre.hideBannerAd();
  }

  void Awake() {
    DontDestroyOnLoad(transform.gameObject);
  }

	// Use this for initialization
	void Start () {
    loadedLevel = Application.loadedLevel;
    
    pre = PrefetchAd.get();

    pre.getBanner().AdLoaded += HandleAdLoaded;

    // Only load the InterstitialAd on Level 2 (The Interstitial Ad Scene)
    if (Application.loadedLevel == 2) {
      if (pre.getInterstitial().IsLoaded()) {
        pre.getInterstitial().AdClosed += HandleAdClosed;

        if (! pre.showInterstitial()) {
          print("========== Failed to show interstitial ==========");
          endScene();
        }
      }
    }
	}

  void Update() {
    if (Application.loadedLevel == 2 && !pre.getInterstitialAdsEnabled()) {
      print("========== level 2 ==========");
      endScene();
    } else if (Application.loadedLevel != 3) {
      hideBannerAd();
    } else if (loadedLevel != Application.loadedLevel) {
      pre.showBanner();
    }

    loadedLevel = Application.loadedLevel;
  }

  void HandleAdLoaded(object sender, System.EventArgs e) {
    if (Application.loadedLevel == 3 && pre.getBannerAdsEnabled()) {
      pre.showBanner();
    }
  }

  void HandleAdClosed (object sender, System.EventArgs e)
  {
    print("========== Ad closed ==========");
    endScene();
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
}
