using UnityEngine;

public class Landmark : MonoBehaviour
{
  public string Name;
  private void Start()
  {
    Collider[] colliders = Physics.OverlapSphere(transform.position, 1000);
    foreach (Collider col in colliders)
    {
      if (col.tag == "Gate")
      {
        Object.Destroy(gameObject);
      }
    }
  }
  private void Update()
  {
    if (transform.position.z - Character.Position.z < TrackController.Instance.LandmarkNameDistance)
    {
      UI.Instance.LandmarkName.text = Name;
      UI.Instance.LandmarkGroup.gameObject.SetActive(true);
    }
    if (transform.position.z < Character.Position.z && UI.Instance.LandmarkName.text != "")
    {
      UI.Instance.LandmarkName.text = "";
      UI.Instance.LandmarkGroup.gameObject.SetActive(false);
    }
  }
}