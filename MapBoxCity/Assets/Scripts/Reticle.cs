namespace TreeCity
{
    using UnityEngine;

    [RequireComponent(typeof(Camera))]
    public class Reticle : MonoBehaviour
    {
        private const string RETICLE = "+";

        private GUIStyle _style;
        private Camera _camera;
        private FeatureSelectionDetector _lastSelected;

        private void Awake()
        {
            _style = new GUIStyle();
            _style.fontSize = 16;
            _style.fontStyle = FontStyle.Bold;
            _style.normal.textColor = Color.white;
        }

        private void Start()
        {
            _camera = GetComponent<Camera>();
        }

        private void Update()
        {
            bool didClickMouse = Input.GetMouseButtonDown(0);

            if (didClickMouse && _lastSelected != null)
            {
                _lastSelected.RequestSelect(false);
                _lastSelected = null;
            }

            Ray ray = _camera.ScreenPointToRay(new Vector3(_camera.pixelWidth / 2, _camera.pixelHeight / 2, 0));
            RaycastHit hit;
            int layerMask = 1 << LayerMask.NameToLayer("Tree");

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask, QueryTriggerInteraction.Collide))
            {
                GameObject go = hit.transform.gameObject;
                FeatureSelectionDetector selectDetector = go.GetComponent<FeatureSelectionDetector>();
                HighlightFeature highlight = go.GetComponent<HighlightFeature>();

                if (didClickMouse)
                {
                    if (selectDetector != null)
                    {
                        selectDetector.RequestSelect(true);
                        _lastSelected = selectDetector;
                    }
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

        /// <summary>
        /// Hide the cursor and lock to center of the screen
        /// </summary>
        private void LockCursor()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
