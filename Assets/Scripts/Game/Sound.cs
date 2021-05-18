using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
  public AudioSource Source;
  public AudioClip ObstracleHitSound;
  public AudioClip CoinCollectSound;
  public AudioClip MenuMusic;
  public AudioClip InGameMusic;
  private bool Playing = false;
  private void Update() {
    Source.loop = Playing;
  }
  public void PlayObstracleHitSound()
  {
    Source.PlayOneShot(ObstracleHitSound, 1);
  }
  public void PlayCoinCollectSound()
  {
    Source.PlayOneShot(CoinCollectSound, 1);
  }
  public void PlayMenuMusic()
  {
    Source.Stop();
    Playing = true;
    Source.clip = MenuMusic;
    Source.Play();
  }
  public void PlayInGameMusic()
  {
    Source.Stop();
    Playing = true;
    Source.clip = InGameMusic;
    Source.Play();
  }
}
