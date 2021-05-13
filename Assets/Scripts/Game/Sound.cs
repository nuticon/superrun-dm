using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
  public static AudioSource Source;
  private void Start()
  {
    Source = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioSource>();
  }
}
