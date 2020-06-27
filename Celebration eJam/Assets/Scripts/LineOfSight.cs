using System;
using System.Collections;
using UnityEngine;

public class LineOfSight : MonoBehaviour {

  private Animator _animator;
  private PolygonCollider2D _polygonCollider2D;
  private SpriteRenderer _spriteRenderer;

  public bool metalCatSolid = false;
  public float delay = 2f;
  
  private void Awake() {
    _polygonCollider2D = GetComponent<PolygonCollider2D>();
    _spriteRenderer = GetComponent<SpriteRenderer>();
    _animator = GetComponent<Animator>();

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

  public void IsLooking() {
    _animator.SetBool("isLooking", true);
  }

  public void StopLooking() {
    _animator.SetBool("isLooking", false);
  }

}
