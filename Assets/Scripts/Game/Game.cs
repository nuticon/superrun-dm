using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
  public static bool Over = false;
  public static int Point = 0;
  public static int Coin = 0;
  public static bool GameStarted = false;
  public static bool CountDownEnded = false;
  public Text PointText;
  public Text GameOverText;
  public Text CoinText;
  public Text GameOverPointText;
  public Text CountDownText;
  public Button PlayButton;
  public Button RetryButton;
  private float Timer;
  private int CountDown = 3;
  protected GameObject TileSet;
  void Start()
  {
    PlayButton.onClick.AddListener(StartGame);
    GameOverText.gameObject.SetActive(false);
    GameOverPointText.gameObject.SetActive(false);
    CountDownText.gameObject.SetActive(false);
    RetryButton.gameObject.SetActive(false);
    RetryButton.onClick.AddListener(RestartGame);
    TileSet = new GameObject("TileSet");
  }
  void Update()
  {
    if (GameStarted && CountDownEnd())
    {
      PointText.text = "Point " + Point.ToString();
      CoinText.text = "Coin " + Coin.ToString();
    }
    if (GameStarted && Over)
      TriggerGameOver();
  }
  private void StartGame()
  {
    GameStarted = true;
    CountDownText.gameObject.SetActive(true);
    PlayButton.gameObject.SetActive(false);
  }
  private void RestartGame()
  {
    Object.Destroy(TileSet.gameObject);
    CountDownEnded = false;
    CountDownText.gameObject.SetActive(true);
    Character.RequireRestart = true;
    Point = 0;
    Coin = 0;
    GameStarted = true;
    Over = false;
    TileSet = new GameObject("TileSet");
    TrackController.LastTilePosition = new Vector3(0, 0, 0);
    PlayButton.onClick.AddListener(StartGame);
    GameOverText.gameObject.SetActive(false);
    GameOverPointText.gameObject.SetActive(false);
    RetryButton.gameObject.SetActive(false);
  }
  private void TriggerGameOver()
  {
    GameStarted = false;
    GameOverText.gameObject.SetActive(true);
    GameOverPointText.text = "Points "
      + Point.ToString()
      + "\nCoins "
      + Coin.ToString()
      + "\nTotal "
      + (Point + Coin).ToString();
    GameOverPointText.gameObject.SetActive(true);
    RetryButton.gameObject.SetActive(true);
  }

  private bool CountDownEnd()
  {
    if (CountDownEnded) return true;
    if (CountDown == 0)
    {
      CountDownText.gameObject.SetActive(false);
      CountDownEnded = true;
      CountDown = 3;
      CountDownText.text = CountDown.ToString();
      return true;
    }
    Timer += Time.deltaTime;
    if (Timer >= 1)
    {
      Timer = 0;
      CountDown--;
      CountDownText.text = CountDown.ToString();
    }
    return false;
  }
}
