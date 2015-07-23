using UnityEngine;
using System.Collections;

class DebugManager : MonoBehaviour {
  private static  bool          initialized = false;
  private static  DebugManager  singleton;

#if UNITY_EDITOR
  [Tooltip("Set the log level to DEBUG if you want to see the debug scene.  Set it to any other level to only see that severity of log message and up.")]
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
    //  The .ToString() here is a hack so that the C# compiler won't throw
    //  an error because of unreachable code (it's only unreachable
    //  if the version number is DEVELOPMENT which it should never be in
    //  production)
    if (GameVars.VERSION_NUMBER.ToString() != "DEVELOPMENT" && logLevel != Debug.LogLevel.ERROR) {
      // ba84c7e0-cfab-4fee-96e7-0c1c4d34c530 = Unexpected logLevel in production.
#if !UNITY_EDITOR
      Debug.QuietLogError("ba84c7e0-cfab-4fee-96e7-0c1c4d34c530");
      logLevel = Debug.LogLevel.ERROR;
#endif
    }

    Debug.setLogLevel(logLevel);
  }

  void Start() {
    Debug.setLogLevel(logLevel);
  }

  public static DebugManager getInstance() {
    return singleton;
  }
}
