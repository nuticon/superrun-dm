using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackController : MonoBehaviour
{
  public GameObject[] Tile;
  public float TileLoadDelay = 1f;
  private Vector3 LastTilePosition = new Vector3(0,0,0);
  private float Timer;
  private void Update()
  {
    if (Game.GameStarted && !Game.Over)
    {
      Timer += Time.deltaTime;
      if (Timer >= TileLoadDelay)
      {
        Instantiate(Tile[0], LastTilePosition += Vector3.forward * 10, Quaternion.identity);
        Timer = 0;
      }
    }
  }
}
