using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
  private float DeclayDelay = 5;
  void Update()
  {
    if (transform.position.z + (DeclayDelay * 10) < Character.Position.z)
      Object.Destroy(this.gameObject);
  }
}
