using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
  // Update is called once per frame
  void Update()
  {
    if (Character.IsMoving)
    {
      Vector3 TargetPosition = new Vector3(transform.position.x, transform.position.y, Character.Position.z - 36.97f);
      transform.position = Vector3.Lerp(transform.position, TargetPosition,Time.deltaTime * Character.Speed * Character.SpeedMultiplier);
    }
  }
}
