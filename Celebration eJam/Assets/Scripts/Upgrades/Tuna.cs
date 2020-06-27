using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Tuna : Food {
  
  public override void ApplyUpgrade(Player player) {
    Debug.Log("YOU DID IT!!!");
    FindObjectsOfType<LineOfSight>().ToList().ForEach(lineOfSight => { lineOfSight.EnableLineOfSight(); });
    Destroy(gameObject);
  }
  
}
