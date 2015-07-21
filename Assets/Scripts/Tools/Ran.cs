using UnityEngine;

/* 
 * This class will just provide some randomness shortcuts.
 */
public class Ran {
  // This function grabs a random int between 0 and maxRan,
  //  it then generates a random number (ran)
  //  it then adds each rarity array item to sum until sum exceeds ran
  //  it then returns the index where ran was exceeded
  //  if this never happens, it returns -1
  public static int rarityIndex(float[] rarity, int maxRan) {
    float sum = 0f;
    int ran = Random.Range(0, maxRan);

    for (int i = 0; i < rarity.Length; ++i) {
      sum += rarity[i];
      if ((float) ran < sum) {
        return i;
      }
    }

    return -1;
  }
}
