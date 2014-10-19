using UnityEngine;
using System.Collections;

public class EndlessRunnerCollisions : MonoBehaviour {
  HudScript hud_script;

  void OnTriggerEnter2D(Collider2D other)
  {
    if (other.name == "Powerup(Clone)")
    {
      Destroy (other);
      hud_script.IncreaseScore(100);
    }
    if (other.name == "Enemy")
    {
      if (this.name == "Player")
      {
        hud_script.IncreaseScore(-100);
      }
      else if(this.name == "Bullet(clone)")
      {
        hud_script.IncreaseScore(100);
      }
      else
      {
        return;
      }
      Destroy (other);
    }
  }
}
