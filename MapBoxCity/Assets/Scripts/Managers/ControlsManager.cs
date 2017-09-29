using UnityEngine;

public class ControlsManager : MonoBehaviour
{
    [SerializeField]
    private float _moveSpeed = 4;

    [SerializeField]
    private float _mouseSensitivity = 2;

    public float MoveSpeed { get; private set; }
    public float MouseSensitivity { get; private set; }
    public bool SelectionQueryEnabled { get; private set; }

    public void Startup()
    {
        MoveSpeed = _moveSpeed;
        MouseSensitivity = _mouseSensitivity;
        SelectionQueryEnabled = true;
    }

    public void UpdateMoveSpeed(float value)
    {
        Messenger<float>.Broadcast(GameEvent.MOVE_SPEED_CHANGED, value);
    }

    public void UpdateSensitivity(float value)
    {
        Messenger<float>.Broadcast(GameEvent.MOUSE_SENSITIVITY_CHANGED, value);
    }

    public void UpdateSelectionQueryEnabled(bool isEnabled)
    {
        SelectionQueryEnabled = isEnabled;
    }

    /// <summary>
    /// Hide the cursor and lock to center of the screen
    /// </summary>
    public void LockCursor()
    {
        if (Screen.fullScreen)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    public void ReleaseCursor()
    {
        if (Screen.fullScreen)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
