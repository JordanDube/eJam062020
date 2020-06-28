using UnityEngine;

public class StealthyKitty : MonoBehaviour {

  private Player _player;
  public bool isHidden = false;

  private void Awake() {
    _player = GetComponent<Player>();
  }

  public bool IsHidden() {
    return isHidden;
  }
  
}
