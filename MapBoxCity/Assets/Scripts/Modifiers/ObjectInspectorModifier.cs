namespace TreeCity
{
    using Mapbox.Unity.MeshGeneration.Components;
    using Mapbox.Unity.MeshGeneration.Data;
    using Mapbox.Unity.MeshGeneration.Modifiers;
    using UnityEngine;
    using UnityEngine.UI;

    [CreateAssetMenu(menuName = "TreeCity/Modifiers/Object Inspector Modifier")]
    public class ObjectInspectorModifier : GameObjectModifier
    {
        private const string CANVAS_NAME = "Canvas";
        private const string SELECTOR_PATH = "selector";

	    private FeatureUiMarker _marker;

	    public override void Run(FeatureBehaviour fb, UnityTile tile)
	    {
		    if(_marker == null)
		    {
			    var canvases = FindObjectsOfType<Canvas>();
                Canvas canv = null;

                foreach (Canvas _canv in canvases)
                {
                    if (_canv.name == CANVAS_NAME)
                    {
                        canv = _canv;
                        break;
                    }
                }

			    if (canv == null)
			    {
				    var go = new GameObject(CANVAS_NAME, typeof(Canvas), typeof(CanvasScaler), typeof(GraphicRaycaster));
				    canv = go.GetComponent<Canvas>();
				    canv.renderMode = RenderMode.ScreenSpaceOverlay;
			    }

			    var sel = Instantiate(Resources.Load<GameObject>(SELECTOR_PATH));
			    sel.transform.SetParent(canv.transform);
			    _marker = sel.GetComponent<FeatureUiMarker>();
		    }

		    var det = fb.gameObject.AddComponent<FeatureSelectionDetector>();
		    det.Initialize(_marker, fb);
	    }
    }
}