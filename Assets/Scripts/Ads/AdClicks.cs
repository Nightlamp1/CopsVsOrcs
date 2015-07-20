using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

[XmlRoot("AdClicks")]
public class AdClicks {
  public static AdClicks singleton;
  
  [XmlArray("clicks")]
  [XmlArrayItem("DateTime", typeof(DateTime))]
  public List<DateTime> clicks;

  private AdClicks() {
  }

  public static void serialize() {
    getInstance().serializeToPlayerPrefs();
  }
  
  public static void addClick() {
    getInstance().clicks.Add(DateTime.Now);

    serialize();
  }

  public static bool tooManyClicks() {
    for (int i = 0; i < getInstance().clicks.Count; ++i) {
      if (getInstance().clicks[i].AddDays(1) < DateTime.Now) {
        getInstance().clicks.RemoveAt(i);
      }
    }

    serialize();

    return getInstance().clicks.Count >= 3;
  }

  public static bool wayTooManyClicks() {
    if (tooManyClicks()) {
      return getInstance().clicks.Count >= 5;
    }

    return false;
  }

  public static AdClicks getInstance() {
    if (singleton == null) {
      try {
        serializeFromPlayerPrefs();
      } catch (Exception ex) {
        Debug.LogException("DEBUG", ex);

        singleton = new AdClicks();
      }
    }

    if (singleton.clicks == null) {
      singleton.clicks = new List<DateTime>();
    }

    return singleton;
  }

  private void serializeToPlayerPrefs() {
    string clicksText = "";

    for (int i = 0; i < clicks.Count; ++i) {
      clicksText += clicks[i].ToString() + "\n";
    }

    PlayerPrefs.SetString("adClicks", clicksText);
  }

  // This function loads the local scores from the player preferences.
  private static void serializeFromPlayerPrefs() {
    if (singleton == null) {
      singleton = new AdClicks();
    }

    string   clicksText = "";
    string[] clicksSplit;

    try {
      clicksText = PlayerPrefs.GetString("adClicks");
      clicksSplit = clicksText.Split(new char[] {'\n'});

      if (clicksText == "") {
        return;
      }

      singleton.clicks = new List<DateTime>();

      for (int i = 0; i < clicksSplit.Length; ++i) {
        singleton.clicks.Add(DateTime.Parse(clicksSplit[i]));
      }
    } catch (Exception ex) {
      Debug.LogException(ex);

      Debug.LogDebug("Unable to parse clicksText: " + clicksText);
    }
  }
}
