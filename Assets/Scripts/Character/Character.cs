using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
  [Header("Character")]

  public float JumpStrength = 1.2f;
  public float SlideLength = 1.0f;
  public float JumpSpeed = 1;

  [Header("Game")]
  public float MinSpeed = 1.0f;
  public float MaxSpeed = 10.0f;
  public float PointDelay = 0.5f;
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
  private int JumpingFrame = 0;
  private bool Sliding = false;
  private int SlidingFrame = 0;
  private float CalculatedSideWaySpeed;
  private float TempAnimatorSpeed;
  private Vector2 StartTouch;
  private float Timer;
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
    //Get Component
    animator = GetComponent<Animator>();
    Collider = GetComponent<BoxCollider>();

    //Set default value
    Speed = MinSpeed;
    CalculatedJumpStrength = JumpStrength * 20; //Base on animation used frame
    CalculatedSlideLength = SlideLength * 46;
    CalculatedSideWaySpeed = SideWaySpeed * 25;
    CalculatedJumpSpeed = JumpSpeed * 10;
    SlideAnimatorPauseAt = CalculatedSlideLength / SlideLength;
    transform.position = new Vector3(0, 0, 0);
    Position = transform.position;
    DefaultColliderCenter = Collider.center;
    DefaultColliderSize = Collider.size;
    TempAnimatorSpeed = animator.speed;
  }
  void Update()
  {
    if (!IsMoving && RequireRestart) ResetState();
    //On Moving
    if (IsMoving && !Game.Over)
    {

      if (Sliding) CheckSlidingFrame();
      if (Jumping) CheckJumpingFrame();
      Run();
      if (Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.UpArrow)) Jump();
      if (Input.GetKeyDown(KeyCode.DownArrow)) Slide();
      if (Input.GetKeyDown(KeyCode.RightArrow)) ChangeLanesRight();
      if (Input.GetKeyDown(KeyCode.LeftArrow)) ChangeLanesLeft();
      // Use touch input on mobile
      if (Input.touchCount == 1)
      {
        if (IsSwiping)
        {
          Vector2 diff = Input.GetTouch(0).position - StartTouch;

          // Put difference in Screen ratio, but using only width, so the ratio is the same on both
          // axes (otherwise we would have to swipe more vertically...)
          diff = new Vector2(diff.x / Screen.width, diff.y / Screen.width);

          if (diff.magnitude > 0.01f) //we set the swip distance to trigger movement to 1% of the screen width
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

        // Input check is AFTER the swip test, that way if TouchPhase.Ended happen a single frame after the Began Phase
        // a swipe can still be registered (otherwise, IsSwiping will be set to false and the test wouldn't happen for that began-Ended pair)
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
    //On Game Over
    if (IsMoving && Game.Over) Over();
    if (!IsMoving && Game.GameStarted && !RequireRestart) StartMoving();
  }
  private void ResetState()
  {
    transform.position = new Vector3(0, 0, 0);
    Lanes = 0;
    TargetLenOffset = 0;
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
    animator.speed = TempAnimatorSpeed;
  }
  private void Run()
  {
    transform.position += Vector3.forward * Time.deltaTime * Speed * SpeedMultiplier;
    if (transform.position.x != TargetLenOffset)
    {
      Vector3 interpolPostion = new Vector3(TargetLenOffset, transform.position.y, transform.position.z);
      transform.position = Vector3.Lerp(transform.position, interpolPostion, Time.deltaTime * CalculatedSideWaySpeed);
    }
    Position = transform.position;
    if (IsNotMaxSpeed()) Speed += 0.001f;
    if (animator.speed < 1.5 && !Jumping && !Sliding)
    {
      animator.speed += Time.deltaTime;
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
    JumpingFrame++;
    animator.speed = JumpSpeed;
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
      Jumping = false;
      JumpingFrame = 0;
      animator.speed = TempAnimatorSpeed;
      transform.position = new Vector3(transform.position.x, 0, transform.position.z);
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
    if (SlidingFrame < CalculatedSlideLength)
    {
      SlidingFrame++;
      //if (SlidingFrame >= SlideAnimatorPauseAt) animator.speed = 0;
      Collider.size = new Vector3(DefaultColliderSize.x, 0.02f, DefaultColliderSize.z);
      Collider.center = new Vector3(DefaultColliderCenter.x, 0.015f, DefaultColliderCenter.z);
    }
    else
    {
      //animator.speed = TempAnimatorSpeed;
      Collider.size = DefaultColliderSize;
      Collider.center = DefaultColliderCenter;
      Sliding = false;
      SlidingFrame = 0;
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