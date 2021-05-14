using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Player
{
  public int HighScroll;
  public int Coin;
  public Player(int HighScroll,int Coin)
  {
    this.HighScroll = HighScroll;
    this.Coin = Coin;
  }
}
