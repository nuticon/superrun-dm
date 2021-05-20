using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
  [Header("Character Settings")]
  public Vector3 DefaultModelScale;
  public float JumpSpeed = 1;
  public float JumpStrength = 1.2f;
  public float LanesOffset = 7.0f;
  public float MaxSpeed = 10.0f;
  public float MinSpeed = 1.0f;
  public float SpeedIncreaseDelay;
  public float SideWaySpeed = 1.0f;
  public float SlideLength = 1.0f;
  public int MaxLife;

  //Instance
  internal Animator animator;
  internal BoxCollider Collider;
  public static Vector3 Position;
  public static int Life;
  public static bool Invincible = false;
  private float InvinibleTimer = 0;
  private float BlinkTimer = 0;
  private bool BlinkState = false;
  internal Vector3 DefaultColliderCenter;
  internal Vector3 DefaultColliderSize;
  public CharacterMovement characterMovement;
  public CharacterParticle characterParticle;
  public Sound sound;
  private int LocalLife;
  public Power power;
  void Start()
  {
    animator = GetComponent<Animator>();
    Collider = GetComponent<BoxCollider>();
    Position = transform.position;
    DefaultColliderCenter = Collider.center;
    DefaultColliderSize = Collider.size;
    Life = MaxLife;
    LocalLife = Life;
  }
  void Update()
  {
    if (CharacterMovement.IsMoving && Game.Over)
    {
      Over();
      return;
    }
    if (!CharacterMovement.IsMoving && Game.GameStarted && Game.CountDownEnded)
    {
      characterMovement.StartMoving();
      return;
    }
    if (Invincible) Blink();
    WatchLife();
  }
  public void ResetState()
  {
    animator.SetTrigger("IsIdle");
    transform.position = new Vector3(0, 0, 0);
    characterMovement.Lanes = 0;
    characterMovement.TargetLenOffset = 0;
    Position = transform.position;
    CharacterMovement.Speed = MinSpeed;
    animator.speed = 1;
    Life = MaxLife;
    LocalLife = Life;
    characterMovement.TempDistance = 0;
    Debug.Log("Reseted character");
  }
  public void Over()
  {
    characterMovement.StopMoving();
    characterMovement.StopJump();
    characterMovement.StopSlide();
    animator.speed = 1.5f;
    animator.SetTrigger("IsOver");
  }
  private void Blink()
  {
    if (BlinkState) transform.localScale = Vector3.zero;
    else transform.localScale = DefaultModelScale;
    InvinibleTimer += Time.deltaTime;
    if (InvinibleTimer >= 2f)
    {
      InvinibleTimer = 0;
      BlinkTimer = 0;
      Invincible = false;
      BlinkState = false;
      transform.localScale = DefaultModelScale;
      return;
    }
    BlinkTimer += Time.deltaTime;
    if (BlinkTimer >= 0.05f)
    {
      BlinkTimer = 0;
      BlinkState = !BlinkState;
    }
  }
  private void WatchLife()
  {
    if (LocalLife > Life)
    {
      characterParticle.PlayHitParticle();
      sound.PlayObstracleHitSound();
      LocalLife = Life;
    }
  }
  public void CoinUp()
  {
    characterParticle.PlayCoinParticle();
    sound.PlayCoinCollectSound();
  }
}