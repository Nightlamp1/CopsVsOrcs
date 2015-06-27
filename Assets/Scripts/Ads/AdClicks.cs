using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

public class AdClicks {
  public static AdClicks singleton;
  
  private List<DateTime> clicks;

  private AdClicks() {
    string[] split = PlayerPrefs.GetString("adClicks").Split(new char[] {});

    clicks = new List<DateTime>();

    try {
      for (int i = 0; i < split.Length; ++i) {
        getInstance().clicks.Add(Base64.decodeDateTime(split[i]));
      }
    } catch (Exception ex) {
    }
  }

  public static void serialize() {
    string serializedClicks = "";

    for (int i = 0; i < getInstance().clicks.Count; ++i) {
      serializedClicks += Base64.encodeDateTime(getInstance().clicks[i]);

      if (i + 1 != getInstance().clicks.Count) {
        serializedClicks += "\n";
      }
    }

    PlayerPrefs.SetString("adClicks", serializedClicks);
  }
  
  public static void addClick() {
    getInstance().clicks.Add(DateTime.Now);
  }

  public static bool tooManyClicks() {
    for (int i = 0; i < getInstance().clicks.Count; ++i) {
      if (getInstance().clicks[i].AddDays(1) < DateTime.Now) {
        getInstance().clicks.RemoveAt(i);
      }
    }

    return getInstance().clicks.Count >= 3;
  }

  public static AdClicks getInstance() {
    if (singleton == null) {
      singleton = new AdClicks();
    }

    return singleton;
  }
}
