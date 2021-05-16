using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
  public Vector3 RunningCameraPosition;
  public Quaternion RunningCameraRotation;
  public Vector3 StartCameraPosition;
  public Quaternion StartCameraRotation;
  public static bool RequestCameraReset = false;
  private void Start()
  {
    transform.position = StartCameraPosition;
    transform.rotation = StartCameraRotation;
  }
  void Update()
  {
    if (RequestCameraReset)
    {
      SetDefaultCamera();
      return;
    }
    if (Game.GameStarted && !Game.Over && !Game.CountDownEnded) RotateCamera();
    if (Game.GameStarted && !Game.Over && Game.CountDownEnded) MoveCamera();
  }

  void SetDefaultCamera()
  {
    transform.position = StartCameraPosition;
    transform.rotation = StartCameraRotation;
    RequestCameraReset = false;
  }
  void RotateCamera()
  {
    Vector3 TargetPosition = new Vector3(RunningCameraPosition.x, RunningCameraPosition.y, Character.Position.z + RunningCameraPosition.z);
    transform.position = Vector3.Lerp(transform.position, TargetPosition, Time.deltaTime * 1.5f);
    Quaternion TargetRotation = Quaternion.Euler(RunningCameraRotation.x, RunningCameraRotation.y, RunningCameraRotation.z);
    transform.rotation = Quaternion.Slerp(transform.rotation, TargetRotation, Time.deltaTime * 1.5f);
  }
  void MoveCamera()
  {
    if (transform.rotation != RunningCameraRotation)
    {
      Quaternion TargetRotation = Quaternion.Euler(RunningCameraRotation.x, RunningCameraRotation.y, RunningCameraRotation.z);
      transform.rotation = Quaternion.Slerp(transform.rotation, TargetRotation, Time.deltaTime * 1.5f);
    }
    Vector3 TargetPosition = new Vector3(RunningCameraPosition.x, RunningCameraPosition.y, Character.Position.z + RunningCameraPosition.z);
    transform.position = Vector3.Lerp(transform.position, TargetPosition, Time.deltaTime * CharacterMovement.Speed * CharacterMovement.SpeedMultiplier);
  }
}
