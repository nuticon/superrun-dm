using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
  void Update()
  {
    Vector3 TargetPosition = new Vector3(transform.position.x, transform.position.y, Character.Position.z - 36.97f);
    transform.position = Vector3.Lerp(transform.position, TargetPosition, Time.deltaTime * Character.Speed * Character.SpeedMultiplier);
  }
}
