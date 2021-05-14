using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
  private float DeclayDelay = 5;
  private GameObject Coin;
  public float CoinPositionXOffset;
  private int[] Lanes = new[] { -7, 0, 7 };
  void Start()
  {
    Coin = Resources.Load("coinGold") as GameObject;
    int Len = GetRandomLanes();
    for (int i = 0; i <= 5; i++)
    {
      var ChildCoin = Instantiate(Coin, new Vector3(Len, CoinPositionXOffset, transform.position.z + (i * 10)), Quaternion.identity);
      ChildCoin.transform.parent = this.gameObject.transform;
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
