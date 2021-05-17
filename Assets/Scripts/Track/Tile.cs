using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
  public float DeclayDelay = 5;

  public GameObject CoinModel;
  public Quaternion CoinRotation;
  public float CoinLaneOffset = 0;
  public float CoinPositionXOffset;
  private int[] Lanes = new[] { -7, 0, 7 };
  private GameObject CoinSet;
  void Start()
  {
    StartCoroutine(SpawnCoin());
  }
  private IEnumerator SpawnCoin()
  {
    int Len = GetRandomLanes();
    CoinSet = new GameObject("CoinSet");
    CoinSet.transform.parent = this.gameObject.transform;
    for (int i = 0; i <= 5; i++)
    {
      var ChildCoin = Instantiate(CoinModel, new Vector3(Len - CoinLaneOffset, CoinPositionXOffset, transform.position.z + (i * 10)), Quaternion.identity);
      ChildCoin.transform.parent = CoinSet.transform;
      yield return new WaitForSeconds(1f);
    }
  }
  private int GetRandomLanes()
  {
    int start2 = Random.Range(0, Lanes.Length);
    return Lanes[start2];
  }
  void Update()
  {
    if (transform.position.z + (DeclayDelay * 10) < Character.Position.z)
      Object.Destroy(this.gameObject);
  }
}
