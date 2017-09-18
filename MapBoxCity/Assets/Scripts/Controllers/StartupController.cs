using UnityEngine;

public class StartupController : MonoBehaviour
{
    [SerializeField]
    private GameObject _player;

    private void Awake()
    {
        Messenger.AddListener(MapEvent.MAP_INITIALIZED, OnMapInitialized);
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(MapEvent.MAP_INITIALIZED, OnMapInitialized);
    }

    private void OnMapInitialized()
    {
        GameObject player = Instantiate(_player);
        player.transform.position = new Vector3(-80, 1, -80); // TODO: Serialize? LatLng->World?
    }
}
