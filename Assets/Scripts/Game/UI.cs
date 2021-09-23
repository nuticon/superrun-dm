using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class UI : MonoBehaviour
{
  public static UI Instance;
  [Header("UI Component")]
  public Button PlayButton;
  public Button RetryButton;
  public Button HomeButton;
  public Button HomeOverButton;
  public Button ShopButton;
  public Button OverShopButton;
  public Game game;
  public Text GameOverCoinText;
  public Text InGameCoinText;
  public TMP_Text PlayerCoinText;
  public TMP_Text PlayerLifeText;
  public TMP_Text PowerUpSet;
  public Text CountDownText;
  public Text GameOverPointText;
  public Text GameOverText;
  public Text HighScrollText;
  public Text PointText;
  public Text LandmarkName;
  public GameObject LandmarkGroup;
  public GameObject GameOverGroup;
  public GameObject InGameGroup;
  public GameObject MainGroup;

  [Header("Other")]
  public TrackController trackController;
  private void Awake()
  {
      Instance = this;
  }
  private void Start()
  {
    PlayButton.onClick.AddListener(StartTrigger);
    RetryButton.onClick.AddListener(RestartTrigger);
    ShopButton.onClick.AddListener(ToShop);
    OverShopButton.onClick.AddListener(ToShop);
    HomeButton.onClick.AddListener(ToMenu);
    HomeOverButton.onClick.AddListener(ToMenu);
    LandmarkName.text = "";
    ResetUI();
  }
  private void Update()
  {
    InGameCoinText.text = "" + Game.Coin.ToString();
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
    GameOverCoinText.text = Game.Coin.ToString() + "";
    GameOverPointText.text = Game.Point.ToString() + "";
    GameOverGroup.SetActive(true);
    InGameGroup.SetActive(false);
  }
  public void ResetUI()
  {
    CountDownText.gameObject.SetActive(false);
    HighScrollText.text = "Highest Score\n" + Game.CurrentHighScroll.ToString() + " M.";
    PlayerCoinText.SetText(game.GetPlayerCoin().ToString() + "");
    PowerUpSet.SetText("");
    MainGroup.SetActive(true);
    GameOverGroup.SetActive(false);
    InGameGroup.SetActive(false);
  }
  private void SetPlayingUI()
  {
    CountDownText.gameObject.SetActive(true);
    MainGroup.SetActive(false);
    GameOverGroup.SetActive(false);
    InGameGroup.SetActive(true);
  }
  public void ToShop()
  {
    SceneManager.LoadScene("Shop", LoadSceneMode.Single);
  }
  public void ToMenu()
  {
    SceneManager.LoadScene("Menu", LoadSceneMode.Single);
  }
}