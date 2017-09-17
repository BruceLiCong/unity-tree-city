namespace TreeCity
{
    using Mapbox.Unity.MeshGeneration;
    using UnityEngine;

    public class LoadingPanelController : MonoBehaviour
    {
        public MapVisualizer MapVisualizer;
        public GameObject Content;

        void Awake()
        {
            MapVisualizer.OnMapVisualizerStateChanged += (s) =>
            {
                if (s == ModuleState.Finished)
                {
                    Content.SetActive(false);
                    Messenger.Broadcast(MapEvent.MAP_INITIALIZED);
                }
                else if (s == ModuleState.Working)
                {
                    Content.SetActive(true);
                }

            };
        }
    }
}
