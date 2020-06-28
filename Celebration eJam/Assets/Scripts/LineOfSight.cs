using System;
using System.Collections;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class LineOfSight : MonoBehaviour {

  private Animator _animator;
  private PolygonCollider2D _polygonCollider2D;
  private SpriteRenderer _spriteRenderer;

  private Person _person;
  private bool _facingRight = true;
  
  public bool metalCatSolid = false;
  public float delay = 2f;
  
  private void Awake() {
    _polygonCollider2D = GetComponent<PolygonCollider2D>();
    _spriteRenderer = GetComponent<SpriteRenderer>();
    _animator = GetComponent<Animator>();

    _person = GetComponentInParent<Person>();
    
    if (metalCatSolid)
      StartCoroutine(TactiCATEspionageAction());
  }

  private IEnumerator TactiCATEspionageAction() {
    _polygonCollider2D.enabled = !_polygonCollider2D.enabled; 
    _spriteRenderer.enabled = !_spriteRenderer.enabled;
    yield return new WaitForSeconds(delay);

    StartCoroutine(TactiCATEspionageAction());
  }

  private void OnTriggerStay2D(Collider2D other) {
    if ("Cat".Equals(other.tag)) {
      StealthyKitty stealthyKitty = other.GetComponent<StealthyKitty>();
      
      if(!stealthyKitty.IsHidden())
        _animator.SetTrigger("Seen");
    }
  }

  private void Update() {
    // int direction = _person.FacingRight ? 1 : -1;
    // int direction = -1;
    // Debug.Log(Mathf.Abs(-70));
    // transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, -Mathf.Abs(transform.eulerAngles.z));
    // _person.FacingRight ? 1 : -1
    if (!_person.FacingRight.Equals(_facingRight)) {
      transform.rotation = Quaternion.Inverse(transform.rotation);
      _facingRight = _person.FacingRight;
    }
  }

  public void EnableLineOfSight() {
    _spriteRenderer.enabled = true;
    _polygonCollider2D.enabled = true;
  }
  
  public void IsLooking() {
    _animator.SetBool("isLooking", true);
  }

  public void StopLooking() {
    _animator.SetBool("isLooking", false);
  }

}
