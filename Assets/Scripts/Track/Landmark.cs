using UnityEngine;

public class Landmark : MonoBehaviour
{
  public string Name;
  private void Update()
  {
    if (transform.position.z - Character.Position.z < TrackController.Instance.LandmarkNameDistance)
    {
      Debug.Log(Name);
    }
  }
}