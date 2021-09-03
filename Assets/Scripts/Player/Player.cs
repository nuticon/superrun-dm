using UnityEngine;

[System.Serializable]
public class Player
{
  public int HighScroll;
  public int Coin;
  public bool Female = false;
  public Player(int HighScroll, int Coin)
  {
    this.HighScroll = HighScroll;
    this.Coin = Coin;
  }
  public Player()
  {

  }
  public void Save()
  {
    Storage.SavePlayer(this);
    Debug.Log("Player saved");
  }
  public void Load()
  {
    Player player = Storage.LoadPlayer();
    this.HighScroll = player.HighScroll;
    this.Coin = player.Coin;
    this.Female = player.Female;
    Debug.Log("Player loaded");
  }
}
