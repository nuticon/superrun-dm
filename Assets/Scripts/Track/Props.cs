using UnityEngine;

public class Props : MonoBehaviour
{
  private void OnCollisionEnter(Collision other)
  {
    if (other.gameObject.tag == "Props")
    {
      transform.position += Vector3.forward * 1;
    }
    if (other.gameObject.tag == "Building" || other.gameObject.tag == "Road" || other.gameObject.tag == "Water")
    {
      Object.Destroy(this.gameObject);
    }
  }
}