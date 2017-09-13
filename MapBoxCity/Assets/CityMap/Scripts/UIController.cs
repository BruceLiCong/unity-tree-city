using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private Camera _camera;

    private void Start()
    {
        // Hide the cursor and lock to center of the screen
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnGUI()
    {
        GUIStyle style = new GUIStyle();
        style.fontSize = 15;
        style.fontStyle = FontStyle.Bold;
        style.normal.textColor = Color.white;

        float posX = _camera.pixelWidth / 2 - style.fontSize / 4;
        float posY = _camera.pixelHeight / 2 - style.fontSize / 2;
        GUI.Label(new Rect(posX, posY, style.fontSize, style.fontSize), "+", style);
    }
}
