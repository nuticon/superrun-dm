using UnityEngine;

public class PowerUp : MonoBehaviour
{
  private void OnTriggerEnter(Collider other)
  {
    if (other.gameObject.tag == "Player") Power.GlobalPower = this.gameObject.tag;
    Object.Destroy(this.gameObject);
  }
}