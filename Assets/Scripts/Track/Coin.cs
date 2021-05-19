using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
  Animator animator;
  float TargetPosition = 0;
  private void Update()
  {
    if (Game.MagnetActive)
    {
      if (transform.position.z - Character.Position.z <= 20)
      {
        if (TargetPosition == 0) TargetPosition = Character.Position.z + 10 + CharacterMovement.Speed;
        Vector3 Target = new Vector3(Character.Position.x, Character.Position.y, TargetPosition);
        transform.position = Vector3.Lerp(transform.position, Target, Time.deltaTime * 15f);
        return;
      }
    }
  }
  private void OnTriggerEnter(Collider other)
  {
    if (other.gameObject.tag == "Player")
    {
      Game.Coin++;
      Object.Destroy(this.gameObject);
    }
  }
}
