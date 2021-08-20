using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UI : MonoBehaviour
{
  [Header("UI Component")]
  public Button PlayButton;
  public Button RetryButton;
  public Game game;
  public TMP_Text GameOverCoinText;
  public TMP_Text InGameCoinText;
  public TMP_Text PlayerCoinText;
  public TMP_Text PlayerLifeText;
  public TMP_Text PowerUpSet;
  public Text CountDownText;
  public Text GameOverPointText;
  public Text GameOverText;
  public Text HighScrollText;
  public Text PointText;

  [Header("Other")]
  public TrackController trackController;
  private void Start()
  {
    PlayButton.onClick.AddListener(StartTrigger);
    RetryButton.onClick.AddListener(RestartTrigger);
    ResetUI();
  }
  private void Update()
  {
    InGameCoinText.SetText("" + Game.Coin.ToString());
    PlayerLifeText.SetText("" + Character.Life.ToString());
    PointText.text = "" + Game.Point.ToString();
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
    GameOverCoinText.SetText(Game.Coin.ToString() + "");
    GameOverPointText.text = Game.Point.ToString() + "";
    GameOverText.gameObject.SetActive(true);
    InGameCoinText.gameObject.SetActive(false);
    PointText.gameObject.SetActive(false);
    RetryButton.gameObject.SetActive(true);
  }
  public void ResetUI()
  {
    CountDownText.gameObject.SetActive(false);
    GameOverText.gameObject.SetActive(false);
    HighScrollText.gameObject.SetActive(true);
    HighScrollText.text = "Highest Score\n" + Game.CurrentHighScroll.ToString() + " M.";
    InGameCoinText.gameObject.SetActive(false);
    PlayButton.gameObject.SetActive(true);
    PlayerCoinText.SetText(game.GetPlayerCoin().ToString() + "");
    PlayerCoinText.gameObject.SetActive(true);
    PlayerLifeText.gameObject.SetActive(false);
    PointText.gameObject.SetActive(false);
    RetryButton.gameObject.SetActive(false);
    RetryButton.gameObject.SetActive(false);
    PowerUpSet.gameObject.SetActive(false);
    PowerUpSet.SetText("");
  }
  private void SetPlayingUI()
  {
    CountDownText.gameObject.SetActive(true);
    HighScrollText.gameObject.SetActive(false);
    InGameCoinText.gameObject.SetActive(true);
    PlayButton.gameObject.SetActive(false);
    PlayerCoinText.gameObject.SetActive(false);
    PlayerLifeText.gameObject.SetActive(true);
    PointText.gameObject.SetActive(true);
    PowerUpSet.gameObject.SetActive(true);
  }
}