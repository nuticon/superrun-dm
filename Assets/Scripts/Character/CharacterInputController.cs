using UnityEngine;

public class CharacterInputController : MonoBehaviour
{
  public CharacterMovement characterMovement;
  private bool IsSwiping;
  private Vector2 StartTouch;
  private void Update()
  {
    if (Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.UpArrow)) characterMovement.Jump();
    if (Input.GetKeyDown(KeyCode.DownArrow)) characterMovement.Slide();
    if (Input.GetKeyDown(KeyCode.RightArrow)) characterMovement.ChangeLanesRight();
    if (Input.GetKeyDown(KeyCode.LeftArrow)) characterMovement.ChangeLanesLeft();
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
              characterMovement.Slide();
            }
            else
            {
              characterMovement.Jump();
            }
          }
          else
          {
            if (diff.x < 0)
            {
              characterMovement.ChangeLanesLeft();
            }
            else
            {
              characterMovement.ChangeLanesRight();
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
}