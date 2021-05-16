using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackController : MonoBehaviour
{
  [Header("Track Setting")]
  public GameObject[] Tile;
  public float TileLoadDelay = 0.3f;
  public float TileDistanceOffset = 73.7f;
  public int TilePerRound = 6;
  public float TileSpawnDistanceLimit = 1000f;
  public static Vector3 LastTilePosition = new Vector3(0, 0, 0);
  private float Timer = 0;
  private GameObject TileSet;
  private void Start()
  {
    TileSet = new GameObject("TileSet");
  }
  private void Update()
  {
    if (!Game.Over)
    {
      Timer += Time.deltaTime;
      if (Timer >= TileLoadDelay)
      {
        GameObject SelectedTile;
        GameObject TileSet = GameObject.Find("TileSet");
        for (int i = 0; i < TilePerRound; i++)
        {
          if ((LastTilePosition.z - Character.Position.z) < TileSpawnDistanceLimit)
          {
            SelectedTile = GetRandomTile();
            var ChildTile = Instantiate(SelectedTile, new Vector3(0, 0, LastTilePosition.z + TileDistanceOffset), Quaternion.identity);
            ChildTile.transform.parent = TileSet.transform;
            LastTilePosition = ChildTile.transform.position;
          }
        }
        Timer = 0;
      }
    }
  }
  private GameObject GetRandomTile()
  {
    int index = Random.Range(0, Tile.Length);
    return Tile[index];
  }
  public void RefreshTile()
  {
    foreach (Transform Tile in TileSet.transform)
    {
      Object.Destroy(Tile.gameObject);
    }
    TrackController.LastTilePosition = new Vector3(0, 0, 0); foreach (Transform Tile in TileSet.transform)
    {
      Object.Destroy(Tile.gameObject);
    }
    TrackController.LastTilePosition = new Vector3(0, 0, 0);
  }
}
