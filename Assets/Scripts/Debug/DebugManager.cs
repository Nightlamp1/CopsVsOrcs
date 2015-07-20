using UnityEngine;
using System.Collections;

class DebugManager : MonoBehaviour {
  private static  bool          initialized = false;
  private static  DebugManager  singleton;

#if UNITY_EDITOR
  public Debug.LogLevel logLevel = Debug.LogLevel.DEBUG;
#else
  public Debug.LogLevel logLevel = Debug.LogLevel.ERROR;
#endif

  void Awake() {
    if (initialized) {
      Destroy(gameObject);
      return;
    }

    initialized = true;
    singleton = this;

    DontDestroyOnLoad(gameObject);

    // Try to ensure that we don't log anything above ERROR on releases.
    if (GameVars.VERSION_NUMBER != "vDevelopment") {
      logLevel = Debug.LogLevel.ERROR;
    }

    Debug.setLogLevel(logLevel);
  }

  public static DebugManager getInstance() {
    return singleton;
  }
}
