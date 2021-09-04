using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Setting
{
  public bool Music = true;
  public bool SFX = true;
  public Setting(bool music, bool sfx)
  {
    Music = music;
    SFX = sfx;
  }
  public Setting()
  {

  }
  public void Save()
  {
    Storage.SaveSettting(this);
  }
  public void Load()
  {
    Setting setting = Storage.LoadSetting();
    if (setting != null)
    {
      this.Music = setting.Music;
      this.SFX = setting.SFX;
    }
  }
}
