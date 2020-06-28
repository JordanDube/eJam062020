
public class CatTreats : Food {

  public float jumpIncrease = 2f;
  
  public override void ApplyUpgrade(Player player) {
    player.jumpHeight += jumpIncrease;
    Destroy(gameObject);
  }
  
}
