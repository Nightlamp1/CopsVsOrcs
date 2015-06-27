using System.Collections.Generic;
using System.Xml.Serialization;
using System;

[XmlRoot("HighScoreboard")]
public class HighScoreboard {
  [XmlArray("HighScoreEntries")]
  [XmlArrayItem("HighScoreEntry", typeof(HighScoreEntry))]
  public List<HighScoreEntry> highScoreEntries { get; set; }

  public HighScoreboard() {
    highScoreEntries = new List<HighScoreEntry>();
  }

  public void addHighScoreEntry(HighScoreEntry highScoreEntry) {
    highScoreEntries.Add(highScoreEntry);
    highScoreEntries.Sort(
      delegate(HighScoreEntry lhs, HighScoreEntry rhs) {
        return rhs.score - lhs.score;
      });

    while (highScoreEntries.Count > 10) {
      highScoreEntries.RemoveAt(10);
    }
  }

  public string getNames() {
    string names = "";

    for (int i = 0; i < highScoreEntries.Count; ++i) {
      names += (i + 1) + " " + highScoreEntries[i].playerName + "\n";
    }

    for (int i = 0; i < 10 - highScoreEntries.Count; ++i) {
      names += "\n";
    }

    return names;
  }

  public string getScores() {
    string scores = "";

    for (int i = 0; i < highScoreEntries.Count; ++i) {
      scores += "$" + highScoreEntries[i].score + "\n";
    }

    for (int i = 0; i < 10 - highScoreEntries.Count; ++i) {
      scores += "\n";
    }

    return scores;
  }
}

