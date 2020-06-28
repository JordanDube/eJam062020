using UnityEngine;

public class BonusTime : Food {
    public float timeToAdd = 30;
  
    public override void ApplyUpgrade(Player player) {
        FindObjectOfType<GameClock>().currenttime += timeToAdd;
        FindObjectOfType<GameClock>().addedTime += timeToAdd;
        Destroy(gameObject);
    }

}
