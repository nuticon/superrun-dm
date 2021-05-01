using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
  [Header("Character")]
  public float MinSpeed;
  public float MaxSpeed;
  public float JumpStrength;
  public float SlideLength;
  public float SideWaySpeed = 10;

  //Helper Value
  public static float Speed;
  public static float SpeedMultiplier = 25;
  private float CalculatedSlideLength;
  private float SlideAnimatorPauseAt;
  private bool Jumping = false;
  private int JumpingFrame = 0;
  private float JumpFrame;
  private float JumpFrameCenter;
  private bool Sliding = false;
  private int SlidingFrame = 0;
  private float TempAnimatorSpeed;
  /**
    * -1 left
    * 0 center
    * 1 center
    */
  [Range(-1, 1)]
  private int Lens = 0;
  public int LensOffset = 15;
  public static bool IsMoving = false;
  private float CurrentLensOffset;

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
    CalculatedSlideLength = SlideLength * 50;
    SlideAnimatorPauseAt = CalculatedSlideLength / SlideLength;
    transform.position = new Vector3(0, 0, 0);
    Position = transform.position;
    DefaultColliderCenter = Collider.center;
    DefaultColliderSize = Collider.size;

    //Temporaly
    StartMoving();
  }
  void Update()
  {
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
    if (IsMoving && Game.Over)
    {
      Over();
    }

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
    Vector3 interpolPostion = new Vector3((Lens * LensOffset), transform.position.y, transform.position.z);
    transform.position = Vector3.Lerp(Vector3.left, interpolPostion, 1f);
    Position = transform.position;
    if (IsNotMaxSpeed()) Speed += 0.001f;
    if (animator.speed < 2 && !Jumping && !Sliding)
    {
      animator.speed = Speed;
      TempAnimatorSpeed = animator.speed;
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
      if (JumpingFrame < JumpFrame)
      {
        JumpingFrame++;
        if (JumpingFrame < JumpFrameCenter) transform.position += Vector3.up * Time.deltaTime * JumpFrameCenter;
        if (JumpingFrame > JumpFrameCenter) transform.position += Vector3.down * Time.deltaTime * JumpFrameCenter;
        if (JumpingFrame == JumpFrameCenter) animator.speed = 0;
      }
      else
      {
        Jumping = false;
        JumpingFrame = 0;
        animator.speed = TempAnimatorSpeed;
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
  public void CheckSlidingFrame()
  {
    if (Sliding)
    {
      if (SlidingFrame < CalculatedSlideLength)
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
  public void StartMoving()
  {
    IsMoving = true;
    animator.SetBool("IsMoving", true);
  }
  public void StopMoving()
  {
    IsMoving = false;
    animator.SetBool("IsMoving", false);
  }
  public void ChangeLensRight()
  {
    Lens++;
  }
  public void ChangeLensLeft()
  {
    Lens--;
  }
}