using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
  public float CoinRotationSpeed;
  Animator animator;
  private void Start() {
    animator = GetComponent<Animator>();
    animator.speed = CoinRotationSpeed;
  }
  private void OnTriggerEnter(Collider other)
  {
    if (other.gameObject.tag == "Player")
    {
      Game.Coin++;
      Object.Destroy(this.gameObject);
    }
  }
}
