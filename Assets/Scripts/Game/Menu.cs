using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Menu : MonoBehaviour
{
  public Button PlayButton;
  public Button QuitButton;
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
    Music.Instance.PlayMenuMusic();
  }
  private void Update()
  {
    MusicButton.GetComponent<Image>().sprite =  SettingCache.Instance.setting.Music ? MusicButtonEnableIcon : MusicButtonDisabledIcon;
    SFXButton.GetComponent<Image>().sprite =  SettingCache.Instance.setting.SFX ? SFXButtonEnableIcon : SFXButtonDisabledIcon;
  }
  void ToGame()
  {
    SceneManager.LoadScene("Main", LoadSceneMode.Single);
  }
  void SwitchSFX()
  {
    SettingCache.Instance.setting.SFX = !SettingCache.Instance.setting.SFX;
    SettingCache.Instance.setting.Save();
  }
  void SwitchMusic()
  {
    SettingCache.Instance.setting.Music = !SettingCache.Instance.setting.Music;
    SettingCache.Instance.setting.Save();
  }
}
