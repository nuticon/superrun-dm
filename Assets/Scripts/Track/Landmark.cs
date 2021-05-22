using UnityEngine;

public class Landmark : MonoBehaviour
{
  void Update()
  {
    if (Game.GameStarted && !Game.Over && Game.CountDownEnded) Move();
  }
  void Move()
  {
    // Vector3 TargetPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z + Character.Position.z);
    // transform.position = Vector3.Lerp(transform.position, TargetPosition, Time.deltaTime * CharacterMovement.Speed * CharacterMovement.SpeedMultiplier);
    transform.position += Vector3.forward * Time.deltaTime * CharacterMovement.Speed * CharacterMovement.SpeedMultiplier;
  }
}