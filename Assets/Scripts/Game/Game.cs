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
  public Text PointText;
  public Text GameOverText;
  public Text CoinText;
  public Text GameOverPointText;
  public Button PlayButton;
  public Button RetryButton;
  private float Timer;

  void Start()
  {
    PlayButton.onClick.AddListener(StartGame);
    GameOverText.gameObject.SetActive(false);
    GameOverPointText.gameObject.SetActive(false);
    RetryButton.gameObject.SetActive(false);
    RetryButton.onClick.AddListener(RestartGame);
  }
  void Update()
  {
    if (GameStarted)
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
    PlayButton.gameObject.SetActive(false);
  }
  private void RestartGame()
  {
    Character.RequireRestart = true;
    Point = 0;
    GameStarted = true;
    Over = false;
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
}
