using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackController : MonoBehaviour
{
  public GameObject[] Tile;
  public float TileLoadDelay = 1f;
  public static Vector3 LastTilePosition = new Vector3(0, 0, 0);
  private float Timer;
  private void Update()
  {
    if (Game.GameStarted && !Game.Over)
    {
      Timer += Time.deltaTime;
      if (Timer >= TileLoadDelay)
      {
        GameObject SelectedTile = GetRandomTile();
        Instantiate(SelectedTile, new Vector3(0, 0, LastTilePosition.z + 42.93203f), Quaternion.identity);
        Timer = 0;
      }
    }
  }
  private GameObject GetRandomTile()
  {
    int index = Random.Range(0, Tile.Length);
    return Tile[index];
  }
}
