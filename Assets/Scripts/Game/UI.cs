using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UI : MonoBehaviour
{
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
  public Game game;
  public TrackController trackController;
  private void Start()
  {
    PlayButton.onClick.AddListener(StartTrigger);
    RetryButton.onClick.AddListener(RestartTrigger);
    ResetUI();
  }
  private void Update()
  {
    PointText.text = "M " + Game.Point.ToString();
    InGameCoinText.SetText("<sprite=0>" + Game.Coin.ToString());
  }
  private void StartTrigger()
  {
    game.StartGame();
    SetPlayingUI();
  }
  private void RestartTrigger()
  {
    trackController.RefreshTile();
    game.RestartGame();
    ResetUI();
  }
  public void SetGameOverUI()
  {
    GameOverText.gameObject.SetActive(true);
    GameOverPointText.text = Game.Point.ToString() + "M.";
    GameOverCoinText.SetText(Game.Coin.ToString() + "<sprite=0>");
    RetryButton.gameObject.SetActive(true);
    PointText.gameObject.SetActive(false);
    InGameCoinText.gameObject.SetActive(false);
  }
  private void ResetUI()
  {
    PlayButton.gameObject.SetActive(true);
    RetryButton.gameObject.SetActive(false);
    GameOverText.gameObject.SetActive(false);
    CountDownText.gameObject.SetActive(false);
    RetryButton.gameObject.SetActive(false);
    HighScrollText.gameObject.SetActive(true);
    PointText.gameObject.SetActive(false);
    PointText.text = "M " + Game.Point.ToString();
    InGameCoinText.SetText("<sprite=0>" + Game.Coin.ToString());
    InGameCoinText.gameObject.SetActive(false);
    PlayerCoinText.SetText(game.GetPlayerCoin().ToString() + "<sprite=0>");
    PlayerCoinText.gameObject.SetActive(true);
    HighScrollText.text = "HighScroll\n" + Game.CurrentHighScroll.ToString() + " M.";
  }
  private void SetPlayingUI()
  {
    CountDownText.gameObject.SetActive(true);
    PlayButton.gameObject.SetActive(false);
    HighScrollText.gameObject.SetActive(false);
    PointText.gameObject.SetActive(true);
    InGameCoinText.gameObject.SetActive(true);
    PlayerCoinText.gameObject.SetActive(false);
  }
}