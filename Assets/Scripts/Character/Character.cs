using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
  public float min_speed;
  public float speed;
  public float side_way_speed = 10;
  public float max_speed;
  protected float speed_multiplier = 10;
  public float jump_strenght;
  protected bool jumping = false;
  protected bool sliding = false;
  protected float current_height = 0;
  private Animator animator;
  private Rigidbody rb;
  /**
  * -1 left
  * 0 center
  * 1 center
  */
  [Range(-1, 1)]
  private int lens = 0;
  public int lensOffset = 15;
  private bool isMoving = false;
  void Start()
  {
    animator = GetComponent<Animator>();
    rb = GetComponent<Rigidbody>();
    speed = min_speed;
    transform.position = new Vector3(0, 0, 0);
  }
  private void Update()
  {
    if (Input.GetKeyDown(KeyCode.LeftShift)) startMoving();
    if (Input.GetKeyDown(KeyCode.LeftControl)) stopMoving();
    if (isMoving)
    {
      if (Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.UpArrow)) jump();
      if (Input.GetKeyDown(KeyCode.DownArrow)) slide();
      if (Input.GetKeyDown(KeyCode.RightArrow)) changeLensRight();
      if (Input.GetKeyDown(KeyCode.LeftArrow)) changeLensLeft();
      run();
    }
  }
  private bool isNotMaxSpeed()
  {
    if (speed != max_speed) return true;
    return false;
  }
  public void run()
  {
    // transform.position = new Vector3((lens * (lensOffset/2)) - lensOffset, transform.position.y, transform.position.z + (Time.deltaTime * speed * speed_multiplier) * 2);
    transform.position += Vector3.forward * Time.deltaTime * speed * speed_multiplier;
        Vector3 interpolPostion = new Vector3((lens * lensOffset), transform.position.y, transform.position.z);
    transform.position = Vector3.Lerp(Vector3.left, interpolPostion, 1f);
    if (isNotMaxSpeed()) speed += 0.001f;
    if (animator.speed < 2) animator.speed = speed;
  }
  public void jump()
  {
    print("Jump");
    animator.SetTrigger("isJumpping");
    jumping = true;
  }
  public void stopJump()
  {
    jumping = false;
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
  public void startMoving()
  {
    isMoving = true;
    animator.SetBool("isMoving",true);
  }
  public void stopMoving()
  {
    isMoving = false;
     animator.SetBool("isMoving",false);
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