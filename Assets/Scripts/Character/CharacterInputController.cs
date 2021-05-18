using UnityEngine;

public class CharacterInputController : MonoBehaviour
{
  public CharacterMovement characterMovement;
  private bool IsSwiping;
  private Vector2 StartTouch;
  private void Update()
  {
    if (CharacterMovement.IsMoving)
    {
      if (Input.GetButtonDown("Jump") || Input.GetAxis("Vertical") > 0) characterMovement.Jump();
      if (Input.GetAxis("Vertical") < 0) characterMovement.Slide();
      if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) characterMovement.ChangeLanesRight();
      if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) characterMovement.ChangeLanesLeft();
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
}