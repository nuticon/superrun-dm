using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
  public float min_speed;
  public float speed;
  public float max_speed;
  protected float speed_multiplier = 10;
  public float max_jump_height;
  protected bool jumping = false;
  private Rigidbody rb;
  void Start()
  {
    rb = GetComponent<Rigidbody>();
    speed = min_speed;
    // Changes the position to x:1, y:1, z:0
    transform.position = new Vector3(0, 0, 1);
    // It is also possible to set the position with a Vector2
    // This automatically sets the Z axis to 0
    transform.position = new Vector2(0, 0);
    // Moving object on a single axis
    Vector3 newPosition = transform.position; // We store the current position
    newPosition.z = 0; // We set a axis, in this case the y axis
    transform.position = newPosition; // We pass it back
  }
  private void Update()
  {
    if (Input.GetButtonDown("Jump"))
    {
      jump();
      return;
    }
    if (!jumping)
    {
      transform.position += new Vector3(0, 0, 1 * (Time.deltaTime * speed * speed_multiplier));
    }
    // We add +1 to the x axis every frame.
    // Time.deltaTime is the time it took to complete the last frame
    // The result of this is that the object moves one unit on the x axis every second
    if (isNotMaxSpeed()) speed += 0.001f;
  }
  private bool isNotMaxSpeed()
  {
    if (speed != max_speed) return true;
    return false;
  }

  public void jump()
  {
    print("Jump");
    rb.AddForce(Vector3.up * max_jump_height);
    jumping = true;
  }
}