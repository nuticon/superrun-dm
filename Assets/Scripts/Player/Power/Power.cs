using UnityEngine;

public class Power : MonoBehaviour
{
  private bool IsDouble = false;
  private float DoubleTimer = 0;
  private int DoubleLevel;
  private bool IsMagnet = false;
  private float MagnetTimer = 0;
  private int MagnetLevel;
  private PowerData powerData;
  private void Start() {
    powerData = new PowerData();
  }
  public void SetActivePower(string PowerName)
  {
    switch (PowerName)
    {
        case "Double" :
          ActivateDouble();
          break;
        case "Magnet" :
          ActivateMagnet();
          break;
    }
  }
  private void ActivateDouble()
  {
    DoubleTimer = 0;
    IsDouble = true;
  }
   private void ActivateMagnet()
  {
    MagnetTimer = 0;
    IsMagnet = true;
  }
  public bool DoubleActivating()
  {
    if (!IsDouble) return false;
    DoubleTimer += Time.deltaTime;
    if(DoubleTimer >= 5 * powerData.GetDoubleLevel())
    {
      IsDouble = false;
      DoubleTimer = 0;
    }
    return true;
  }
}
