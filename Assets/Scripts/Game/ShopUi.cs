using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class ShopUi : MonoBehaviour
{
  public Text MagnetPriceText;
  public Text DoublePriceText;
  public Text LifeText;
  public GameObject MagnetLevelBar;
  public GameObject DoubleLevelBar;
  public GameObject LifeLevelBar;
  public Text PlayerCoinText;

  public Button MagnetLevelUpBtn;
  public Button DoubleLevelUpBtn;
  public Button LifeLevelUpBtn;
  public Button BackButton;
  public Button EquipBoy;
  public Button EquipGirl;
  private PowerData powerData;
  private Player player;
  private void Start()
  {
    player = new Player();
    player.Load();
    PlayerCoinText.text = player.Coin.ToString();
    MagnetLevelUpBtn.onClick.AddListener(MagnetLevelUp);
    LifeLevelUpBtn.onClick.AddListener(LifeLevelUp);
    DoubleLevelUpBtn.onClick.AddListener(DoubleLevelUp);
    BackButton.onClick.AddListener(ToMainScene);
    EquipBoy.onClick.AddListener(EquipCharacterBoy);
    EquipGirl.onClick.AddListener(EquipCharacterGirl);
    powerData = new PowerData();
    LoadData();
  }
  void LoadData()
  {
    try
    {
      powerData.Load();
    }
    catch (System.Exception)
    {
      PowerData data = new PowerData();
      data.Save();
      throw;
    }
    finally
    {
      powerData.Load();
    }
  }
  private void Update()
  {
    PlayerCoinText.text = player.Coin.ToString();
    setBar(MagnetPriceText, MagnetLevelBar, (float)powerData.GetMagnetLevel());
    setBar(DoublePriceText, DoubleLevelBar, (float)powerData.GetDoubleLevel());
    setBar(LifeText, LifeLevelBar, (float)powerData.GetLifeLevel());
    SetCharacterEquip();
  }
  void setBar(Text priceText, GameObject bar, float level)
  {
    priceText.text = GetPrice((int)level).ToString();
    Transform dot = bar.gameObject.transform.Find("Dot");
    Image image = dot.gameObject.GetComponent<Image>();
    image.fillAmount = level / 5f;
  }
  void SetCharacterEquip()
  {
    if (player.Female)
    {
      EquipGirl.interactable = false;
      EquipBoy.interactable = true;
      EquipGirl.GetComponentInChildren<Text>().text = "Equipped";
      EquipBoy.GetComponentInChildren<Text>().text = "Equip";
    }
    else
    {
      EquipGirl.interactable = true;
      EquipBoy.interactable = false;
      EquipGirl.GetComponentInChildren<Text>().text = "Equip";
      EquipBoy.GetComponentInChildren<Text>().text = "Equipped";
    }
  }
  void EquipCharacterBoy()
  {
    player.Female = false;
    player.Save();
  }
  void EquipCharacterGirl()
  {
    player.Female = true;
    player.Save();
  }
  int GetPrice(int level)
  {
    return 500 * (level + 1);
  }
  void ToMainScene()
  {
    SceneManager.LoadScene("Main", LoadSceneMode.Single);
  }
  public void MagnetLevelUp()
  {
    int price = GetPrice(powerData.GetMagnetLevel());
    PowerModel magnet = new PowerModel(powerData.GetMagnetLevel());
    if (player.Coin >= price)
    {
      powerData.SetMagnetLevel(magnet.LevelUp());
      player.Coin -= price;
      powerData.Save();
      player.Save();
    }
  }
  public void DoubleLevelUp()
  {
    int price = GetPrice(powerData.GetDoubleLevel());
    PowerModel doubles = new PowerModel(powerData.GetDoubleLevel());
    if (player.Coin >= price)
    {
      powerData.SetDoubleLevel(doubles.LevelUp());
      player.Coin -= price;
      powerData.Save();
      player.Save();
    }
  }
  public void LifeLevelUp()
  {
    int price = GetPrice(powerData.GetLifeLevel());
    PowerModel life = new PowerModel(powerData.GetLifeLevel());
    if (player.Coin >= price)
    {
      powerData.SetLifeLevel(life.LevelUp());
      player.Coin -= price;
      powerData.Save();
      player.Save();
    }
  }
}
