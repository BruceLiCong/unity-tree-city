using UnityEngine;

[RequireComponent(typeof(Camera))]
public class Reticle : MonoBehaviour
{
    private const string RETICLE = "+";

    private GUIStyle _style;
    private Camera _camera;

    private void Awake()
    {
        _style = new GUIStyle();
        _style.fontSize = 15;
        _style.fontStyle = FontStyle.Bold;
        _style.normal.textColor = Color.white;
    }

    private void Start()
    {
        _camera = GetComponent<Camera>();
    }

    private void OnGUI()
    {
        float posX = _camera.pixelWidth / 2 - _style.fontSize / 4;
        float posY = _camera.pixelHeight / 2 - _style.fontSize / 2;
        GUI.Label(new Rect(posX, posY, _style.fontSize, _style.fontSize), RETICLE, _style);
    }

    /// <summary>
    /// Hide the cursor and lock to center of the screen
    /// </summary>
    public void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
