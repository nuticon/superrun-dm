using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour
{
  public GameObject Coin;
  public int[] Lens = new[]{ -7, 0, 7 };
  // Start is called before the first frame update
  void Start()
  {
    Coin = Resources.Load("coinGold") as GameObject;
    int Len = GetRandomLens();
    for (int i = 0; i <= 5; i++)
    {
      Instantiate(Coin, new Vector3(Len, transform.position.y, (transform.position.z + 10) + (i * 10)), Quaternion.identity);
    }
  }
  // Update is called once per frame
  void Update()
  {

  }

  private int GetRandomLens()
  {
    int start2 = Random.Range(0, Lens.Length);
    return Lens[start2];
  }
}
