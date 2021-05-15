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
  public Text PlayerCoinText;
  public Text InGameCoinText;
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
  protected Player player1;
  void Start()
  {
    PlayButton.onClick.AddListener(StartGame);
    player1 = GetPlayer();
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
      InGameCoinText.text = "Coin " + Coin.ToString();
    }
    if (GameStarted && Over)
      TriggerGameOver();
  }
  private void SetDefaultState()
  {
    PlayButton.gameObject.SetActive(true);
    GameOverText.gameObject.SetActive(false);
    GameOverPointText.gameObject.SetActive(false);
    CountDownText.gameObject.SetActive(false);
    RetryButton.gameObject.SetActive(false);
    RetryButton.onClick.AddListener(RestartGame);
    HighScrollText.gameObject.SetActive(true);
    PointText.gameObject.SetActive(false);
    Point = 0;
    PointText.text = "Point " + Point.ToString();
    Coin = 0;
    InGameCoinText.text = "Coin " + Coin.ToString();
    InGameCoinText.gameObject.SetActive(false);
    PlayerCoinText.text = "Coin " + player1.Coin.ToString();
    PlayerCoinText.gameObject.SetActive(true);
    Over = false;
    CountDownEnded = false;
  }
  private void ResetTile()
  {
    foreach (Transform Tile in TileSet.transform)
    {
        Object.Destroy(Tile.gameObject);
    }
    TrackController.LastTilePosition = new Vector3(0, 0, 0);
  }
  private void StartGame()
  {
    GameStarted = true;
    CountDownText.gameObject.SetActive(true);
    PlayButton.gameObject.SetActive(false);
    HighScrollText.gameObject.SetActive(false);
    PointText.gameObject.SetActive(true);
    InGameCoinText.gameObject.SetActive(true);
    PlayerCoinText.gameObject.SetActive(false);
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
  void SavePlayer(int Scroll)
  {
    if (player1 == null) player1 = new Player(Scroll, 0);
    if (Scroll > player1.HighScroll) player1.HighScroll = Scroll;
    player1.Coin += Coin;
    Storage.SavePlayer(player1);
  }
  Player GetPlayer()
  {
    Player player = Storage.LoadPlayer();
    if (player != null) return player;
    return null;
  }
  void GetHighScroll()
  {
    if (player1 != null)
      HighScrollText.text = "HighScroll\n" + player1.HighScroll.ToString();
    else
      HighScrollText.text = "HighScroll\n0";

  }
  private void TriggerGameOver()
  {
    GameStarted = false;
    GameOverText.gameObject.SetActive(true);
    GameOverPointText.text = "Points " + Point.ToString() + "\nCoin " + Coin.ToString();
    SavePlayer(Point);
    GameOverPointText.gameObject.SetActive(true);
    RetryButton.gameObject.SetActive(true);
    PointText.gameObject.SetActive(false);
    InGameCoinText.gameObject.SetActive(false);
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
