using UnityEngine;
using System.Collections;

public class HankGUI : MonoBehaviour 
{
  int HALF_WIDTH = 200;
  int HEIGHT = 50;

  float m_playerHP;
  float m_lastPlayerHP;
  float m_enemyHP;
  float m_lastEnemyHP;

  private static HankGUI singleton;

  GUIStyle guiStyle;

  Rect m_guiBox;
  Rect m_playerHealthBackground;
  Rect m_enemyHealthBackground;
  Rect m_playerHealthForeground;
  Rect m_enemyHealthForeground;
  
  public Texture playerHealthBackgroundTexture;
  public Texture playerHealthForegroundTexture;
  public Texture playerHealthFrameTexture;
  
  public Texture enemyHealthBackgroundTexture;
  public Texture enemyHealthForegroundTexture;
  public Texture enemyHealthFrameTexture;

  bool m_init;

  void init()
  {
    m_init = true;

    m_playerHP = 1.0f;
    m_enemyHP  = -1f;

    int l_guiLeft   = ((Screen.width / 2) - HALF_WIDTH);
    int l_guiTop    = (Screen.height - HEIGHT);
    int l_guiWidth  = (HALF_WIDTH * 2);
    int l_guiHeight = (HEIGHT);

    guiStyle = new GUIStyle();

    guiStyle.alignment = TextAnchor.MiddleLeft;

    //m_guiBox = new Rect(l_guiLeft, l_guiTop, l_guiWidth, l_guiHeight);
    m_enemyHealthBackground  = new Rect(l_guiLeft + 5, l_guiTop +  5, l_guiWidth - 10, (l_guiHeight - 10) / 2);
    m_playerHealthBackground = new Rect(l_guiLeft + 5, l_guiTop + 20, l_guiWidth - 10, (l_guiHeight - 10) / 2);

    m_lastPlayerHP = -1;
    m_lastEnemyHP  = -1;

    singleton = this;
  }

  void OnGUI () 
  {
    if (m_init == false)
    {
      init();
    }
    
    // Reduce player health recalculations
    if (m_lastPlayerHP != m_playerHP)
    {
      m_playerHealthForeground = new Rect(m_playerHealthBackground.xMin,  m_playerHealthBackground.yMin,
                                          m_playerHealthBackground.width, m_playerHealthBackground.height);

      m_playerHealthForeground.width = (m_playerHP * m_playerHealthForeground.width);

      m_lastPlayerHP = m_playerHP;
    }

    // Draw player health    
    GUI.DrawTexture(m_playerHealthBackground, playerHealthBackgroundTexture, ScaleMode.StretchToFill, true, 0 );
    GUI.DrawTexture(m_playerHealthForeground, playerHealthForegroundTexture, ScaleMode.StretchToFill, true, 0 );
    GUI.DrawTexture(m_playerHealthBackground, playerHealthFrameTexture, ScaleMode.StretchToFill, true, 0 );

    if (m_enemyHP >= 0)
    {
      // Enemy health background
      GUI.Box(m_enemyHealthBackground, "");

      // Reduce enemy health recalculations
      if (m_lastEnemyHP != m_enemyHP)
      {
        m_enemyHealthForeground = new Rect(m_enemyHealthBackground);
        
        m_enemyHealthForeground.width = (m_enemyHP * m_enemyHealthForeground.width);
        
        m_lastEnemyHP = m_enemyHP;
      }
      
      // Draw enemy health
      GUI.Box(m_enemyHealthForeground, "");
    }
  }

  public void decPlayerHP(float decrement)
  {
    setPlayerHP(m_playerHP - decrement);
  }
  
  public void decEnemyHP(float decrement)
  {
    setPlayerHP(m_enemyHP - decrement);
  }

  public void setPlayerHP(float p_hp)
  {
    Debug.Log ("Setting player HP from " + m_playerHP + " to " + p_hp);

    m_playerHP = p_hp;

    if (p_hp <= 0)
    {
      // TODO: Callback Death
      m_playerHP = 0;
    }
    else if (p_hp > 1.0f)
    {
      // Cannot set above 1.0f
      m_playerHP = 1.0f;
    }
  }

  public void setEnemyHP(float p_hp)
  {
    Debug.Log ("Setting enemy HP from " + m_enemyHP + " to " + p_hp);

    m_enemyHP = p_hp;

    if (p_hp <= 0)
    {
      // TODO: Callback Death
      m_enemyHP = p_hp;
    }
    else if (p_hp > 1.0f)
    {
      // Cannot set above 1.0f
      m_enemyHP = 1.0f;
    }
  }

  public static HankGUI getInstance()
  {
    if (singleton == null)
    {
      singleton = new HankGUI();

      singleton.init();
    }

    return singleton;
  }
}