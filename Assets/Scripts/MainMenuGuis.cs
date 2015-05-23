using UnityEngine;
using System.Collections;

public class MainMenuGuis : MonoBehaviour {
  private       float XPOS   = 0; // Set in OnGUI once.  Should be fixed...
  private       float YPOS   = 0; // Set in OnGUI once.  Should be fixed...

  private const float WIDTH  = 1000;
  private const float HEIGHT = 100;

	public GUISkin customSkin;
	public Texture2D startGame;
	public Texture2D optionButton;
	public Texture2D creditsButton;

  string moarAdsText;

	// Use this for initialization
	void Awake () 
	{

	}

  void Start() {
    moarAdsText = "Moar Ads Plz";
  }
	
	// Update is called once per frame
	void OnGUI()
	{
    XPOS = Screen.width * 0.665f;
    YPOS = Screen.height * 0.52f;

    if (GUI.Button (new Rect (XPOS, YPOS + 217, 190, 65), moarAdsText)) {
      if (PrefetchAd.get().getMinSecondsBetweenAds() == PrefetchAd.DEFAULT_MIN_SECONDS_BETWEEN_ADS) {
        print("========== Moar Ads ==========");
        PrefetchAd.get().setMinSecondsBetweenAds(1);
        moarAdsText = "Less Ads Plz";
      } else {
        print("========== Less Ads ==========");
        PrefetchAd.get().setMinSecondsBetweenAds(PrefetchAd.DEFAULT_MIN_SECONDS_BETWEEN_ADS);
        moarAdsText = "More Ads Plz";
      }
    }

	  if (GUI.Button (new Rect (XPOS, YPOS, WIDTH, HEIGHT), startGame, "")) 
    {
      if (GameVars.getInstance().getPlayerName() == "")
      {
        GameVars.getInstance().setPlayerName("not entered");
      }

			Application.LoadLevel(1);
		}

		GUI.Button (new Rect (XPOS, YPOS + 73, WIDTH, HEIGHT), optionButton ,"");
		if (GUI.Button (new Rect (XPOS, YPOS + 145, WIDTH, HEIGHT), creditsButton, "")) {
			Application.LoadLevel(4);
		}

	}
}
