using UnityEngine;

public class StealthyKitty : MonoBehaviour {

  private Player _player;

  private void Awake() {
    _player = GetComponent<Player>();
  }

  public bool IsHidden() {
    return Vector2.zero.Equals(_player.rb.velocity) && Input.GetAxis("Horizontal").Equals(0f);
  }
  
}
