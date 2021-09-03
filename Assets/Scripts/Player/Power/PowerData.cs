using UnityEngine;
[System.Serializable]
public class PowerData
{
  private int DoubleLevel = 0;
  private int MagnetLevel = 0;
  private int LifeLevel = 0;
  public int GetDoubleLevel()
  {
    return DoubleLevel;
  }
  public int GetMagnetLevel()
  {
    return MagnetLevel;
  }
  public int GetLifeLevel()
  {
    return LifeLevel;
  }
  public void SetMagnetLevel(int level)
  {
    MagnetLevel = level;
  }
  public void SetDoubleLevel(int level)
  {
    DoubleLevel = level;
  }
  public void SetLifeLevel(int level)
  {
    LifeLevel = level;
  }
  public void Save()
  {
    Storage.SavePowerUpData(this);
  }
  public void Load()
  {
    PowerData data = Storage.LoadPowerData();
    if (data != null)
    {
      this.DoubleLevel = data.DoubleLevel;
      this.MagnetLevel = data.MagnetLevel;
      this.LifeLevel = data.LifeLevel;
    }
  }
}
