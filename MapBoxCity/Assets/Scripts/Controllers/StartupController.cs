namespace TreeCity
{
    using UnityEngine;

    public class StartupController : MonoBehaviour
    {
        private const string ESCAPE = "escape";

        [SerializeField]
        private GameObject _player;

        [SerializeField]
        private Reticle _reticle;

        private GameObject _playerInstance;

        private void Awake()
        {
            Messenger.AddListener(MapEvent.MAP_INITIALIZED, OnMapInitialized);
        }

        private void Start()
        {
            if (Screen.fullScreen)
            {
                _reticle.LockCursor();
            }
        }

        private void OnDestroy()
        {
            Messenger.RemoveListener(MapEvent.MAP_INITIALIZED, OnMapInitialized);
        }

        private void OnMapInitialized()
        {
            if (_playerInstance == null)
            {
                _playerInstance = Instantiate(_player);
                _playerInstance.transform.position = new Vector3(-80, 1, -80); // TODO: Serialize? LatLng->World?
            }
        }

        private void Update()
        {
            if (Input.GetKey(ESCAPE))
            {
                Application.Quit();
            }
        }
    }
}
