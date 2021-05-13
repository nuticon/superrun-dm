using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
  public AudioClip CoinSound;
  private AudioSource Source;
  private void Start() {
    Source = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>();
  }
  private void OnTriggerEnter(Collider other)
  {
    if (other.gameObject.tag == "Player")
    {
      Source.PlayOneShot(CoinSound,1f);
      Game.Coin++;
      Object.Destroy(this.gameObject);
    }
  }
}
