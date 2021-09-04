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
  private void Start()
  {
    Setting setting = new Setting();
    setting.Load();
    if (!setting.Music) Source.volume = 0;
  }
  private void Update()
  {
    Source.loop = Playing;
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
