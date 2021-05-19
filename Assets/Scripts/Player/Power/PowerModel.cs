using UnityEngine;

public class PowerModel
{
  public bool IsEnable = false;
  public float MaxTime;
  public float Timer = 0;
  public int TimeLeft;
  public int Level;

  public PowerModel(int level)
  {
    Level = level;
    MaxTime = 10 + (float) level;
    TimeLeft = (int) MaxTime;
  }

  public void Enable()
  {
    Timer = 0;
    TimeLeft = (int) MaxTime;
    IsEnable = true;
  }
  public void Disable()
  {
    Timer = 0;
    TimeLeft = (int) MaxTime;
    IsEnable = false;
  }
}