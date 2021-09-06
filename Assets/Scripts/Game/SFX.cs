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
  private void Update()
  {
    if (!SettingCache.Instance.setting.SFX) Source.mute = true;
    else Source.mute = false;
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
