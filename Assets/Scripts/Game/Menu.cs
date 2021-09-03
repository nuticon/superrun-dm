using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Menu : MonoBehaviour
{
  public Button PlayButton;
  public Button QuitButton;
  private void Start()
  {
    PlayButton.onClick.AddListener(ToGame);
    QuitButton.onClick.AddListener(Application.Quit);
  }
  void ToGame()
  {
    SceneManager.LoadScene("Main", LoadSceneMode.Single);
  }
}
