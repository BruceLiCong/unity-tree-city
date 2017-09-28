namespace TreeCity
{
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
        private float _sensitivityHor;
        private float _sensitivityVert;
        private float _rotX = 0;

        public Direction direction;

        private void Awake()
        {
            Messenger<float>.AddListener(GameEvent.MOUSE_SENSITIVITY_CHANGED, OnMouseSensitivity);
        }

        private void Start()
        {
            _rotation = new Vector3();
            _sensitivityHor = Managers.Controls.MouseSensitivity;
            _sensitivityVert = Managers.Controls.MouseSensitivity;

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
                transform.Rotate(Vector3.up, Input.GetAxis("Mouse X") * _sensitivityHor);
            }
            else if (direction == Direction.Vertical)
            {
                _rotX -= Input.GetAxis("Mouse Y") * _sensitivityHor;
                _rotX = Mathf.Clamp(_rotX, MIN_VERTICAL_ANGLE, MAX_VERTICAL_ANGLE);

                _rotation.x = _rotX;

                transform.localEulerAngles = _rotation;
            }
            else
            {
                _rotX -= Input.GetAxis("Mouse Y") * _sensitivityVert;
                _rotX = Mathf.Clamp(_rotX, MIN_VERTICAL_ANGLE, MAX_VERTICAL_ANGLE);

                float rotY = Input.GetAxis("Mouse X") * _sensitivityHor;

                _rotation.x = _rotX;
                _rotation.y = transform.localEulerAngles.y + rotY;

                transform.localEulerAngles = _rotation;
            }
        }

        private void OnDestroy()
        {
            Messenger<float>.RemoveListener(GameEvent.MOUSE_SENSITIVITY_CHANGED, OnMouseSensitivity);
        }

        private void OnMouseSensitivity(float sensitivity)
        {
            _sensitivityHor = sensitivity;
            _sensitivityVert = sensitivity;
        }
    }
}
