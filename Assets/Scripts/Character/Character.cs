using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
  [Header("Character")]

  public float JumpStrength = 1.2f;
  public float SlideLength = 1.0f;

  [Header("Game")]
  public float MinSpeed = 1.0f;
  public float MaxSpeed = 10.0f;
  public float PointDelay = 0.5f;
  public float LensOffset = 7.0f;
  public float SideWaySpeed = 1.0f;

  //Helper Value
  public static float Speed;
  public static float SpeedMultiplier = 25;
  private float CalculatedSlideLength;
  private float SlideAnimatorPauseAt;
  private bool Jumping = false;
  private float CalculatedJumpStrength;
  private int JumpingFrame = 0;
  private float JumpFrame;
  private float JumpFrameCenter;
  private bool Sliding = false;
  private int SlidingFrame = 0;
  private float CalculatedSideWaySpeed;
  private float TempAnimatorSpeed;
  private float Timer;
  /**
    * -1 left
    * 0 center
    * 1 center
    */
  [Range(-1, 1)]
  private int Lens = 0;
  public static bool IsMoving = false;
  private float TargetLenOffset;
  public static bool RequireRestart = false;
  //Instance
  private Animator animator;
  private BoxCollider Collider;
  public static Vector3 Position;
  private Vector3 DefaultColliderCenter;
  private Vector3 DefaultColliderSize;

  void Start()
  {
    //Get Component
    animator = GetComponent<Animator>();
    Collider = GetComponent<BoxCollider>();

    //Set default value
    Speed = MinSpeed;
    JumpFrame = JumpStrength * 50;
    JumpFrameCenter = JumpFrame / 2;
    CalculatedJumpStrength = JumpStrength * 5;
    CalculatedSlideLength = SlideLength * 50;
    CalculatedSideWaySpeed = SideWaySpeed * 25;
    SlideAnimatorPauseAt = CalculatedSlideLength / SlideLength;
    transform.position = new Vector3(0, 0, 0);
    Position = transform.position;
    DefaultColliderCenter = Collider.center;
    DefaultColliderSize = Collider.size;

  }
  void Update()
  {
    if (!IsMoving && RequireRestart) ResetState();
    //On Moving
    if (IsMoving && !Game.Over)
    {
      if (Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.UpArrow)) Jump();
      if (Input.GetKeyDown(KeyCode.DownArrow)) Slide();
      if (Input.GetKeyDown(KeyCode.RightArrow)) ChangeLensRight();
      if (Input.GetKeyDown(KeyCode.LeftArrow)) ChangeLensLeft();
      Run();
      if (Sliding) CheckSlidingFrame();
      if (Jumping) CheckJumpingFrame();
    }

    //On Game Over
    if (IsMoving && Game.Over) Over();
    if (!IsMoving && Game.GameStarted && !RequireRestart) StartMoving();
  }
  private void ResetState()
  {
    transform.position = new Vector3(0, 0, 0);
    Position = transform.position;
    Speed = MinSpeed;
    StartMoving();
    RequireRestart = false;
  }
  private bool IsNotMaxSpeed()
  {
    if (Speed != MaxSpeed) return true;
    return false;
  }
  private void Over()
  {
    StopMoving();
    StopJump();
    StopSlide();
    animator.SetTrigger("IsOver");
  }
  private void Run()
  {
    transform.position += Vector3.forward * Time.deltaTime * Speed * SpeedMultiplier;
    Vector3 interpolPostion = new Vector3(TargetLenOffset, transform.position.y, transform.position.z);
    transform.position = Vector3.Lerp(transform.position, interpolPostion, Time.deltaTime * CalculatedSideWaySpeed);
    Position = transform.position;
    if (IsNotMaxSpeed()) Speed += 0.001f;
    if (animator.speed < 2 && !Jumping && !Sliding)
    {
      animator.speed = Speed - 0.5f;
      TempAnimatorSpeed = animator.speed;
    }
    CountPoint();
  }
  private void CountPoint()
  {
    Timer += Time.deltaTime;
    if (Timer >= PointDelay)
    {
      Game.Point++;
      Timer = 0;
    }
  }
  private void Jump()
  {
    if (!Jumping)
    {
      animator.SetTrigger("IsJumpping");
      Jumping = true;
    }
  }
  private void StopJump()
  {
    Jumping = false;
  }
  private void CheckJumpingFrame()
  {
    if (Jumping)
    {
      if (JumpingFrame <= JumpFrame)
      {
        Vector3 interpolPostionUp = new Vector3(transform.position.x, CalculatedJumpStrength, transform.position.z);
        Vector3 interpolPostionDown = new Vector3(transform.position.x, 0, transform.position.z);
        JumpingFrame++;
        if (JumpingFrame < JumpFrameCenter) transform.position = transform.position = Vector3.Lerp(transform.position, interpolPostionUp, Time.deltaTime * 10);
        if (JumpingFrame > JumpFrameCenter) transform.position = transform.position = Vector3.Lerp(transform.position, interpolPostionDown, Time.deltaTime * 10);
        if (JumpingFrame == JumpFrameCenter) animator.speed = 0;
      }
      else
      {
        Jumping = false;
        JumpingFrame = 0;
        animator.speed = TempAnimatorSpeed;
        transform.position = new Vector3(transform.position.x, 0, transform.position.z);
      }
    }
  }
  private void Slide()
  {
    animator.SetTrigger("IsSlide");
    Sliding = true;
  }
  private void StopSlide()
  {
    Sliding = false;
  }
  private void CheckSlidingFrame()
  {
    if (Sliding)
    {
      if (SlidingFrame <= CalculatedSlideLength)
      {
        SlidingFrame++;
        if (SlidingFrame == SlideAnimatorPauseAt) animator.speed = 0;
        Collider.size = new Vector3(DefaultColliderSize.x, 0.02f, DefaultColliderSize.z);
        Collider.center = new Vector3(DefaultColliderCenter.x, 0.015f, DefaultColliderCenter.z);
      }
      else
      {
        Sliding = false;
        SlidingFrame = 0;
        animator.speed = TempAnimatorSpeed;
        Collider.size = DefaultColliderSize;
        Collider.center = DefaultColliderCenter;
      }
    }
  }
  private void StartMoving()
  {
    IsMoving = true;
    animator.SetBool("IsMoving", true);
  }
  private void StopMoving()
  {
    IsMoving = false;
    animator.SetBool("IsMoving", false);
  }
  private void ChangeLensRight()
  {
    if (Lens < 1)
    {
      Lens++;
      TargetLenOffset = Lens * LensOffset;
    }
  }
  private void ChangeLensLeft()
  {
    if (Lens > -1)
    {
      Lens--;
      TargetLenOffset = Lens * LensOffset;
    }
  }
}