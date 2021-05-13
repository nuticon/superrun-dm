using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
  public AudioClip CoinSound;
  private void OnTriggerEnter(Collider other)
  {
    if (other.gameObject.tag == "Player")
    {
      Sound.Source.PlayOneShot(CoinSound, 1f);
      Game.Coin++;
      Object.Destroy(this.gameObject);
    }
  }
}
