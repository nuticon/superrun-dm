using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
  public TMP_Text PlayerCoinText;
  public TMP_Text InGameCoinText;
  public Text GameOverPointText;
  public Text CountDownText;
  public Text HighScrollText;
  public TMP_Text GameOverCoinText;
  public Button PlayButton;
  public Button RetryButton;
  private float CountDownTimer;
  private float PointCountTimer;
  private int CountDown = 3;
  private GameObject TileSet;
  private Player player1;
  private bool IsDouble = false;
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
      PointText.text = "M " + Point.ToString();
      InGameCoinText.SetText("<sprite=0>" + Coin.ToString());
    }
    if (GameStarted && Character.Life <= 0)
      TriggerGameOver();
  }
  private void SetDefaultState()
  {
    PlayButton.gameObject.SetActive(true);
    GameOverText.gameObject.SetActive(false);
    CountDownText.gameObject.SetActive(false);
    RetryButton.gameObject.SetActive(false);
    RetryButton.onClick.AddListener(RestartGame);
    HighScrollText.gameObject.SetActive(true);
    PointText.gameObject.SetActive(false);
    Point = 0;
    PointText.text = "M " + Point.ToString();
    Coin = 0;
    InGameCoinText.SetText("<sprite=0>" + Coin.ToString());
    InGameCoinText.gameObject.SetActive(false);
    PlayerCoinText.SetText(player1.Coin.ToString() + "<sprite=0>");
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
      HighScrollText.text = "HighScroll\n" + player1.HighScroll.ToString() + " M.";
    else
      HighScrollText.text = "HighScroll\n0 M.";

  }
  private void TriggerGameOver()
  {
    GameStarted = false;
    Over = true;
    GameOverText.gameObject.SetActive(true);
    GameOverPointText.text = Point.ToString() + "M.";
    GameOverCoinText.SetText(Coin.ToString() + "<sprite=0>");
    SavePlayer(Point);
    RetryButton.gameObject.SetActive(true);
    PointText.gameObject.SetActive(false);
    InGameCoinText.gameObject.SetActive(false);
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
