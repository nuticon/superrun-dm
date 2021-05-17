using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackController : MonoBehaviour
{
  [Header("Track Setting")]
  private GameObject TileSet;
  private float Timer = 0;
  private int[] PropsOffset = new[] { -40, 20 };
  public GameObject[] Landmarks;
  public GameObject[] Props;
  public GameObject[] Tiles;
  public float TileDistanceOffset = 73.7f;
  public float TileLoadDelay = 0.3f;
  public float TileSpawnDistanceLimit = 1000f;
  public int PropsPopulation;
  public int TilePerRound = 6;
  public static Vector3 LastTilePosition = new Vector3(0, 0, 0);
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
        SpawnTiles();
        Timer = 0;
      }
    }
  }
  private void SpawnTiles()
  {
    GameObject SelectedTile;
    for (int i = 0; i < TilePerRound; i++)
    {
      if ((LastTilePosition.z - Character.Position.z) < TileSpawnDistanceLimit)
      {
        SelectedTile = GetRandomTile();
        var ChildTile = Instantiate(SelectedTile, new Vector3(0, 0, LastTilePosition.z + TileDistanceOffset), Quaternion.identity);
        ChildTile.transform.parent = TileSet.transform;
        LastTilePosition = ChildTile.transform.position;
        SpawnProps(ChildTile);
      }
    }
  }
  private void SpawnProps(GameObject Parent)
  {
    GameObject SelectedProps;
    for (int i = 0; i < PropsPopulation; i++)
    {
      SelectedProps = GetRandomProp();
      int RandomPropOffset = GetRandomPropOffset() + Random.Range(0, 20);
      var ChildProp = Instantiate(SelectedProps, new Vector3(RandomPropOffset, 0, LastTilePosition.z + TileDistanceOffset + RandomPropOffset), Quaternion.identity);
      ChildProp.transform.parent = Parent.transform;
    }
  }
  private GameObject GetRandomTile()
  {
    int index = Random.Range(0, Tiles.Length);
    return Tiles[index];
  }
  private GameObject GetRandomProp()
  {
    int index = Random.Range(0, Props.Length);
    return Props[index];
  }
  private int GetRandomPropOffset()
  {
    int index = Random.Range(0, PropsOffset.Length);
    return PropsOffset[index];
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
