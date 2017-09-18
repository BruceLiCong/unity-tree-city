using Mapbox.Examples;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class Reticle : MonoBehaviour
{
    private const string RETICLE = "+";

    private GUIStyle _style;
    private Camera _camera;
    private Vector3 _screenCenter;

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
        _screenCenter = new Vector3(_camera.pixelWidth / 2, _camera.pixelHeight / 2, 0);

        /// Hide the cursor and lock to center of the screen
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void FixedUpdate()
    {
        bool didMouseClick = Input.GetMouseButtonDown(0);

        Ray ray = _camera.ScreenPointToRay(_screenCenter);
        RaycastHit hit;
        int layerMask = 1 << LayerMask.NameToLayer("Tree");

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask, QueryTriggerInteraction.Collide))
        {
            GameObject go = hit.transform.gameObject;
            FeatureSelectionDetector selectDetector = go.GetComponent<FeatureSelectionDetector>();
            HighlightFeature highlight = go.GetComponent<HighlightFeature>();

            if (selectDetector != null && didMouseClick)
            {
                selectDetector.Select();
            }

            if (highlight != null)
            {
                highlight.SetFocus(true);
            }
        }
    }

    private void OnGUI()
    {
        float posX = _camera.pixelWidth / 2 - _style.fontSize / 4;
        float posY = _camera.pixelHeight / 2 - _style.fontSize / 2;
        GUI.Label(new Rect(posX, posY, _style.fontSize, _style.fontSize), RETICLE, _style);
    }
}
