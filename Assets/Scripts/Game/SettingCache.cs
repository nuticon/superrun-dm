using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingCache : MonoBehaviour
{
  public static SettingCache Instance;
  public Setting setting;
  private void Awake()
  {
    Instance = this;
  }
  private void Start()
  {
    setting = new Setting();
    setting.Load();
  }
}
