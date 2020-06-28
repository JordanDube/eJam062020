
public class Catnip : Food {

  public float speedIncrease = 2f;
  
  public override void ApplyUpgrade(Player player) {
    player.movementSpeed += speedIncrease;
    Destroy(gameObject);
  }
  
}
