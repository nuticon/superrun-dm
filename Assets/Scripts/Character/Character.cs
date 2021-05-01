using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
  public float min_speed;
  public static float speed;
  public float side_way_speed = 10;
  public float max_speed;
  public static float speed_multiplier = 25;
  public float jump_strenght;
  public float sliding_length;
  private float calculated_sliding_length;
  private float sliding_animator_pause;
  protected bool jumping = false;
  private int jumping_frame = 0;
  protected bool sliding = false;
  private int sliding_frame = 0;
  protected float current_height = 0;
  private Animator animator;
  private Rigidbody rb;
  private BoxCollider box_collider;
  public static Vector3 position;
  private Vector3 defaultColliderCenter;
  private Vector3 defaultColliderSize;
  private float tempAnimatorSpeed;
  /**
  * -1 left
  * 0 center
  * 1 center
  */
  [Range(-1, 1)]
  private int lens = 0;
  public int lensOffset = 15;
  public static bool isMoving = false;
  private float curent_lens_offset;
  private float jump_frame;
  private float jump_frame_center;
  void Start()
  {
    //Get Component
    animator = GetComponent<Animator>();
    rb = GetComponent<Rigidbody>();
    box_collider = GetComponent<BoxCollider>();
    //Set default value
    speed = min_speed;
    jump_frame = jump_strenght * 50;
    jump_frame_center = jump_frame / 2;
    calculated_sliding_length = sliding_length * 50;
    sliding_animator_pause = calculated_sliding_length / sliding_length;
    transform.position = new Vector3(0, 0, 0);
    position = transform.position;
    defaultColliderCenter = box_collider.center;
    defaultColliderSize = box_collider.size;
    //Temporaly
    startMoving();
  }
  private void Update()
  {

    if (isMoving && !Game.over)
    {
      if (Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.UpArrow)) jump();
      if (Input.GetKeyDown(KeyCode.DownArrow)) slide();
      if (Input.GetKeyDown(KeyCode.RightArrow)) changeLensRight();
      if (Input.GetKeyDown(KeyCode.LeftArrow)) changeLensLeft();
      run();
      if (sliding) checkSlidingFrame();
      if (jumping) checkJumpingFrame();
    }
    if (isMoving && Game.over)
    {
      over();
    }

  }
  private bool isNotMaxSpeed()
  {
    if (speed != max_speed) return true;
    return false;
  }
  private void over()
  {
    stopMoving();
    stopJump();
    stopSlide();
    animator.SetTrigger("isOver");
  }
  public void stopAnimation()
  {
    animator.speed = 0;
  }
  public void run()
  {
    // transform.position = new Vector3((lens * (lensOffset/2)) - lensOffset, transform.position.y, transform.position.z + (Time.deltaTime * speed * speed_multiplier) * 2);
    transform.position += Vector3.forward * Time.deltaTime * speed * speed_multiplier;
    Vector3 interpolPostion = new Vector3((lens * lensOffset), transform.position.y, transform.position.z);
    transform.position = Vector3.Lerp(Vector3.left, interpolPostion, 1f);
    position = transform.position;
    if (isNotMaxSpeed()) speed += 0.001f;
    if (animator.speed < 2 && !jumping && !sliding)
    {
      animator.speed = speed;
      tempAnimatorSpeed = animator.speed;
    }
  }
  public void jump()
  {
    if (!jumping)
    {
      print("Jump");
      animator.SetTrigger("isJumpping");
      jumping = true;
    }
  }
  public void stopJump()
  {
    jumping = false;
  }
  public void checkJumpingFrame()
  {
    if (jumping)
    {
      if (jumping_frame < jump_frame)
      {
        jumping_frame++;
        if (jumping_frame < jump_frame_center) transform.position += Vector3.up * Time.deltaTime * jump_frame_center;
        if (jumping_frame > jump_frame_center) transform.position += Vector3.down * Time.deltaTime * jump_frame_center;
        if (jumping_frame == jump_frame_center) animator.speed = 0;
        // box_collider.size = new Vector3 (defaultColliderSize.x, 0.02f, defaultColliderSize.z );
        // box_collider.center = new Vector3(defaultColliderCenter.x, 0.015f, defaultColliderCenter.z);
      }
      else
      {
        jumping = false;
        jumping_frame = 0;
        animator.speed = tempAnimatorSpeed;
        // box_collider.size = defaultColliderSize;
        // box_collider.center = defaultColliderCenter;
      }
    }
  }
  public void slide()
  {
    print("Slide");
    animator.SetTrigger("isSlide");
    sliding = true;
  }
  public void stopSlide()
  {
    sliding = false;
  }
  public void checkSlidingFrame()
  {
    if (sliding)
    {
      if (sliding_frame < calculated_sliding_length)
      {
        sliding_frame++;
        if (sliding_frame == sliding_animator_pause) animator.speed = 0;
        box_collider.size = new Vector3(defaultColliderSize.x, 0.02f, defaultColliderSize.z);
        box_collider.center = new Vector3(defaultColliderCenter.x, 0.015f, defaultColliderCenter.z);
      }
      else
      {
        sliding = false;
        sliding_frame = 0;
        animator.speed = tempAnimatorSpeed;
        box_collider.size = defaultColliderSize;
        box_collider.center = defaultColliderCenter;
      }
    }
  }
  public void startMoving()
  {
    isMoving = true;
    animator.SetBool("isMoving", true);
  }
  public void stopMoving()
  {
    isMoving = false;
    animator.SetBool("isMoving", false);
  }
  public void changeLensRight()
  {
    lens++;
  }
  public void changeLensLeft()
  {
    lens--;
  }
}