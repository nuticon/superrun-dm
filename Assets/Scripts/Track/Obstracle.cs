using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstracle : MonoBehaviour
{
  private void OnTriggerEnter(Collider other)
  {
    if (other.gameObject.tag == "Player")
    {
      Game.Over = true;
    }
    if (other.gameObject.tag == "Coin")
    {
        if(this.gameObject.tag == "JumpObstracle") other.transform.position += Vector3.up * 2;
        if(this.gameObject.tag == "NoPassObstracle") Object.Destroy(other.gameObject);
    }
  }
}
