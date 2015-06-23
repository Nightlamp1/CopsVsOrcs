using UnityEngine;
using System.Collections;

public class Prefabs {
  public const float DEF_X = -1000;
  public const float DEF_Y = -1000;
  private static Prefabs singleton;
  private Hashtable m_prefabs;

  private Prefabs()
  {
    m_prefabs = new Hashtable();
  }

  public Object getPrefab(string name)
  {
    if (m_prefabs[name] == null)
    {
      m_prefabs.Add (name, prefab(name));
    }

    return (Object) m_prefabs[name];
  }

  public Object prefab(string name)
  {
    System.Type prefabType = System.Type.GetType(name);

    Object obj = (Object) prefabType.GetMethod("getPrefab").Invoke(null, null);

    obj.name = name;

    return obj;
  }

  public static Prefabs getInstance()
  {
    if (singleton == null)
    {
      singleton = new Prefabs();
    }

    return singleton;
  }
}
