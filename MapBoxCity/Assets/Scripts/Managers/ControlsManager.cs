using UnityEngine;

public class ControlsManager : MonoBehaviour
{
    [SerializeField]
    private float _moveSpeed = 4;

    [SerializeField]
    private float _mouseSensitivity = 2;

    public float MoveSpeed { get; private set; }
    public float MouseSensitivity { get; private set; }

    public void Startup()
    {
        MoveSpeed = _moveSpeed;
        MouseSensitivity = _mouseSensitivity;
    }

    public void UpdateMoveSpeed(float value)
    {
        Messenger<float>.Broadcast(GameEvent.MOVE_SPEED_CHANGED, value);
    }

    public void UpdateSensitivity(float value)
    {
        Messenger<float>.Broadcast(GameEvent.MOUSE_SENSITIVITY_CHANGED, value);

    }
}
