using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
  private bool Jumping = false;
  private float CalculatedJumpStrength;
  private float JumpingFrame = 0;
  private bool Sliding = false;
  private float SlidingFrame = 0;
  private float CalculatedSideWaySpeed;
  private float TempAnimatorSpeed;
  public static float Speed;
  public static float SpeedMultiplier = 25;
  private float CalculatedSlideLength;
  private float CalculatedJumpSpeed;
  /**
    * -1 left
    * 0 center
    * 1 center
    */
  [Range(-1, 1)]
  internal int Lanes = 0;
  public static bool IsMoving = false;
  internal float TargetLenOffset;
  public Character character;
  private float SpeedTimer = 0;
  internal float TempDistance = 0;
  public static bool IsGrounded;
  public Power power;
  private void Start()
  {
    Speed = character.MinSpeed;
    CalculatedJumpStrength = character.JumpStrength * 20;
    CalculatedSlideLength = character.SlideLength * 46;
    CalculatedSideWaySpeed = character.SideWaySpeed * 25;
    CalculatedJumpSpeed = character.JumpSpeed * 10;
    transform.position = new Vector3(0, 0, 0);
  }
  private void Update()
  {
    if (IsMoving && !Game.Over) Run();
    if (Sliding)
    {
      CheckSlidingFrame();
      return;
    }
    if (Jumping)
    {
      CheckJumpingFrame();
      return;
    }
  }
  private void SpeedIncrease()
  {
    if (Speed <= character.MaxSpeed)
    {
      SpeedTimer += Time.deltaTime;
      if (SpeedTimer >= character.SpeedIncreaseDelay)
      {
        Speed += 0.05f;
        Debug.Log("Current Speed " + Speed.ToString());
        SpeedTimer = 0;
      }
    }
  }
  public void Run()
  {
    transform.position += Vector3.forward * Time.deltaTime * Speed * SpeedMultiplier;
    if (transform.position.x != TargetLenOffset)
    {
      Vector3 interpolPostion = new Vector3(TargetLenOffset, transform.position.y, transform.position.z);
      transform.position = Vector3.Lerp(transform.position, interpolPostion, Time.deltaTime * CalculatedSideWaySpeed);
    }
    if (!Jumping && !IsGrounded)
    {
      Vector3 interpolPostionDown = new Vector3(transform.position.x, 0, transform.position.z);
      transform.position = Vector3.Lerp(transform.position, interpolPostionDown, Time.deltaTime * CalculatedJumpSpeed);
    }
    Character.Position = transform.position;
    SpeedIncrease();
    if (character.animator.speed < 1.5f && !Jumping && !Sliding)
    {
      character.animator.speed += Time.deltaTime;
    }
    if (transform.position.z - TempDistance >= 10)
    {
      TempDistance = transform.position.z;
      Game.Point++;
      if (power.DoubleActivating()) Game.Point++;
    }
  }
  public void Jump()
  {
    if (!Jumping && IsGrounded)
    {
      Debug.Log("Player Jumped");
      character.animator.SetTrigger("IsJumpping");
      Jumping = true;
      if (Sliding) StopSlide();
    }
  }
  public void StopJump()
  {
    if (!Sliding && !Game.Over) character.animator.SetTrigger("IsRunning");
    Jumping = false;
    JumpingFrame = 0;
  }
  public void CheckJumpingFrame()
  {
    if (JumpingFrame < CalculatedJumpStrength)
    {
      Vector3 interpolPostionUp = new Vector3(transform.position.x, CalculatedJumpStrength / 2, transform.position.z);
      transform.position = Vector3.Lerp(transform.position, interpolPostionUp, Time.deltaTime * CalculatedJumpSpeed);
      JumpingFrame += Time.deltaTime * 100;
      return;
    }
    StopJump();
  }
  public void Slide()
  {
    if (!Sliding)
    {
      Debug.Log("Player Slided");
      character.animator.SetTrigger("IsSlide");
      Sliding = true;
      if (Jumping) StopJump();
    }
  }
  public void StopSlide()
  {
    if (!Jumping && !Game.Over) character.animator.SetTrigger("IsRunning");
    character.Collider.size = character.DefaultColliderSize;
    character.Collider.center = character.DefaultColliderCenter;
    Sliding = false;
    SlidingFrame = 0;
  }
  public void CheckSlidingFrame()
  {
    if (SlidingFrame < CalculatedSlideLength)
    {
      Vector3 interpolPostionDown = new Vector3(transform.position.x, 0, transform.position.z);
      transform.position = Vector3.Lerp(transform.position, interpolPostionDown, Time.deltaTime * CalculatedJumpSpeed);
      character.Collider.size = new Vector3(character.DefaultColliderSize.x, 0.02f, character.DefaultColliderSize.z);
      character.Collider.center = new Vector3(character.DefaultColliderCenter.x, 0.015f, character.DefaultColliderCenter.z);
    }
    else
    {
      StopSlide();
      return;
    }
    SlidingFrame += Time.deltaTime * 100;
  }
  public void StartMoving()
  {
    IsMoving = true;
    character.animator.SetBool("IsMoving", true);
    Debug.Log("Player started run");
  }
  public void StopMoving()
  {
    IsMoving = false;
    character.animator.SetBool("IsMoving", false);
  }
  public void ChangeLanesRight()
  {
    if (Lanes < 1)
    {
      Lanes++;
      TargetLenOffset = Lanes * character.LanesOffset;
      Debug.Log("Player change to right lane");
    }
  }
  public void ChangeLanesLeft()
  {
    if (Lanes > -1)
    {
      Lanes--;
      TargetLenOffset = Lanes * character.LanesOffset;
      Debug.Log("Player change to left lane");
    }
  }
}