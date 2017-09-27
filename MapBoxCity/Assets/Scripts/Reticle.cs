namespace TreeCity
{
    using UnityEngine;

    [RequireComponent(typeof(Camera))]
    public class Reticle : MonoBehaviour
    {
        private Camera _camera;
        private FeatureSelectionDetector _lastSelected;

        private void Start()
        {
            _camera = GetComponent<Camera>();
            Physics.queriesHitTriggers = true;
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

            if (Physics.Raycast(ray, out hit))
            {
                GameObject go = hit.transform.gameObject;
                if (go.layer == LayerMask.NameToLayer("Tree"))
                {
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
}
