namespace TreeCity
{
    using Mapbox.Unity.MeshGeneration.Components;
    using UnityEngine;

    public class FeatureSelectionDetector : MonoBehaviour
    {
	    private FeatureUiMarker _marker;
	    private FeatureBehaviour _feature;

	    internal void Initialize(FeatureUiMarker marker, FeatureBehaviour fb)
	    {
		    _marker = marker;
		    _feature = fb;
	    }

        public void Select()
        {
            _marker.Show(_feature);
        }
    }
}