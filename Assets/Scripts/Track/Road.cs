using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour
{
  private GameObject Coin;
  public float CoinPositionXOffset;
  private int[] Lanes = new[] { -7, 0, 7 };
  void Start()
  {
    Coin = Resources.Load("coinGold") as GameObject;
    int Len = GetRandomLanes();
    for (int i = 0; i <= 5; i++)
    {
      var ChildCoin = Instantiate(Coin, new Vector3(Len, CoinPositionXOffset, (transform.position.z + 10) + (i * 10)), Quaternion.identity);
      ChildCoin.transform.parent = this.gameObject.transform;
    }
  }
  private int GetRandomLanes()
  {
    int start2 = Random.Range(0, Lanes.Length);
    return Lanes[start2];
  }
}
