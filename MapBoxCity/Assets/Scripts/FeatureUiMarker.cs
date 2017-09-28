namespace TreeCity
{
    using Mapbox.Unity.MeshGeneration.Components;
    using UnityEngine;
    using UnityEngine.UI;
    using System.Linq;
    using System.Collections;

    public class FeatureUiMarker : MonoBehaviour
    {
        [SerializeField]
	    private Transform _wrapperMarker;

	    internal Transform _infoPanel;
        internal Text _info;

	    private Vector3[] _targetVerts;
	    private FeatureBehaviour _selectedFeature;

        void Update()
	    {
            if (_selectedFeature != null)
            {
                Vector3 direction = _selectedFeature.transform.position - Camera.main.transform.position;
                if (Vector3.Dot(Camera.main.transform.forward, direction) > 0.0f)
                {
                    Snap();
                }
            }
	    }

	    internal void Clear()
	    {
            gameObject.SetActive(false);
            _infoPanel.gameObject.SetActive(false);
        }

	    internal void Show(FeatureBehaviour selectedFeature)
	    {
		    if (selectedFeature == null)
		    {
			    Clear();
			    return;
		    }

		    _selectedFeature = selectedFeature;
		    transform.position = new Vector3(0, 0, 0);
		    var mesh = selectedFeature.GetComponent<MeshFilter>();

            gameObject.SetActive(true);
            _infoPanel.gameObject.SetActive(true);

            if (mesh != null)
		    {
			    _targetVerts = mesh.mesh.vertices;
                StartCoroutine(SnapDelayed());
		    }
        }

        private IEnumerator SnapDelayed()
        {
            yield return null;
            Snap();
        }

        private void Snap()
	    {
            if (_targetVerts == null || _selectedFeature == null)
			    return;

		    var left = float.MaxValue;
		    var right = float.MinValue;
		    var top = float.MinValue;
		    var bottom = float.MaxValue;
		    foreach (var vert in _targetVerts)
		    {
			    var pos = Camera.main.WorldToScreenPoint(_selectedFeature.transform.position + (_selectedFeature.transform.lossyScale.x * vert));
			    if (pos.x < left)
				    left = pos.x;
			    else if (pos.x > right) 
				    right = pos.x;
			    if (pos.y > top)
				    top = pos.y;
			    else if (pos.y < bottom)
				    bottom = pos.y;
		    }

            _wrapperMarker.position = new Vector2(left - 10, top + 10);
		    (_wrapperMarker as RectTransform).sizeDelta = new Vector2(right - left + 20, top - bottom + 20);

            string[] infoText = _selectedFeature.Data.Properties
                                    .Where(x => !string.IsNullOrEmpty(x.Value.ToString()))
                                    .Select(x => x.Key + " - " + x.Value.ToString())
                                    .ToArray();

            _infoPanel.position = new Vector2(right + 10, top + 10);
            _info.text = string.Join("\r\n", infoText);
        }
    }
}
