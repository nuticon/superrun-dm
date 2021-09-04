using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX : MonoBehaviour
{
  public AudioClip CoinCollectSound;
  public AudioClip ObstracleHitSound;
  public AudioClip PowerUpCollectSound;
  public AudioSource Source;
  public static SFX Instance;
  private void Awake()
  {
    Instance = this;
  }
  private void Start()
  {
    Setting setting = new Setting();
    setting.Load();
    if (!setting.SFX) Source.volume = 0;
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
}
