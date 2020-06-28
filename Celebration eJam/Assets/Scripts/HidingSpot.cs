using UnityEngine;

public class HidingSpot : MonoBehaviour {
  
  private void OnTriggerStay2D(Collider2D other) {
    if (other.tag == "Cat") {
      other.GetComponent<StealthyKitty>().isHidden = true;
    }
  }

  private void OnTriggerExit2D(Collider2D other) {
    if (other.tag == "Cat") {
      other.GetComponent<StealthyKitty>().isHidden = false;
    }
  }
}
