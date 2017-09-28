using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class FPSInput : MonoBehaviour
{
    private const float EARTH_GRAVITY = -9.8f;

    private CharacterController _characterController;
    private float speed;

    private void Awake()
    {
        Messenger<float>.AddListener(GameEvent.MOVE_SPEED_CHANGED, OnSpeedChanged);
    }

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
        speed = Managers.Controls.MoveSpeed;
    }

    private void Update()
    {
        float deltaX = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        float deltaZ = Input.GetAxis("Vertical") * speed * Time.deltaTime;

        Vector3 movement = new Vector3(deltaX, 0, deltaZ);
        movement = Vector3.ClampMagnitude(movement, speed);
        movement.y = EARTH_GRAVITY;
        movement = transform.TransformDirection(movement);

        _characterController.Move(movement);
    }

    private void OnDestroy()
    {
        Messenger<float>.RemoveListener(GameEvent.MOVE_SPEED_CHANGED, OnSpeedChanged);
    }

    private void OnSpeedChanged(float speed)
    {
        this.speed = speed;
    }
}
