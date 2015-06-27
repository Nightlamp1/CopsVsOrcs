using System;
using System.Collections.Generic;
using System.Xml.Serialization;

[XmlRoot("HighScoreEntry")]
public class HighScoreEntry {
  [XmlAttribute("playerUuid")]
  public Guid playerUuid { get; set; }

  [XmlAttribute("playerName")]
  public string playerName { get; set; }

  [XmlAttribute("scoreUuid")]
  public Guid scoreUuid { get; set; }

  [XmlAttribute("score")]
  public int score { get; set; }

  [XmlAttribute("scoreVersion")]
  public int scoreVersion { get; set; }

  [XmlAttribute("appVersion")]
  public string appVersion { get; set; }
}
