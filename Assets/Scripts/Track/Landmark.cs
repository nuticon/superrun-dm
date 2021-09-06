using UnityEngine;

public class Landmark : MonoBehaviour
{
  public string Name;
  private void Update()
  {
    if (transform.position.z - Character.Position.z < TrackController.Instance.LandmarkNameDistance)
    {
      UI.Instance.LandmarkName.text = Name;
    }
    if (transform.position.z < Character.Position.z && UI.Instance.LandmarkName.text != "")
    {
      UI.Instance.LandmarkName.text = "";
    }
  }
}