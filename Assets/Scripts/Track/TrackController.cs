using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackController : MonoBehaviour
{
  private GameObject TileSet;
  private float TimeToChangeZoneTimer = 0;
  private float Timer = 0;
  private int[] PropsOffset = new[] { -40, 20 };
  private int[] Lanes = new[] { -7, 0, 7 };
  public GameObject[] CorridorTiles;
  public GameObject[] Landmarks;
  public GameObject[] Props;
  public GameObject[] Tiles;
  public GameObject[] PowerUps;
  public float PowerUpFrequency;
  private int LocalTileCounter = 0;
  public float TileDistanceOffset = 73.7f;
  public float TileLoadDelay;
  public float TileSpawnDistanceLimit;
  public float TimeToChangeZone;
  public int CorridorTileAmount;
  public int PropsPopulation;
  public int TilePerRound;
  public static Vector3 LastTilePosition = new Vector3(0, 0, 0);
  public float LandmarkSpawnTime;
  private void Start()
  {
    TileSet = new GameObject("TileSet");
  }
  private void Update()
  {
    if (!Game.Over)
    {
      TimeToChangeZoneTimer += Time.deltaTime;
      if (TimeToChangeZoneTimer >= TimeToChangeZone)
      {
        SpawnCorridorTile();
        TimeToChangeZoneTimer = 0;
        return;
      }
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
        LocalTileCounter++;
        if (LocalTileCounter >= PowerUpFrequency)
        {
          SpawnPowerUp(ChildTile, ChildTile.transform.position.z - (TileDistanceOffset / 2));
          LocalTileCounter = 0;
        }
      }
    }
  }
  private void SpawnCorridorTile()
  {
    for (int i = 0; i < CorridorTiles.Length; i++)
    {
      var ChildTile = Instantiate(CorridorTiles[i], new Vector3(0, 0, LastTilePosition.z + TileDistanceOffset), Quaternion.identity);
      ChildTile.transform.parent = TileSet.transform;
      LastTilePosition = ChildTile.transform.position;
      SpawnProps(ChildTile);
      LocalTileCounter++;
      if (LocalTileCounter >= PowerUpFrequency)
      {
        SpawnPowerUp(ChildTile, ChildTile.transform.position.z - (TileDistanceOffset / 2));
        LocalTileCounter = 0;
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
  private void SpawnPowerUp(GameObject Parent, float PositionZ)
  {
    GameObject power = GetRandomPowerUp();
    int index = Random.Range(0, Lanes.Length);
    int lane = Lanes[index];
    var spawnedPower = Instantiate(power, new Vector3(lane, 5, PositionZ), Quaternion.identity);
    spawnedPower.transform.parent = Parent.transform;
    Debug.Log(spawnedPower.gameObject.tag + "spawned");
  }
  private GameObject GetRandomPowerUp()
  {
    int index = Random.Range(0, PowerUps.Length);
    return PowerUps[index];
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
    Debug.Log("Tile refreshed");
  }
}
