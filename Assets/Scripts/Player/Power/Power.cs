using UnityEngine;

public class Power : MonoBehaviour
{
  public PowerModel Magnet;
  public PowerModel Double;
  private PowerData powerData;
  private string TempPower = "";
  public static string GlobalPower = "";
  public static Power Instance;
  private void Awake()
  {
    Instance = this;
  }
  private void Start()
  {
    powerData = new PowerData();
    powerData.Load();
    Magnet = new PowerModel(powerData.GetMagnetLevel());
    Double = new PowerModel(powerData.GetDoubleLevel());
  }
  private void Update()
  {
    WatchGlobalPower();
  }
  public void SetActivePower(string PowerName)
  {
    switch (PowerName)
    {
      case "Double":
        ActivateDouble();
        break;
      case "Multiply":
        ActivateDouble();
        break;
      case "Magnet":
        ActivateMagnet();
        break;
    }
  }
  private void ActivateDouble()
  {
    Double.Enable();
    Debug.Log("Double point activated");
  }
  private void ActivateMagnet()
  {
    Magnet.Enable();
    Debug.Log("Magnet point activated");
  }
  public bool DoubleActivating()
  {
    if (!Double.IsEnable) return false;
    Double.Timer += Time.deltaTime;
    Double.TimeLeft = (int)(Double.MaxTime - Double.Timer);
    if (Double.Timer >= Double.MaxTime)
    {
      Double.Disable();
    }
    return true;
  }
  public bool MagnetActivating()
  {
    if (!Magnet.IsEnable) return false;
    Magnet.Timer += Time.deltaTime;
    Magnet.TimeLeft = (int)(Magnet.MaxTime - Magnet.Timer);
    if (Magnet.Timer >= Magnet.MaxTime)
    {
      Magnet.Disable();
    }
    return true;
  }
  private void WatchGlobalPower()
  {
    if (GlobalPower != TempPower)
    {
      SetActivePower(GlobalPower);
      SFX.Instance.PlayPowerUpCollectSound();
      GlobalPower = TempPower;
    }
  }
}
