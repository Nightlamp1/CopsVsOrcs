using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour 
{
  void OnCollisionEnter2D(Collision2D other)
  {
    if(gameObject.name == "Cop")
    {
      if(other.transform.tag == "Enemy")
      { 
        HankGUI.getInstance().decPlayerHP(0.10f);
      }

      if(other.gameObject.name == "bullet(Clone)")
      {
        HankGUI.getInstance().decPlayerHP(0.20f);
      }
    }
    else if (gameObject.transform.tag == "Boss")
    {
      if (other.gameObject.name == "bullet(Clone)")
      {
        HankGUI.getInstance().decEnemyHP(0.10f);
      }
    }
  }
}
