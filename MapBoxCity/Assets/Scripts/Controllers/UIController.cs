namespace TreeCity
{
    using UnityEngine;
    using UnityEngine.UI;

    public class UIController : MonoBehaviour
    {
        [SerializeField]
        private SettingsModal _settingsModal;

        [SerializeField]
        private Text _reticleText;

        private void Awake()
        {
            Messenger.AddListener(MapEvent.MAP_INITIALIZED, OnMapInitialized);
        }

        private void Start()
        {
            if (Screen.fullScreen)
            {
                Managers.Controls.LockCursor();
            }

            _settingsModal.gameObject.SetActive(false);
            _reticleText.gameObject.SetActive(false);
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.C))
            {
                OnOpenSettings();
            }
        }

        private void OnDestroy()
        {
            Messenger.RemoveListener(MapEvent.MAP_INITIALIZED, OnMapInitialized);
        }

        private void OnMapInitialized()
        {
            _reticleText.gameObject.SetActive(true);
        }

        public void OnOpenSettings()
        {
            Managers.Controls.UpdateSelectionQueryEnabled(false);

            _settingsModal.gameObject.SetActive(true);
            _reticleText.gameObject.SetActive(false);
        }

        public void OnCloseSettings()
        {
            Managers.Controls.UpdateSelectionQueryEnabled(true);

            _settingsModal.gameObject.SetActive(false);
            _reticleText.gameObject.SetActive(true);
        }
    }

}
