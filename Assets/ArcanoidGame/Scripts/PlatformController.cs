using UnityEngine;

public class PlatformController : MonoBehaviour
{
    public float Speed = 10f;
    public Transform LeftWall;
    public Transform RightWall;
    public bool IsMouseControl = true;

    private float _platformWidth;
    private float _leftBoundary;
    private float _rightBoundary;

    private void Start()
    {
        _platformWidth = GetComponent<SpriteRenderer>().bounds.extents.x;
        _leftBoundary = LeftWall.position.x + _platformWidth;
        _rightBoundary = RightWall.position.x - _platformWidth;
    }

    private void FixedUpdate()
    {
        if (IsMouseControl)
        {
            MouseMovement();
        }
        else
        {
            KeyboardMovement();
        }
    }

    public void MouseMovement()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float clampedX = Mathf.Clamp(mousePosition.x, _leftBoundary, _rightBoundary);
        transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
    }

    public void KeyboardMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector3 movement = Vector3.right * horizontalInput * Speed * Time.deltaTime;
        transform.Translate(movement);

        float clampedX = Mathf.Clamp(transform.position.x, _leftBoundary, _rightBoundary);
        transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
    }
}
