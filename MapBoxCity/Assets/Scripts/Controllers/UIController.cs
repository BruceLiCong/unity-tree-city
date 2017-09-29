namespace TreeCity
{
    using UnityEngine;
    using UnityEngine.UI;
    using System.Collections;

    public class UIController : MonoBehaviour
    {
        private const float INSTRUCTION_FADE_DURATION = 2f;
        private const float INSTRUCTION_FADE_DELAY = 8f;

        [SerializeField]
        private SettingsModal _settingsModal;

        [SerializeField]
        private Text _reticle;

        [SerializeField]
        private Text _instructions;

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
            _reticle.gameObject.SetActive(false);
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
            _reticle.gameObject.SetActive(true);

            StartCoroutine(FadeTextToZeroAlpha(INSTRUCTION_FADE_DURATION, _instructions, INSTRUCTION_FADE_DELAY));
        }

        private IEnumerator FadeTextToZeroAlpha(float t, Text i, float delaySec = 0)
        {
            yield return new WaitForSeconds(delaySec);

            i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
            while (i.color.a > 0.0f)
            {
                i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
                yield return null;
            }
        }

        public void OnOpenSettings()
        {
            Managers.Controls.UpdateSelectionQueryEnabled(false);

            _settingsModal.gameObject.SetActive(true);
            _reticle.gameObject.SetActive(false);
        }

        public void OnCloseSettings()
        {
            Managers.Controls.UpdateSelectionQueryEnabled(true);

            _settingsModal.gameObject.SetActive(false);
            _reticle.gameObject.SetActive(true);
        }
    }

}
