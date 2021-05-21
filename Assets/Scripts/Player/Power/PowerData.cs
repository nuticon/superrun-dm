using UnityEngine;
[System.Serializable]
public class PowerData
{
  private int DoubleLevel = 1;
  private int MagnetLevel = 1;

  public int GetDoubleLevel()
  {
    return DoubleLevel;
  }
  public int GetMagnetLevel()
  {
    return MagnetLevel;
  }
  public void Save()
  {
    Storage.SavePowerUpData(this);
  }
  public void Load()
  {
    PowerData data = Storage.LoadPowerData();
    this.DoubleLevel = data.DoubleLevel;
    this.MagnetLevel = data.MagnetLevel;
  }
}
