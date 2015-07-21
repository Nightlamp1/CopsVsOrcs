using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

public class HighScores : MonoBehaviour {
  // Remember this:
  //  Anytime you add an element to a structure, ALWAYS add it to the end.

  public static bool        initialized = false;
  public static HighScores  singleton;

  private SceneChangedEventHandler sceneChangeEventHandler;
  private AfterDeletedKeyEventHandler deletedKeyEventHandler;

  void Awake() {
    if (initialized) {
      Destroy(gameObject);
      return;
    }

    initialized = true;
    singleton = this;

    DontDestroyOnLoad(gameObject);

    try {
      serializeFromPlayerPrefs();
    } catch (System.Exception ex) {
      Debug.LogException("ERROR", ex);
    }
    
    if (localScores == null) {
      localScores = new HighScoreboard();
    } 

    if (globalScores == null) {
      globalScores = new HighScoreboard();
    }
  }

  void Start() {
    sceneChangeEventHandler = new SceneChangedEventHandler(SceneChange);
    SceneManager.getInstance().SceneChanged += sceneChangeEventHandler;

    deletedKeyEventHandler = new AfterDeletedKeyEventHandler(AfterDeletedKey);
    PlayerPrefs.getInstance().AfterDeletedKey += deletedKeyEventHandler;
  }

  void SceneChange(SceneManager.Scene oldScene, SceneManager.Scene newScene) {
    serializeToPlayerPrefs();
  }

  void AfterDeletedKey(string key) {
    switch (key) {
      case PREFS.LOCAL_SCORES:
        localScores = new HighScoreboard();
        break;
      case PREFS.GLOBAL_SCORES:
        globalScores = new HighScoreboard();
        break;
      default:
        break;
    }
  }

  public static HighScores getInstance() {
    return singleton;
  }

  void OnDestroy() {
    if (this.localScores != null) {
      serializeToPlayerPrefs();
    }

    SceneManager.getInstance().SceneChanged -= sceneChangeEventHandler;
  }

  public void LoadGlobalScoresResponseString(string input) {
    string[] lines  = input.Split(new char[] {'\n'});
    string[] fields;

    string name;
    string score;

    globalScores.highScoreEntries.Clear();

    for (int i = 0; i < lines.Length; ++i) {
      fields = lines[i].Split(new char[] {':',' '});
      name = "";
      score = "";

      for (int j = 0; j < fields.Length; ++j) {
        if (j + 1 == fields.Length) {
          score = fields[j];
        } else {
          name += fields[j];
        }
      }

      if (name != "" && score != "") {
        HighScoreEntry highScoreEntry = new HighScoreEntry();

        highScoreEntry.playerName   = name;
        highScoreEntry.score        = System.Convert.ToInt32(score);
        highScoreEntry.scoreVersion = 0;

        globalScores.addHighScoreEntry(highScoreEntry);
      }
    }
  }

  public HighScoreboard localScores  { set; get; }
  public HighScoreboard globalScores { set; get; }

  // This function saves the local scores to the player preferences.
  private void serializeToPlayerPrefs() {
    XmlSerializer serializer = new XmlSerializer(typeof(HighScoreboard));

    StringWriter localScoreStringWriter = new StringWriter();
    serializer.Serialize(localScoreStringWriter, localScores);

    StringWriter globalScoreStringWriter = new StringWriter();
    serializer.Serialize(globalScoreStringWriter, globalScores);

    PlayerPrefs.SetString(PREFS.LOCAL_SCORES,  localScoreStringWriter.ToString());
    PlayerPrefs.SetString(PREFS.GLOBAL_SCORES, globalScoreStringWriter.ToString());
  }

  // This function loads the local scores from the player preferences.
  private void serializeFromPlayerPrefs() {
    XmlSerializer serializer = new XmlSerializer(typeof(HighScoreboard));

    StringReader localScoreStringReader = new StringReader(PlayerPrefs.GetString(PREFS.LOCAL_SCORES));
    localScores = (HighScoreboard) serializer.Deserialize(localScoreStringReader);

    StringReader globalScoreStringReader = new StringReader(PlayerPrefs.GetString(PREFS.GLOBAL_SCORES));
    globalScores = (HighScoreboard) serializer.Deserialize(globalScoreStringReader);
  }
}
