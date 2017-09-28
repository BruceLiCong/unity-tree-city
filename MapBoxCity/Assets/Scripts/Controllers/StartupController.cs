namespace TreeCity
{
    using UnityEngine;
    using UnityEngine.UI;

    public class StartupController : MonoBehaviour
    {
        private const string ESCAPE = "escape";

        [SerializeField]
        private GameObject _player;

        [SerializeField]
        private RayShooter _rayShooter;

        [SerializeField]
        private Text _reticleText;

        private GameObject _playerInstance;

        private void Awake()
        {
            Messenger.AddListener(MapEvent.MAP_INITIALIZED, OnMapInitialized);
        }

        private void Start()
        {
            if (Screen.fullScreen)
            {
                _rayShooter.LockCursor();
            }
        }

        private void OnDestroy()
        {
            Messenger.RemoveListener(MapEvent.MAP_INITIALIZED, OnMapInitialized);
        }

        private void OnMapInitialized()
        {
            _reticleText.gameObject.SetActive(true);

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
