using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
  [Header("Character Settings")]

  public float JumpStrength = 1.2f;
  public float SlideLength = 1.0f;
  public float JumpSpeed = 1;
  public float MinSpeed = 1.0f;
  public float MaxSpeed = 10.0f;
  public float LanesOffset = 7.0f;
  public float SideWaySpeed = 1.0f;

  //Helper Value
  public static float Speed;
  public static float SpeedMultiplier = 25;
  private float CalculatedSlideLength;
  private float CalculatedJumpSpeed;
  private float SlideAnimatorPauseAt;
  private bool Jumping = false;
  private float CalculatedJumpStrength;
  private float JumpingFrame = 0;
  private bool Sliding = false;
  private float SlidingFrame = 0;
  private float CalculatedSideWaySpeed;
  private float TempAnimatorSpeed;
  private Vector2 StartTouch;
  /**
    * -1 left
    * 0 center
    * 1 center
    */
  [Range(-1, 1)]
  private int Lanes = 0;
  public static bool IsMoving = false;
  private float TargetLenOffset;
  private bool IsSwiping;
  public static bool RequireRestart = false;
  //Instance
  private Animator animator;
  private BoxCollider Collider;
  public static Vector3 Position;
  private Vector3 DefaultColliderCenter;
  private Vector3 DefaultColliderSize;
  void Start()
  {
    animator = GetComponent<Animator>();
    Collider = GetComponent<BoxCollider>();
    Speed = MinSpeed;
    CalculatedJumpStrength = JumpStrength * 20;
    CalculatedSlideLength = SlideLength * 46;
    CalculatedSideWaySpeed = SideWaySpeed * 25;
    CalculatedJumpSpeed = JumpSpeed * 10;
    transform.position = new Vector3(0, 0, 0);
    Position = transform.position;
    DefaultColliderCenter = Collider.center;
    DefaultColliderSize = Collider.size;
    TempAnimatorSpeed = animator.speed;
  }
  void Update()
  {
    if (!IsMoving && RequireRestart) ResetState();
    if (IsMoving && !Game.Over)
    {
      if (Sliding) CheckSlidingFrame();
      if (Jumping) CheckJumpingFrame();
      Run();
      if (Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.UpArrow)) Jump();
      if (Input.GetKeyDown(KeyCode.DownArrow)) Slide();
      if (Input.GetKeyDown(KeyCode.RightArrow)) ChangeLanesRight();
      if (Input.GetKeyDown(KeyCode.LeftArrow)) ChangeLanesLeft();
      if (Input.touchCount == 1)
      {
        if (IsSwiping)
        {
          Vector2 diff = Input.GetTouch(0).position - StartTouch;

          diff = new Vector2(diff.x / Screen.width, diff.y / Screen.width);

          if (diff.magnitude > 0.01f)
          {
            if (Mathf.Abs(diff.y) > Mathf.Abs(diff.x))
            {
              if (diff.y < 0)
              {
                Slide();
              }
              else
              {
                Jump();
              }
            }
            else
            {
              if (diff.x < 0)
              {
                ChangeLanesLeft();
              }
              else
              {
                ChangeLanesRight();
              }
            }

            IsSwiping = false;
          }
        }
        if (Input.GetTouch(0).phase == TouchPhase.Began)
        {
          StartTouch = Input.GetTouch(0).position;
          IsSwiping = true;
        }
        else if (Input.GetTouch(0).phase == TouchPhase.Ended)
        {
          IsSwiping = false;
        }
      }

    }
    if (IsMoving && Game.Over) Over();
    if (!IsMoving && Game.GameStarted && !RequireRestart && Game.CountDownEnded) StartMoving();
  }
  private void ResetState()
  {
    animator.SetTrigger("IsIdle");
    transform.position = new Vector3(0, 0, 0);
    Lanes = 0;
    TargetLenOffset = 0;
    Position = transform.position;
    Speed = MinSpeed;
    animator.speed = 1;
    TempAnimatorSpeed = 1;
    RequireRestart = false;
  }
  private bool IsNotMaxSpeed()
  {
    if (Speed <= MaxSpeed) return true;
    return false;
  }
  private void Over()
  {
    StopMoving();
    StopJump();
    StopSlide();
    animator.speed = 1.5f;
    animator.SetTrigger("IsOver");
  }
  private void Run()
  {
    transform.position += Vector3.forward * Time.deltaTime * Speed * SpeedMultiplier;
    if (transform.position.x != TargetLenOffset)
    {
      Vector3 interpolPostion = new Vector3(TargetLenOffset, transform.position.y, transform.position.z);
      transform.position = Vector3.Lerp(transform.position, interpolPostion, Time.deltaTime * CalculatedSideWaySpeed);
    }
    if (!Jumping)
    {
      Vector3 interpolPostionDown = new Vector3(transform.position.x, 0, transform.position.z);
      transform.position = Vector3.Lerp(transform.position, interpolPostionDown, Time.deltaTime * CalculatedJumpSpeed);
    }
    Position = transform.position;
    if (IsNotMaxSpeed()) Speed += 0.001f;
    if (animator.speed < 1.5f && !Jumping && !Sliding)
    {
      animator.speed += Time.deltaTime;
      TempAnimatorSpeed = animator.speed;
    }
  }
  private void Jump()
  {
    if (!Jumping)
    {
      animator.SetTrigger("IsJumpping");
      Jumping = true;
      if (Sliding) StopSlide();
    }
  }
  private void StopJump()
  {
    if (!Sliding && !Game.Over) animator.SetTrigger("IsRunning");
    Jumping = false;
    JumpingFrame = 0;
  }
  private void CheckJumpingFrame()
  {
    if (JumpingFrame < CalculatedJumpStrength)
    {
      Vector3 interpolPostionUp = new Vector3(transform.position.x, CalculatedJumpStrength / 2, transform.position.z);
      transform.position = Vector3.Lerp(transform.position, interpolPostionUp, Time.deltaTime * CalculatedJumpSpeed);
    }
    if (JumpingFrame > CalculatedJumpStrength && JumpingFrame < CalculatedJumpStrength * 2)
    {
      Vector3 interpolPostionDown = new Vector3(transform.position.x, 0, transform.position.z);
      transform.position = Vector3.Lerp(transform.position, interpolPostionDown, Time.deltaTime * CalculatedJumpSpeed);
    }
    if (JumpingFrame >= CalculatedJumpStrength * 2)
    {
      StopJump();
      return;
    }
    JumpingFrame += Time.deltaTime * 100;
  }
  private void Slide()
  {
    animator.SetTrigger("IsSlide");
    Sliding = true;
    if (Jumping) StopJump();
  }
  private void StopSlide()
  {
    if (!Jumping  && !Game.Over) animator.SetTrigger("IsRunning");
    Collider.size = DefaultColliderSize;
    Collider.center = DefaultColliderCenter;
    Sliding = false;
    SlidingFrame = 0;
  }
  private void CheckSlidingFrame()
  {
    if (SlidingFrame < CalculatedSlideLength)
    {
      Vector3 interpolPostionDown = new Vector3(transform.position.x, 0, transform.position.z);
      transform.position = Vector3.Lerp(transform.position, interpolPostionDown, Time.deltaTime * CalculatedJumpSpeed);
      Collider.size = new Vector3(DefaultColliderSize.x, 0.02f, DefaultColliderSize.z);
      Collider.center = new Vector3(DefaultColliderCenter.x, 0.015f, DefaultColliderCenter.z);
    }
    else
    {
      StopSlide();
      return;
    }
    SlidingFrame += Time.deltaTime * 100;
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
  private void ChangeLanesRight()
  {
    if (Lanes < 1)
    {
      Lanes++;
      TargetLenOffset = Lanes * LanesOffset;
    }
  }
  private void ChangeLanesLeft()
  {
    if (Lanes > -1)
    {
      Lanes--;
      TargetLenOffset = Lanes * LanesOffset;
    }
  }
}