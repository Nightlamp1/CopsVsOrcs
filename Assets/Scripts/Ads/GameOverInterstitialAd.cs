using UnityEngine;
using System.Collections;
using GoogleMobileAds.Api;

public class GameOverInterstitialAd : MonoBehaviour 
{
  double secondsLeft;

  PrefetchAd pre;
  int lastLoadedLevel;

  void Awake() {
    DontDestroyOnLoad(transform.gameObject);
  }

	// Use this for initialization
  void Start () {
    pre = PrefetchAd.get();

    // Only load the InterstitialAd on Level 2 (The GameOver Scene)
    if (Application.loadedLevel == GameVars.GAME_OVER_SCENE) {
      pre.getInterstitial().AdClosed += HandleAdClosed;
      pre.getInterstitial().AdFailedToLoad += HandleAdFailedToLoad;
      pre.getInterstitial().AdLoaded += HandleAdLoaded;
    }
  }

  void Update() {
    if (Application.loadedLevel != lastLoadedLevel) {
      lastLoadedLevel = Application.loadedLevel;

      if (Application.loadedLevel == GameVars.GAME_OVER_SCENE) {
        if (pre.getInterstitial().IsLoaded()) {
          print("========== Ad is loaded ==========");
          if (! pre.showInterstitial()) {
            print("========== Did not show interstitial ==========");
          }
        }
      }
    }

    lastLoadedLevel = Application.loadedLevel;
  }

  void HandleAdClosed (object sender, System.EventArgs e)
  {
    print("========== Ad closed ==========");
  }

  void HandleAdFailedToLoad (object sender, AdFailedToLoadEventArgs e)
  {
    print("========== AdFailedToLoad ==========");
  }

  void HandleAdLoaded(object sender, System.EventArgs e) {
    print("========== Ad loaded ==========");
  }
}
