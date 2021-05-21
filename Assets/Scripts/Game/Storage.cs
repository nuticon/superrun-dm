using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
public static class Storage
{
  public static void SavePlayer(Player player)
  {
    BinaryFormatter Formatter = new BinaryFormatter();
    string Path = Application.persistentDataPath + "/player.dat";
    FileStream stream = new FileStream(Path, FileMode.Create);
    Formatter.Serialize(stream, player);
    stream.Close();
  }

  public static Player LoadPlayer()
  {
    string Path = Application.persistentDataPath + "/player.dat";
    if (File.Exists(Path))
    {
      BinaryFormatter formatter = new BinaryFormatter();
      FileStream stream = new FileStream(Path, FileMode.Open);
      Player data = formatter.Deserialize(stream) as Player;
      stream.Close();
      return data;
    }
    return null;
  }

  public static void SavePowerUpData(PowerData power)
  {
    BinaryFormatter Formatter = new BinaryFormatter();
    string Path = Application.persistentDataPath + "/power.dat";
    FileStream stream = new FileStream(Path, FileMode.Create);
    Formatter.Serialize(stream, power);
    stream.Close();
  }

  public static PowerData LoadPowerData()
  {
    string Path = Application.persistentDataPath + "/power.dat";
    if (File.Exists(Path))
    {
      BinaryFormatter formatter = new BinaryFormatter();
      FileStream stream = new FileStream(Path, FileMode.Open);
      PowerData data = formatter.Deserialize(stream) as PowerData;
      stream.Close();
      return data;
    }
    return null;
  }
}
