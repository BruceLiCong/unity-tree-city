using UnityEngine;

public class LayerOcclusion : MonoBehaviour
{
    private const int MAX_LAYER_COUNT = 32;

    [SerializeField]
    private Camera _camera;

    [SerializeField]
    private string _layerName;

    [SerializeField]
    private float _distance;

    private void Start()
    {
        float[] distances = new float[MAX_LAYER_COUNT];

        if (_camera.layerCullDistances.Length == MAX_LAYER_COUNT)
        {
            for (int i = 0; i < MAX_LAYER_COUNT; i++)
            {
                distances[i] = _camera.layerCullDistances[i];
            }
        }

        distances[LayerMask.NameToLayer(_layerName)] = _distance;
        _camera.layerCullDistances = distances;
    }
}
