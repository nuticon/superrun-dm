using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Menu : MonoBehaviour
{
  public Button PlayButton;
  public Button QuitButton;
  private Setting setting;

  public Sprite SFXButtonEnableIcon;
  public Sprite SFXButtonDisabledIcon;
  public Sprite MusicButtonEnableIcon;
  public Sprite MusicButtonDisabledIcon;
  public Button MusicButton;
  public Button SFXButton;
  private void Start()
  {
    PlayButton.onClick.AddListener(ToGame);
    QuitButton.onClick.AddListener(Application.Quit);
    MusicButton.onClick.AddListener(SwitchMusic);
    SFXButton.onClick.AddListener(SwitchSFX);
    setting = new Setting();
    setting.Load();
  }
  private void Update()
  {
    MusicButton.GetComponent<Image>().sprite = setting.Music ? MusicButtonEnableIcon : MusicButtonDisabledIcon;
    SFXButton.GetComponent<Image>().sprite = setting.SFX ? SFXButtonEnableIcon : SFXButtonDisabledIcon;
  }
  void ToGame()
  {
    SceneManager.LoadScene("Main", LoadSceneMode.Single);
  }
  void SwitchSFX()
  {
    setting.SFX = !setting.SFX;
    setting.Save();
  }
  void SwitchMusic()
  {
    setting.Music = !setting.Music;
    setting.Save();
  }
}
