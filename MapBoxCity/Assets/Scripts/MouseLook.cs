using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public enum Direction
    {
        Horizontal,
        Vertical,
        Both
    }

    private const float MAX_VERTICAL_ANGLE = 45.0f;
    private const float MIN_VERTICAL_ANGLE = -45.0f;

    private Vector3 _rotation;
    private float _rotX = 0;

    public Direction direction;
    public float sensitivityHor = 6.0f;
    public float sensitivityVert = 6.0f;

    private void Start()
    {
        _rotation = new Vector3();

        /// Disallow physics rotation on player
        Rigidbody body = GetComponent<Rigidbody>();
        if (body != null)
        {
            body.freezeRotation = true;
        }
    }

    private void Update()
    {
        if (direction == Direction.Horizontal)
        {
            transform.Rotate(Vector3.up, Input.GetAxis("Mouse X") * sensitivityHor);
        }
        else if (direction == Direction.Vertical)
        {
            _rotX -= Input.GetAxis("Mouse Y") * sensitivityHor;
            _rotX = Mathf.Clamp(_rotX, MIN_VERTICAL_ANGLE, MAX_VERTICAL_ANGLE);

            _rotation.x = _rotX;

            transform.localEulerAngles = _rotation;
        }
        else
        {
            _rotX -= Input.GetAxis("Mouse Y") * sensitivityVert;
            _rotX = Mathf.Clamp(_rotX, MIN_VERTICAL_ANGLE, MAX_VERTICAL_ANGLE);

            float rotY = Input.GetAxis("Mouse X") * sensitivityHor;

            _rotation.x = _rotX;
            _rotation.y = transform.localEulerAngles.y + rotY;

            transform.localEulerAngles = _rotation;
        }
    }
}
