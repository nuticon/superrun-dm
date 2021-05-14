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
  [Header("UI Component")]
  public Text PointText;
  public Text GameOverText;
  public Text CoinText;
  public Text GameOverPointText;
  public Text CountDownText;
  public Text HighScrollText;
  public Button PlayButton;
  public Button RetryButton;
  [Header("Settings")]
  public float PointCountDelay = 0.05f;
  private float CountDownTimer;
  private float PointCountTimer;
  private int CountDown = 3;
  protected GameObject TileSet;
  void Start()
  {
    PlayButton.onClick.AddListener(StartGame);
    GetHighScroll();
    SetDefaultState();
    TileSet = new GameObject("TileSet");
  }
  void Update()
  {
    if (GameStarted && CountDownEnd())
    {
      CountPoint();
      PointText.text = "Point " + Point.ToString();
      CoinText.text = "Coin " + Coin.ToString();
    }
    if (GameStarted && Over)
      TriggerGameOver();
  }
  private void SetDefaultState()
  {
    GameOverText.gameObject.SetActive(false);
    GameOverPointText.gameObject.SetActive(false);
    CountDownText.gameObject.SetActive(false);
    RetryButton.gameObject.SetActive(false);
    RetryButton.onClick.AddListener(RestartGame);
    HighScrollText.gameObject.SetActive(true);
    Point = 0;
    Coin = 0;
    Over = false;
    CountDownEnded = false;
  }
  private void ResetTile()
  {
    Object.Destroy(TileSet.gameObject);
    TileSet = new GameObject("TileSet");
    TrackController.LastTilePosition = new Vector3(0, 0, 0);
  }
  private void StartGame()
  {
    GameStarted = true;
    CountDownText.gameObject.SetActive(true);
    PlayButton.gameObject.SetActive(false);
    HighScrollText.gameObject.SetActive(false);
    PointText.gameObject.SetActive(true);
    CoinText.gameObject.SetActive(true);
  }
  private void RestartGame()
  {
    ResetTile();
    SetDefaultState();
    Character.RequireRestart = true;
    CameraController.RequestCameraReset = true;
    GetHighScroll();
    RetryButton.gameObject.SetActive(false);
  }
  void SetHighScroll(int Scroll)
  {
    Player player = Storage.LoadPlayer();
    if (player == null)
    {
      Storage.SavePlayer(new Player(Scroll));
      return;
    }
    if (Scroll > player.HighScroll) Storage.SavePlayer(new Player(Scroll));
  }
  void GetHighScroll()
  {
    Player player = Storage.LoadPlayer();
    if (player != null)
      HighScrollText.text = "HighScroll\n" + player.HighScroll.ToString();
    else
      HighScrollText.text = "HighScroll\n0";

  }
  private void TriggerGameOver()
  {
    GameStarted = false;
    GameOverText.gameObject.SetActive(true);
    int TotalPoint = Point + Coin;
    GameOverPointText.text = "Points "
      + Point.ToString()
      + "\nCoins "
      + Coin.ToString()
      + "\nTotal "
      + TotalPoint.ToString();
    SetHighScroll(TotalPoint);
    GameOverPointText.gameObject.SetActive(true);
    RetryButton.gameObject.SetActive(true);
    PointText.gameObject.SetActive(false);
    CoinText.gameObject.SetActive(false);
  }
  private void CountPoint()
  {
    PointCountTimer += Time.deltaTime;
    if (PointCountTimer >= PointCountDelay)
    {
      Point++;
      PointCountTimer = 0;
    }
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
    CountDownTimer += Time.deltaTime;
    if (CountDownTimer >= 1)
    {
      CountDownTimer = 0;
      CountDown--;
      CountDownText.text = CountDown.ToString();
    }
    return false;
  }
}
