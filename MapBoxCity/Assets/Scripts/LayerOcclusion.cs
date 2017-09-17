using UnityEngine;

[RequireComponent(typeof(Camera))]
public class LayerOcclusion : MonoBehaviour
{
    private const int MAX_LAYER_COUNT = 32;

    private Camera _camera;

    public string layerName;
    public float distance;

    private void Awake()
    {
        _camera = GetComponent<Camera>();
    }

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

        distances[LayerMask.NameToLayer(layerName)] = distance;
        _camera.layerCullDistances = distances;
    }
}
