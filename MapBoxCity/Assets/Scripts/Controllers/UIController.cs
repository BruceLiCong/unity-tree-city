using UnityEngine;

public class UIController : MonoBehaviour
{
    private const string RETICLE = "+";

    [SerializeField]
    private Camera _camera;

    private void OnGUI()
    {
        if (_camera != null)
        {
            GUIStyle style = new GUIStyle();
            style.fontSize = 15;
            style.fontStyle = FontStyle.Bold;
            style.normal.textColor = Color.white;

            float posX = _camera.pixelWidth / 2 - style.fontSize / 4;
            float posY = _camera.pixelHeight / 2 - style.fontSize / 2;
            GUI.Label(new Rect(posX, posY, style.fontSize, style.fontSize), RETICLE, style);
        }
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
