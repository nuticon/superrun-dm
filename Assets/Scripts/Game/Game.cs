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
  private bool IsDouble = false;
  public Character character;
  public CameraController cameraController;
  public UI ui;
  void Awake()
  {
    player1 = new Player();
    player1.Load();
    GetHighScroll();
    SetDefaultState();
  }
  void Update()
  {
    if (GameStarted && CountDownEnd())
    {
      CountPoint();
    }
    if (GameStarted && Character.Life <= 0)
      TriggerGameOver();
  }
  private void SetDefaultState()
  {
    Point = 0;
    Coin = 0;
    Over = false;
    CountDownEnded = false;
  }
  public void StartGame()
  {
    GameStarted = true;
  }
  public void RestartGame()
  {
    SetDefaultState();
    GetHighScroll();
    character.ResetState();
    cameraController.SetDefaultCamera();
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
  private void CountPoint()
  {
    int zPoint = (int)Character.Position.z / 10;
    if (IsDouble)
    {
      Point += (zPoint - Point) * 2;
      return;
    }
    Point += zPoint - Point;

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
    if(player1 != null) return player1.Coin;
    return 0;
  }
}
