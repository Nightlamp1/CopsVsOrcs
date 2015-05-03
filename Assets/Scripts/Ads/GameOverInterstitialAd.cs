using UnityEngine;
using System.Collections;
using GoogleMobileAds.Api;

public class GameOverInterstitialAd : MonoBehaviour 
{
  System.DateTime startTime;
  double secondsLeft;
  PrefetchAd pre;

	// Use this for initialization
	void Start () {
    pre = PrefetchAd.get();

#if BANNER
    pre.getBanner().Show();
#else 
    // Only load the InterstitialAd on Level 2 (The Interstitial Ad Scene)
    if (Application.loadedLevel == 2) {
      if (pre.getInterstitial().IsLoaded()) {
        pre.showInterstitial();
        pre.getInterstitial().AdClosed += HandleAdClosed;
      }
      
      startTime = System.DateTime.Now;
    }
#endif
	}
  
#if ! BANNER
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
