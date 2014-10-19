using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour 
{
  public const float DEF_BUMP_ENEMY = 0.101f;
  public const float DEF_BULLET_HITS_ENEMY = 0.201f;
  public const float DEF_BULLET_HITS_PLAYER = 0.201f;
  float m_health;
  float m_bumpEnemy;
  float m_bulletHitsEnemy;
  float m_bulletHitsPlayer;

  public Health()
  {
    m_health = 1.0f;
    m_bumpEnemy = DEF_BUMP_ENEMY;
    m_bulletHitsEnemy = DEF_BULLET_HITS_ENEMY;
    m_bulletHitsPlayer = DEF_BULLET_HITS_PLAYER;
  }

  public void decHealth(float amount)
  {
    m_health -= amount;

    if (m_health <= 0f)
    {
      Destroy (gameObject);
    }
    else if (m_health > 1.0f)
    {
      m_health = 1.0f;
    }
  }

  void OnCollisionEnter2D(Collision2D other)
  {
    if(gameObject.name == "Cop")
    {
      if(other.transform.tag == "Enemy")
      { 
        decHealth(m_bumpEnemy);
        HankGUI.getInstance().decPlayerHP(m_bumpEnemy);
      }

      if(other.gameObject.name == "bullet(Clone)")
      {
        decHealth (m_bulletHitsPlayer);
        HankGUI.getInstance().decPlayerHP(m_bulletHitsPlayer);
      }
    }
    else if (gameObject.transform.tag == "Enemy")
    {
      if (other.gameObject.name == "bullet(Clone)")
       {
        decHealth (m_bulletHitsEnemy);
        HankGUI.getInstance().setEnemyHP(m_health);
      }
    }
    else if (gameObject.transform.tag == "Boss")
    {
      if (other.gameObject.name == "bullet(Clone)")
      {
        HankGUI.getInstance().decEnemyHP(0.10f);
      }
    }

    if (other.gameObject.name == "bullet(Clone)")
    {
      Destroy (other.gameObject);
    }
  }
}
