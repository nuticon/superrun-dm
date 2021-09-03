using UnityEngine;

[System.Serializable]
public class Player
{
  public int HighScroll = 0;
  public int Coin = 0;
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
    if (player != null)
    {
      this.HighScroll = player.HighScroll;
      this.Coin = player.Coin;
      this.Female = player.Female;
      Debug.Log("Player loaded");
    }
  }
}
