using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
  public AudioClip InGameMusic;
  public AudioClip MenuMusic;
  public AudioSource Source;
  private bool Playing = false;
  public static Music Instance;
  private void Awake()
  {
    Instance = this;
  }
  private void Update()
  {
    Source.loop = Playing;
    if (!SettingCache.Instance.setting.Music) Source.mute = true;
    else Source.mute = false;
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
