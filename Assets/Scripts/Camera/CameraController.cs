using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {
    if (Character.isMoving) run();
  }
  public void run()
  {
    // transform.position = new Vector3((lens * (lensOffset/2)) - lensOffset, transform.position.y, transform.position.z + (Time.deltaTime * speed * speed_multiplier) * 2);
    transform.position = new Vector3(transform.position.x ,transform.position.y , Character.position.z - 40);
  }
}
