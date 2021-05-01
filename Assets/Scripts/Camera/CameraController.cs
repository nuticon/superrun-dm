using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
  // Update is called once per frame
  void Update()
  {
    if (Character.IsMoving) transform.position = new Vector3(transform.position.x ,transform.position.y , Character.Position.z - 40);
  }
}
