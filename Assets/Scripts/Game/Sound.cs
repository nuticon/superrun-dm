using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
  public AudioSource Source;
  public AudioClip ObstracleHitSound;
  public AudioClip CoinCollectSound;

  public void PlayObstracleHitSound()
  {
    Source.PlayOneShot(ObstracleHitSound, 1);
  }
  public void PlayCoinCollectSound()
  {
    Source.PlayOneShot(CoinCollectSound, 1);
  }
}
