using UnityEngine;

public class Road : MonoBehaviour {
  private void OnTriggerStay(Collider other) {
    if (other.gameObject.tag == "Player")
    {
      CharacterMovement.IsGrounded = true;
    }
  }
  private void OnTriggerExit(Collider other) {
    if (other.gameObject.tag == "Player")
    {
      CharacterMovement.IsGrounded = false;
    }
  }
}