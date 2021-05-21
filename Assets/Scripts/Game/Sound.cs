using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
  public AudioClip CoinCollectSound;
  public AudioClip InGameMusic;
  public AudioClip MenuMusic;
  public AudioClip ObstracleHitSound;
  public AudioClip PowerUpCollectSound;
  public AudioSource Source;
  private bool Playing = false;
  private void Update()
  {
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
  public void PlayPowerUpCollectSound()
  {
    Source.PlayOneShot(PowerUpCollectSound, 1);
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
