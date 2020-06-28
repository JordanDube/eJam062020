using System;
using System.Collections;
using TMPro;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class LineOfSight : MonoBehaviour {

  private Animator _animator;
  private PolygonCollider2D _polygonCollider2D;
  private SpriteRenderer _spriteRenderer;

  private Person _person;
  private bool _facingRight = true;

  [SerializeField] TMPro.TMP_Text travelText; 
  public bool metalCatSolid = false;
  public float delay = 2f;
    //AudioSource audioSource;
  private void Awake() {
    _polygonCollider2D = GetComponent<PolygonCollider2D>();
    _spriteRenderer = GetComponent<SpriteRenderer>();
    _animator = GetComponent<Animator>();

    _person = GetComponentInParent<Person>();

        //audioSource = gameObject.GetComponent<AudioSource>();

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
      Player player = FindObjectOfType<Player>();
      
      if (!stealthyKitty.IsHidden() && !player.badKitty) {
        StartCoroutine(BadKitty(player));
      }
    }
  }

  private IEnumerator BadKitty(Player player) {
    travelText.text = "\"Bad kitty! Put that down!\"";
    player.badKitty = true;
    _person.AlertedToTransform(player.transform);
    yield return new WaitForSeconds(2f);
    travelText.text = "";
    player.badKitty = false;
    player.ResetToSpawn();
  }
  
  private void Update() {
    if (!_person.FacingRight.Equals(_facingRight)) {
      transform.rotation = Quaternion.Inverse(transform.rotation);
      transform.localPosition = new Vector3(transform.localPosition.x * -1, transform.localPosition.y, transform.localPosition.z);
      _facingRight = _person.FacingRight;
    }
  }

  public void EnableLineOfSight() {
    _spriteRenderer.enabled = true;
    _polygonCollider2D.enabled = true;
  }

  public void DisableLineOfSight() {
    _spriteRenderer.enabled = false;
    _polygonCollider2D.enabled = false;
  }
  
  public void IsLooking() {
    _animator.SetBool("isLooking", true);
        /*if(!audioSource.isPlaying)
        {
            audioSource.Play();
        }*/

  }

  public void StopLooking() {
    _animator.SetBool("isLooking", false);
  }

}
