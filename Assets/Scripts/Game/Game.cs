using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
  public static bool Over = false;
  public static int Point = 0;
  public static int Coin = 0;
  public static bool GameStarted = false;
  public static bool CountDownEnded = false;
  public static int CurrentHighScroll;
  private float CountDownTimer;
  private int CountDown = 3;
  internal Player player1;
  public CameraController cameraController;
  public UI ui;
  private int LocalCoin = 0;

  private string TempPowerUpText;
  public static bool MagnetActive;
  public GameObject Boy;
  public GameObject Girl;
  void Start()
  {
    player1 = new Player();
    player1.Load();
    SetDefaultState();
    ui.ResetUI();
    if (player1.Female)
    {
      var character = Instantiate(Girl, transform.position, Quaternion.identity);
      character.transform.parent = gameObject.transform;
      character.transform.position = Character.Instance.Offset;
    }
    else
    {
      var character = Instantiate(Boy, transform.position, Quaternion.identity);
      character.transform.parent = gameObject.transform;
      character.transform.position = Character.Instance.Offset;
    }
  }
  void Update()
  {
    if (GameStarted && CountDownEnd())
    {
      WatchCoin();
      WatchPowerUp();
    }
    if (GameStarted && Character.Life <= 0)
      TriggerGameOver();
  }
  private void SetDefaultState()
  {
    GetHighScroll();
    Music.Instance.PlayMenuMusic();
    Point = 0;
    Coin = 0;
    LocalCoin = Coin;
    Over = false;
    CountDownEnded = false;
  }
  public void StartGame()
  {
    GameStarted = true;
    Character.Instance.animator.SetTrigger("IsIdle");
    Music.Instance.PlayInGameMusic();
    Debug.Log("Game Started");
  }
  public void RestartGame()
  {
    SetDefaultState();
    Character.Instance.ResetState();
    cameraController.SetDefaultCamera();
    Power.Instance.Double.Disable();
    Power.Instance.Magnet.Disable();
    Debug.Log("Game reseted");
  }
  void SavePlayer(int Scroll)
  {
    if (player1 == null) player1 = new Player(Scroll, 0);
    if (Scroll > player1.HighScroll) player1.HighScroll = Scroll;
    player1.Coin += Coin;
    player1.Save();
  }
  void GetHighScroll()
  {
    if (player1 != null)
      CurrentHighScroll = player1.HighScroll;
    else
      CurrentHighScroll = 0;
  }
  private void TriggerGameOver()
  {
    GameStarted = false;
    Over = true;
    ui.SetGameOverUI();
    SavePlayer(Point);
  }
  private bool CountDownEnd()
  {
    if (CountDownEnded) return true;
    if (CountDown == 0)
    {
      ui.CountDownText.gameObject.SetActive(false);
      CountDownEnded = true;
      CountDown = 3;
      ui.CountDownText.text = CountDown.ToString();
      return true;
    }
    CountDownTimer += Time.deltaTime;
    if (CountDownTimer >= 1)
    {
      CountDownTimer = 0;
      CountDown--;
      ui.CountDownText.text = CountDown.ToString();
    }
    return false;
  }

  public int GetPlayerCoin()
  {
    if (player1 != null) return player1.Coin;
    return 0;
  }

  public void WatchCoin()
  {
    if (Coin > LocalCoin)
    {
      Character.Instance.CoinUp();
      LocalCoin = Coin;
    }
  }
  private void WatchPowerUp()
  {
    MagnetActive = Power.Instance.MagnetActivating();
    TempPowerUpText = "";
    if (Power.Instance.DoubleActivating()) TempPowerUpText += "X2 " + Power.Instance.Double.TimeLeft + "\n";
    if (Power.Instance.MagnetActivating()) TempPowerUpText += "Magnet " + Power.Instance.Magnet.TimeLeft + "\n";
    ui.PowerUpSet.SetText(TempPowerUpText);
  }
}
